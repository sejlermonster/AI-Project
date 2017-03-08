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

        public void FindPath(int xStart, int yStart, int xEnd, int yEnd)
        {
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

            int[,] worldClosed = _worldMatrixWithPossiblePaths;

            worldClosed[xStart, yStart] = 2; // 2 = checked. A place we have already been
            worldClosed[xEnd, yEnd] = 99;

            var x = xStart;
            var y = yStart;
            var g = 0;
            
            List<int> init = new List<int>();
            init.Add(g);
            init.Add(x);
            init.Add(y);

            List<List<int>> open = new List<List<int>>();
            open.Add(init);

            bool found = false;
            bool resign = false;

            while (!found && !resign)
            {
                if (open.Count == 0)
                {
                    resign = true;
                    Debug.Write("Fail");
                }
                else
                {
                    open = open.OrderBy(p => p.ElementAt(2)).OrderBy(p => p.ElementAt(1)).OrderBy(p => p.ElementAt(0)).ToList();
                    open.Reverse();
                    var next = open.Last();
                    open.RemoveAt(open.Count-1);
                 
                    g = next[0];
                    x = next[1];
                    y = next[2];

                    if (x == xEnd && y == yEnd)
                    {
                        found = true;
                        Debug.Write("["+ next[0] + ", " + next[1] + ", " + next[2] + "]");
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
                                if(worldClosed[x2,y2] == 0 && _worldMatrixWithPossiblePaths[x2,y2] == 0 || worldClosed[x2, y2] == 99 && _worldMatrixWithPossiblePaths[x2, y2] == 99)
                                {
                                    int g2 = g + cost;
                                    open.Add(new List<int>()
                                    {
                                        g2,x2,y2
                                    });
                                    worldClosed[x2, y2] = 2;
                                }
                            }
                            
                        }

                    //for i in range(len(delta)):
                    //x2 = x + delta[i][0]
                    //y2 = y + delta[i][1]
                    //if x2 >= 0 and x2 < len(grid) and y2 >= 0 and y2 < len(grid[0]):
                    //    if closed[x2][y2] == 0 and grid[x2][y2] == 0:
                    //        g2 = g + cost
                    //        open.append([g2, x2, y2])
                    //        closed[x2][y2] = 1
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

    }
}
