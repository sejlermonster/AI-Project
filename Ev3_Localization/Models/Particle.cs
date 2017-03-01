﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ev3_Localization.Models
{
    public class Particle : IComparable
    {
        public Point Position { get; set; }
        public double OrientationInRadians { get; set; }
        public double Weight { get; set; }

        public int CompareTo(object obj)
        {
            var obj1 = obj as Particle;
            if (Weight < obj1.Weight)
                return -1;
            if (Weight == obj1.Weight)
                return 0;
            else
                return 1;
        }

        public Particle Clone()
        {
            return new Particle()
            {
                Position = new Point(this.Position.X, Position.Y),
                Weight = this.Weight,
                OrientationInRadians = OrientationInRadians

            };
        }
    }
}
