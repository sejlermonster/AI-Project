using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lego.Ev3.Core;

namespace Ev3_Localization
{
    class RobotMotion
    {
        private readonly Brick _brick;
        private readonly uint _time;
        private readonly int _motorA;
        private readonly int _motorD;
        public RobotMotion(Brick brick, uint time, int motorA, int motorB)
        {
            _brick = brick;
            _time = time;
            _motorA = motorA;
            _motorD = motorB;
        }
        public async Task DriveForward()
        {
            _brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, _motorA, _time, true);
            _brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, _motorD, _time, true);
            await _brick.BatchCommand.SendCommandAsync();
            Thread.Sleep(Convert.ToInt32(_time));
        }

        public async Task DriveBackwards()
        {
            _brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, -_motorA, _time, true);
            _brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, -_motorD, _time, true);
            await _brick.BatchCommand.SendCommandAsync();
            Thread.Sleep(Convert.ToInt32(_time));
        }

        public async Task TurnRight(int degrees)
        {
            var time = Convert.ToUInt32(DegreesToSeconds(degrees));
            _brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, -_motorA, time, true);
            _brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, _motorD, time, true);
            await _brick.BatchCommand.SendCommandAsync();
            Thread.Sleep(Convert.ToInt32(time+500));

        }

        public async Task TurnLeft(int degrees)
        {
            var time = Convert.ToUInt32(DegreesToSeconds(degrees));
            _brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, _motorA, time, true);
            _brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, -_motorD, time, true);
            await _brick.BatchCommand.SendCommandAsync();
            Thread.Sleep(Convert.ToInt32(time+500));
        }

        private static float DegreesToSeconds(int degrees)
        {
            float timeToTurn360 = 2510;
            var val = ((310 / 270) * (360-degrees))/(360/90);
            //float timeToTurn360 = 2820;
            var seconds =  degrees*(timeToTurn360/360) + val;
            Debug.WriteLine("Degrees" + degrees + " seconds: " + seconds);
            return seconds;
        }
    }
}
