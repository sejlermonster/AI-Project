using System.Collections.Generic;
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
            //await _brick.ConnectAsync();
            //await _brick.DirectCommand.PlayToneAsync(100, 1000, 300);

            _robotMotion = new RobotMotion(_brick, _time, _motorA, _motorD);
            _robotSensing = new RobotSensing(_brick);
            var world = new double[100, 50];
            var landmarks = new List<Landmark>
                            {
                                new Landmark(new Point(20, 40), new Point(25, 40), new Point(20, 35), new Point(25, 35)),
                                new Landmark(new Point(70, 30), new Point(75, 30), new Point(70, 25), new Point(75, 25)),
                                new Landmark(new Point(30, 15), new Point(35, 15), new Point(30, 10), new Point(35, 10))
                            };
            _particleFilter = new ParticleFilter(landmarks, world);

            _particleFilter.GenerateParticles(1000, 100, 50);

            _particleFilter.Resampling(50);
            _particleFilter.MoveParticles(10);
            _particleFilter.Resampling(40);
            _particleFilter.MoveParticles(10);
            _particleFilter.Resampling(30);
            _particleFilter.MoveParticles(10);
            _particleFilter.Resampling(20);
            _particleFilter.MoveParticles(10);
            _particleFilter.ParticlesTurnLeft(90);
            _particleFilter.Resampling(10);
            _particleFilter.ParticlesTurnRight(180);
            _particleFilter.Resampling(90);
            _particleFilter.Resampling(90);
            _particleFilter.Resampling(90);
            _particleFilter.Resampling(90);
            _particleFilter.Resampling(90);
            _particleFilter.Resampling(90);
            _particleFilter.Resampling(90);
            _particleFilter.Resampling(90);
            _particleFilter.Resampling(90);
            _particleFilter.Resampling(90);
            



            //await _robotMotion.DriveForward();
            //await _robotMotion.DriveBackwards();
            //await _robotMotion.TurnLeft(90);
            //await _robotMotion.TurnRight(360);
            //await _robotMotion.DriveForward();
            //  await _robotMotion.TurnRight(90);
        }
    }
}
