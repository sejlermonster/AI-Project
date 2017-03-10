using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ev3_Localization.Models
{
    public class MeanAndVariance
    {
        public Point Mean { get; set; }
        public double MeanOrientation { get; set; }
        public Point Variance { get; set; }
    }
}
