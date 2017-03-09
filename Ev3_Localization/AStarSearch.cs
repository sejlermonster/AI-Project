using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Ev3_Localization
{

    public class AStarSearch
    {
        private readonly int[,] _worldMatrixWithPossiblePaths;
        
        public AStarSearch(int xLength, int yLength, List<Landmark> landmarks)
        {
            _worldMatrixWithPossiblePaths = new int[xLength, yLength];
            for (int i = 0; i < _worldMatrixWithPossiblePaths.GetLength(0); i++)
            {
                for (int j = 0; j < _worldMatrixWithPossiblePaths.GetLength(1); j++)
                {
                    foreach (var landmark in landmarks)
                    {
                        if(_worldMatrixWithPossiblePaths[i, j] == 0)
                        {
                            _worldMatrixWithPossiblePaths[i, j] = InArea(i, j, landmark);
                        }
                    }
                }
            }
        }


        public void PrintMap(int xStart, int yStart, int xEnd, int yEnd)
        {
            for (int i = 0; i < _worldMatrixWithPossiblePaths.GetLength(0); i++)
            {
                for (int j = 0; j < _worldMatrixWithPossiblePaths.GetLength(1); j++)
                {
                    var P = "*";
                    if (_worldMatrixWithPossiblePaths[i, j] == 1)
                        P = "░";
                    else if (i == xStart && j == yStart)
                        P = "S";
                    else if (i == xEnd && j == yEnd)
                        P = "E";

                    Debug.Write(P + "");
                }
                Debug.Write("\n");
            }
        }

        public void PrintDirection(int xStart, int yStart, int xEnd, int yEnd, int[,] _direct, int g)
        {
            for (int i = 0; i < _direct.GetLength(0); i++)
            {
                for (int j = 0; j < _direct.GetLength(1); j++)
                {
                    var P = _direct[i,j].ToString();
                    if (_worldMatrixWithPossiblePaths[i, j] == 1)
                        P = "░";
                    else if (i == xStart && j == yStart)
                        P = "S";
                    else if (i == xEnd && j == yEnd)
                        P = "E";
                    else if (_direct[i, j] == 10)
                        P = "^";
                    else if (_direct[i, j] == 11)
                        P = "<";
                    else if (_direct[i, j] == 12)
                        P = "v";
                    else if (_direct[i, j] == 13)
                        P = ">";

                    Debug.Write(P + " ");
                }
                Debug.Write("\n");
            }
        }

        public void FindPath(int xStart, int yStart, int xEnd, int yEnd)
        {
            _worldMatrixWithPossiblePaths[0,1] = 1;
            _worldMatrixWithPossiblePaths[1, 1] = 1;
            _worldMatrixWithPossiblePaths[2, 1] = 1;
            _worldMatrixWithPossiblePaths[3, 1] = 1;
            _worldMatrixWithPossiblePaths[4, 1] = 0;

            int[,] heuristic = GenerateHeuristicFunciton(_worldMatrixWithPossiblePaths.GetLength(1), _worldMatrixWithPossiblePaths.GetLength(0), xEnd, yEnd);

            // Create expand matrix with all -1
            int [,] expand = new int[_worldMatrixWithPossiblePaths.GetLength(0), _worldMatrixWithPossiblePaths.GetLength(1)];
            for (int i = 0; i < expand.GetLength(0); i++)
            {
                for (int j = 0; j < expand.GetLength(1); j++)
                {
                    expand[i, j] = -1;
                }
            }
            // Create action matrix with all -1
            int[,] action = new int[_worldMatrixWithPossiblePaths.GetLength(0), _worldMatrixWithPossiblePaths.GetLength(1)];
            for (int i = 0; i < action.GetLength(0); i++)
            {
                for (int j = 0; j < action.GetLength(1); j++)
                {
                    action[i, j] = 0;
                }
            }

        
            

            // Create delta list with directions
            int cost = 1;
            List<int> up = new List<int>();
            up.Add(-1);
            up.Add(0);

            List<int> left = new List<int>();
            left.Add(0);
            left.Add(-1);

            List<int> down = new List<int>();
            down.Add(1);
            down.Add(0);

            List<int> right = new List<int>();
            right.Add(0);
            right.Add(1);

            List<List<int>> delta =  new List<List<int>>(); //[[-1, 0],[ 0,-1],[ 1, 0],[ 0, 1]]
            delta.Add(up);
            delta.Add(left);
            delta.Add(down);
            delta.Add(right);

            // Clone _worldMatrixWithPossiblePaths into worldClosed
            int[,] worldClosed = new int[_worldMatrixWithPossiblePaths.GetLength(0), _worldMatrixWithPossiblePaths.GetLength(1)];
            for (int i = 0; i < worldClosed.GetLength(0); i++)
            {
                for (int j = 0; j < worldClosed.GetLength(1); j++)
                {
                    worldClosed[i, j] = _worldMatrixWithPossiblePaths[i,j];
                }
            }

            worldClosed[xStart, yStart] = 2; // 2 = checked. A place we have already been
            worldClosed[xEnd, yEnd] = 99;

            var x = xStart;
            var y = yStart;
            var g = 0;
            var h = heuristic[x, y];
            var f = g + h;

            List<int> init = new List<int>();
            init.Add(f);
            init.Add(g);
            init.Add(h);
            init.Add(x);
            init.Add(y);

            List<List<int>> open = new List<List<int>>();
            open.Add(init);

            List<List<int>> possibleMovements = new List<List<int>>();
            List<List<int>> robotActions = new List<List<int>>();


            bool found = false;
            bool resign = false;
            int count = 0;

            while (!found && !resign)
            {
                if (open.Count == 0)
                {
                    resign = true;
                    Debug.Write("Fail");
                   }
                else
                {
                    open = open.OrderBy(p => p.ElementAt(4)).OrderBy(p => p.ElementAt(3)).OrderBy(p => p.ElementAt(2)).OrderBy(p => p.ElementAt(1)).OrderBy(p => p.ElementAt(0)).ToList();
                    open.Reverse();
                    var next = open.Last();
                    open.RemoveAt(open.Count-1);
                 
                    g = next[1];
                    x = next[3];
                    y = next[4];
                
                    expand[x, y] = count;
                    count += 1;

                    if (x == xEnd && y == yEnd)
                    {
                        found = true;

                        Debug.Write("["+ next[0] + ", " + next[1] + ", " + next[2] + "]" + "\n");

                        for (int i = 0; i < possibleMovements.Count; i++)
                        {
                            if (possibleMovements[i].First() <= next.First())
                            {
                                robotActions.Add(possibleMovements[i].GetRange(1,2));
                            }
                        }

                        //PrintDirection(xStart, yStart, xEnd, yEnd, action, g);
                    }
                    else
                    {
                        for (int i = 0; i < delta.Count; i++)
                        {
                            int x2 = x + delta[i][0];
                            int y2 = y + delta[i][1];

                            if (x2 >= 0 && x2 < _worldMatrixWithPossiblePaths.GetLength(0) && y2 >= 0 &&
                                y2 < _worldMatrixWithPossiblePaths.GetLength(1))
                            {
                                if(worldClosed[x2,y2] == 0 && _worldMatrixWithPossiblePaths[x2,y2] == 0 || worldClosed[x2, y2] == 99 && _worldMatrixWithPossiblePaths[x2, y2] == 0)
                                {
                                    int g2 = g + cost;
                                    var h2 = heuristic[x2, y2];
                                    var f2 = g2 + h2;
                                    open.Add(new List<int>()
                                    {
                                        f2, g2, h2, x2, y2
                                    });
                                    worldClosed[x2, y2] = 2;
                                    possibleMovements.Add(new List<int>()
                                    {
                                        f2, x2, y2
                                    });
                                }
                            }
                            
                        }
                    }
                }
            }
        }

        private int InArea(int x, int y, Landmark landmark)
        {
            var minX = landmark.BottomLeft.X < landmark.BottomRight.X ? landmark.BottomLeft.X : landmark.BottomRight.X;
            var maxX = landmark.BottomLeft.X < landmark.BottomRight.X ? landmark.BottomRight.X : landmark.BottomLeft.X;
            var minY = landmark.BottomLeft.Y < landmark.UpperLeft.Y ? landmark.BottomLeft.Y : landmark.UpperLeft.Y;
            var maxY = landmark.BottomLeft.Y < landmark.UpperLeft.Y ? landmark.UpperLeft.Y : landmark.BottomLeft.Y;

            if ((x <= maxX && x >= minX && y >= minY && y <= maxY))
            {
                return 1;
            }
            return 0;
        }

        private int[,] GenerateHeuristicFunciton(int rows, int columns, int endX, int endY)
        {
            int[,] heuristic = new int[rows, columns];

            Point goal = new Point(endX, endY);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    heuristic[i, j] = Convert.ToInt32(Math.Abs(i - goal.X) + Math.Abs(j - goal.Y));
                }
            }

            for (int i = 0; i < heuristic.GetLength(0); i++)
            {
                for (int j = 0; j < heuristic.GetLength(1); j++)
                {

                    Debug.Write(heuristic[i, j] + " ");
                }
                Debug.Write("\n");
            }
            return heuristic;
        }

    }
}
