using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ev3_Localization
{
    public class Landmark
    {
        public Landmark(Point upperL, Point upperR, Point bottomL, Point bottomR)
        {
            UpperRight = upperR;
            UpperLeft = upperL;
            BottomRight = bottomR;
            BottomLeft = bottomL;
        }
        public Point UpperRight { get; set; }
        public Point UpperLeft { get; set; }
        public Point BottomRight { get; set; }
        public Point BottomLeft { get; set; }
    }
}
