using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lego.Ev3.Core;

namespace Ev3_Localization
{
    public class RobotSensing
    {
        private readonly Brick _brick;
        public RobotSensing(Brick brick)
        {
            _brick = brick;
            _brick.BrickChanged += OnBrickChanged;
        }

        private static void OnBrickChanged(object sender, BrickChangedEventArgs e)
        {
            // print out the value of the sensor on Port 1 (more on this later...)
            Debug.WriteLine(e.Ports[InputPort.One].SIValue);
        }
    }
}
