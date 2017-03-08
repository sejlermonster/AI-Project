using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Lego.Ev3.Core;

namespace Ev3_Localization
{
    class RobotMotion
    {
        private readonly Brick _brick;
        private readonly uint _time;
        private readonly int _motorA;
        private readonly int _motorD;
        private readonly RobotSensing _robotSensing;

        public RobotMotion(Brick brick, uint time, int motorA, int motorB, RobotSensing robotSensing)
        {
            _brick = brick;
            _time = time;
            _motorA = motorA;
            _motorD = motorB;
            _robotSensing = robotSensing;
        }

        public async Task DriveForward(int distance)
        {
            var startDistance = _robotSensing.GetDistanceInCentimeters();
            var endDistance = startDistance - distance;
            double currentDistance = startDistance;
            while (currentDistance < endDistance - 1 || currentDistance > endDistance + 1)
            {
                if (currentDistance > endDistance)
                {
                    _brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, _motorA, 30, true);
                    _brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, _motorD, 30, true);
                    await _brick.BatchCommand.SendCommandAsync();
                    Thread.Sleep(45);
                }
                else if (currentDistance < endDistance)
                {
                    _brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, -_motorA, 30, true);
                    _brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, -_motorD, 30, true);
                    await _brick.BatchCommand.SendCommandAsync();
                    Thread.Sleep(45);
                }
                currentDistance = _robotSensing.GetDistanceInCentimeters();
            }

            //var time = DistanceInCentimetersToSeconds(distance);
            //_brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, 40, time, true);
            //_brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, 42, time, true);
            //await _brick.BatchCommand.SendCommandAsync();
            //Thread.Sleep(Convert.ToInt32(time));
        }

        public async Task DriveBackwards(int distance)
        {
            var time = DistanceInCentimetersToSeconds(distance);
            _brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, -_motorA, time, true);
            _brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, -_motorD, time, true);
            await _brick.BatchCommand.SendCommandAsync();
            Thread.Sleep(Convert.ToInt32(time));
        }

        public async Task TurnRight(int degrees)
        {
            var startDegree = _robotSensing.GetGyroData();
            var endDegree = startDegree + degrees;
            double currentDegree = startDegree;
            while (currentDegree < endDegree - 0.02 * degrees || currentDegree > endDegree + 0.01*degrees)
            {
                if (currentDegree > endDegree + 0.01 * degrees)
                {
                    _brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, _motorA, 30, true);
                    _brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, -_motorD, 30, true);
                    await _brick.BatchCommand.SendCommandAsync();
                    Thread.Sleep(100);
                }
                else if (currentDegree < endDegree - 0.02 * degrees)
                {
                    _brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, -_motorA, 30, true);
                    _brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, _motorD, 30, true);
                    await _brick.BatchCommand.SendCommandAsync();
                    Thread.Sleep(100);
                }
                currentDegree = _robotSensing.GetGyroData();
            }
            //var time = Convert.ToUInt32(DegreesToSeconds(degrees));
            //_brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, -_motorA, time, true);
            //_brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, _motorD, time, true);
            //await _brick.BatchCommand.SendCommandAsync();
            //Thread.Sleep(Convert.ToInt32(time+500));

        }

        public async Task TurnLeft(int degrees)
        {
            var startDegree = _robotSensing.GetGyroData();
            var endDegree = startDegree - degrees;
            double currentDegree = startDegree;
            while (currentDegree > endDegree + 1 || currentDegree < endDegree - 1)
            {
                if (currentDegree > endDegree)
                {
                    _brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, _motorA, 40, true);
                    _brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, -_motorD, 40, true);
                    await _brick.BatchCommand.SendCommandAsync();
                    Thread.Sleep(100);
                }
                else if (currentDegree < endDegree)
                {
                    _brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, -_motorA, 40, true);
                    _brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, _motorD, 40, true);
                    await _brick.BatchCommand.SendCommandAsync();
                    Thread.Sleep(100);
                }
                currentDegree = _robotSensing.GetGyroData();
            }
            //var time = Convert.ToUInt32(DegreesToSeconds(degrees));
            //_brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, _motorA, time, true);
            //_brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, -_motorD, time, true);
            //await _brick.BatchCommand.SendCommandAsync();
            //Thread.Sleep(Convert.ToInt32(time+500));
        }

        private static double DegreesToSeconds(int degrees)
        {
            float timeToTurn360 = 2660;
            var val = ((310 / 270) * (360 - degrees)) / (360 / 90);
            //float timeToTurn360 = 2820;
            var seconds = (degrees * (timeToTurn360 / 360) + val); //* 5.0;
            Debug.WriteLine("Degrees" + degrees + " seconds: " + seconds);
            return seconds;
        }

        private static UInt32 DistanceInCentimetersToSeconds(double distance)
        {
            var val = 0.7 * distance / 8;
            var time = Convert.ToUInt32(distance * 62 - val * 62);
            return time;
        }
    }
}
