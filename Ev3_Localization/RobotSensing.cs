using System;
using Lego.Ev3.Core;

namespace Ev3_Localization
{
    public class RobotSensing
    {
        private readonly Brick _brick;
        public static float DistanceMeasurement { get; set; }
        public static float Gyro { get; set; }
        public static bool SensingReady { get; set; }
        public RobotSensing(Brick brick )
        {
            _brick = brick;
            _brick.BrickChanged += OnBrickChanged;
            _brick.Ports[InputPort.Four].SetMode(GyroscopeMode.Angle);
        }

        public double GetDistanceInCentimeters()
        {
            return _brick.Ports[InputPort.One].SIValue - 8.6;
        }

        public double GetGyroData()
        {
            return _brick.Ports[InputPort.Four].SIValue;
        }
        
        private static void OnBrickChanged(object sender, BrickChangedEventArgs e)
        {
        }
    }
}
