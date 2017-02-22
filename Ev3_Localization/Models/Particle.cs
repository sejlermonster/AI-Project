using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ev3_Localization.Models
{
    public class Particle
    {
        public Point Position { get; set; }
        public int OrientationInDegrees { get; set; }
        public double Weight { get; set; }

    }
}
