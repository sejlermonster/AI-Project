using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using Lego.Ev3.Core;
using Lego.Ev3.Desktop;

namespace Ev3_Localization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Brick _brick;
        //Difference in motor speed to calibrate them to drive with equal force.
        private readonly int _motorA = 40;
        private readonly int _motorD = 42;
        private ParticleFilter _particleFilter;
        private RobotMotion _robotMotion;
        private RobotSensing _robotSensing;
        private readonly uint _time = 5000;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            _brick = new Brick(new BluetoothCommunication("COM3"));
            await _brick.ConnectAsync();
            await _brick.DirectCommand.PlayToneAsync(100, 1000, 300);

            _robotMotion = new RobotMotion(_brick, _time, _motorA, _motorD);
            _robotSensing = new RobotSensing(_brick);
            var world = new double[125, 125];
            var landmarks = new List<Landmark>
                            {
                                new Landmark(new Point(16, 109.5), new Point(27.5,109.5), new Point(16, 106), new Point(27.5,106.5)),
                                new Landmark(new Point(24.5,56), new Point(36,56), new Point(24.5, 52.5), new Point(36,52.5)),
                                new Landmark(new Point(88.5, 32.5), new Point(100, 32.5), new Point(88.5, 29 ), new Point(100, 29)),
                                new Landmark(new Point(85.5, 102.5), new Point(89, 102.5), new Point(85.5, 91), new Point(89, 91))
                            };
            _particleFilter = new ParticleFilter(landmarks, world);

            _particleFilter.GenerateParticles(1000, 100, 50);

            //First measurement
            _particleFilter.Resampling(RobotSensing.DistanceMeasurement);
            Debug.Write(RobotSensing.DistanceMeasurement);

            Thread.Sleep(1000);
            await  _robotMotion.TurnRight(100);
            _particleFilter.ParticlesTurnRight(90);
            _particleFilter.Resampling(RobotSensing.DistanceMeasurement);
            _particleFilter.Resampling(RobotSensing.DistanceMeasurement);
            Debug.Write(RobotSensing.DistanceMeasurement);

            Thread.Sleep(1000);
            await _robotMotion.TurnRight(100);
            _particleFilter.ParticlesTurnRight(90);
            _particleFilter.Resampling(RobotSensing.DistanceMeasurement);
            _particleFilter.Resampling(RobotSensing.DistanceMeasurement);
            Debug.Write(RobotSensing.DistanceMeasurement);

            Thread.Sleep(1000);
            await _robotMotion.DriveForward(20);
            _particleFilter.MoveParticles(20);
            _particleFilter.Resampling(RobotSensing.DistanceMeasurement);
            _particleFilter.Resampling(RobotSensing.DistanceMeasurement);
            Debug.Write(RobotSensing.DistanceMeasurement);

            Thread.Sleep(1000);
            await _robotMotion.TurnRight(100);
            _particleFilter.ParticlesTurnRight(90);
            _particleFilter.Resampling(RobotSensing.DistanceMeasurement);
            _particleFilter.Resampling(RobotSensing.DistanceMeasurement);
            Debug.Write(RobotSensing.DistanceMeasurement);

            Thread.Sleep(1000);
            await _robotMotion.DriveForward(20);
            _particleFilter.MoveParticles(20);
            _particleFilter.Resampling(RobotSensing.DistanceMeasurement);
            _particleFilter.Resampling(RobotSensing.DistanceMeasurement);
            Debug.Write(RobotSensing.DistanceMeasurement);

            Thread.Sleep(1000);
            await _robotMotion.TurnRight(100);
            _particleFilter.ParticlesTurnRight(90);
            _particleFilter.Resampling(RobotSensing.DistanceMeasurement);
            _particleFilter.Resampling(RobotSensing.DistanceMeasurement);
            Debug.Write(RobotSensing.DistanceMeasurement);

            Thread.Sleep(1000);
            await _robotMotion.TurnRight(100);
            _particleFilter.ParticlesTurnRight(90);
            _particleFilter.Resampling(RobotSensing.DistanceMeasurement);
            _particleFilter.Resampling(RobotSensing.DistanceMeasurement);
            Debug.Write(RobotSensing.DistanceMeasurement);

            Thread.Sleep(1000);
            await _robotMotion.TurnRight(100);
            _particleFilter.ParticlesTurnRight(90);
            _particleFilter.Resampling(RobotSensing.DistanceMeasurement);
            _particleFilter.Resampling(RobotSensing.DistanceMeasurement);
            Debug.Write(RobotSensing.DistanceMeasurement);

            Thread.Sleep(1000);
            await _robotMotion.TurnRight(100);
            _particleFilter.ParticlesTurnRight(90);
            _particleFilter.Resampling(RobotSensing.DistanceMeasurement);
            _particleFilter.Resampling(RobotSensing.DistanceMeasurement);
            _particleFilter.Resampling(RobotSensing.DistanceMeasurement);
            Debug.Write(RobotSensing.DistanceMeasurement);

            _particleFilter.PrintParticles();
            //_particleFilter.Resampling(50);
            //_particleFilter.MoveParticles(10);
            //_particleFilter.Resampling(40);
            //_particleFilter.MoveParticles(10);
            //_particleFilter.Resampling(30);
            //_particleFilter.MoveParticles(10);
            //_particleFilter.Resampling(20);
            //_particleFilter.MoveParticles(10);
            //_particleFilter.ParticlesTurnLeft(90);
            //_particleFilter.Resampling(10);
            //_particleFilter.ParticlesTurnRight(180);
            //_particleFilter.Resampling(90);
            //_particleFilter.Resampling(90);
            //_particleFilter.Resampling(90);
            //_particleFilter.Resampling(90);
            //_particleFilter.Resampling(90);
            //_particleFilter.Resampling(90);
            //_particleFilter.Resampling(90);
            //_particleFilter.Resampling(90);
            //_particleFilter.Resampling(90);
            //_particleFilter.Resampling(90);
        }
    }
}
