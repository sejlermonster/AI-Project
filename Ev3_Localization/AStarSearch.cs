using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ev3_Localization
{
    public class AStarSearch
    {
        private readonly bool[,] _worldMatrixWithPossiblePaths;

        public AStarSearch(int xLength, int yLength, List<Landmark> landmarks)
        {
            _worldMatrixWithPossiblePaths = new bool[xLength, yLength];
            for (int i = 0; i < _worldMatrixWithPossiblePaths.GetLength(0); i++)
            {
                for (int j = 0; j < _worldMatrixWithPossiblePaths.GetLength(1); j++)
                {
                    foreach (var landmark in landmarks)
                    {
                        _worldMatrixWithPossiblePaths[i, j] = InArea(i, j, landmark);
                    }
                }
            }
        }

        public void PrintMap()
        {
            for (int i = 0; i < _worldMatrixWithPossiblePaths.GetLength(0); i++)
            {
                for (int j = 0; j < _worldMatrixWithPossiblePaths.GetLength(1); j++)
                {
                    var P = "*";
                    if (_worldMatrixWithPossiblePaths[i, j])
                        P = "░";
                           
                    Debug.Write(P + "");
                }
                Debug.Write("\n");
            }
        }

        public void FindPath()
        {

        }

        private bool InArea(int x, int y, Landmark landmark)
        {
            var minX = landmark.BottomLeft.X < landmark.BottomRight.X ? landmark.BottomLeft.X : landmark.BottomRight.X;
            var maxX = landmark.BottomLeft.X < landmark.BottomRight.X ? landmark.BottomRight.X : landmark.BottomLeft.X;
            var minY = landmark.BottomLeft.Y < landmark.UpperLeft.Y ? landmark.BottomLeft.Y : landmark.UpperLeft.Y;
            var maxY = landmark.BottomLeft.Y < landmark.UpperLeft.Y ? landmark.UpperLeft.Y : landmark.BottomLeft.Y;
            return (x <= maxX && x >= minX && y >= minY && y <= maxY);
        }

    }
}
