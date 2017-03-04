using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Ev3_Localization.Models;
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
        private bool _flag;
        //Difference in motor speed to calibrate them to drive with equal force.
        //private readonly int _motorA = 40;
        //private readonly int _motorD = 42;
        private readonly int _motorA = 40;
        private readonly int _motorD = 42;
        private ParticleFilter _particleFilter;
        private RobotMotion _robotMotion;
        private RobotSensing _robotSensing;
        private readonly uint _time = 5000;
        private List<Landmark> _landmarks;
        private int _numberOfParticles = 1000;

        public MainWindow()
        {
            InitializeComponent();

        }

        private async void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            _brick = new Brick(new BluetoothCommunication("COM3"));
            await _brick.ConnectAsync(TimeSpan.FromMilliseconds(10));
            await _brick.DirectCommand.PlayToneAsync(100, 1000, 300);
            _brick.BrickChanged += OnBrickChanged;
            _robotSensing = new RobotSensing(_brick);
            _robotMotion = new RobotMotion(_brick, _time, _motorA, _motorD, _robotSensing);
            var world = new double[125, 125];
            _landmarks = new List<Landmark>
                            {
                                new Landmark(new Point(16, 109.5), new Point(27.5,109.5), new Point(16, 106), new Point(27.5,106.5)),
                                //new Landmark(new Point(24.5,56), new Point(36,56), new Point(24.5, 52.5), new Point(36,52.5)),
                                new Landmark(new Point(88.5, 32.5), new Point(100, 32.5), new Point(88.5, 29 ), new Point(100, 29)),
                                new Landmark(new Point(85.5, 102.5), new Point(89, 102.5), new Point(85.5, 91), new Point(89, 91))
                            };
            _particleFilter = new ParticleFilter(_landmarks, world);

            _particleFilter.GenerateParticles(_numberOfParticles, world.GetLength(0), world.GetLength(1));

            await StartDriving();
        }

        private async Task StartDriving()
        {


            //Debug.WriteLine(_robotSensing.GetGyroData());

            ////await _robotMotion.TurnLeft(90);
            ////Thread.Sleep(5000);
            ////await _robotMotion.TurnRight(90);
            //Thread.Sleep(5000);
            //await _robotMotion.TurnRight(30);
            //_particleFilter.ParticlesTurnRight(30);
            ////_particleFilter.Resampling(_brick.Ports[InputPort.One].SIValue);
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());

            //Thread.Sleep(5000);
            //await _robotMotion.TurnRight(30);
            //_particleFilter.ParticlesTurnRight(30);
            ////_particleFilter.Resampling(_brick.Ports[InputPort.One].SIValue);
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());

            //Thread.Sleep(5000);
            //await _robotMotion.TurnRight(30);
            //_particleFilter.ParticlesTurnRight(30);
            ////_particleFilter.Resampling(_brick.Ports[InputPort.One].SIValue);
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());

            //Thread.Sleep(5000);
            //await _robotMotion.TurnRight(30);
            //_particleFilter.ParticlesTurnRight(30);
            ////_particleFilter.Resampling(_brick.Ports[InputPort.One].SIValue);
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());



            //Thread.Sleep(5000);
            //await _robotMotion.TurnRight(30);
            //_particleFilter.ParticlesTurnRight(30);
            ////_particleFilter.Resampling(_brick.Ports[InputPort.One].SIValue);
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());



            //Thread.Sleep(5000);
            //await _robotMotion.TurnRight(30);
            //_particleFilter.ParticlesTurnRight(30);
            ////_particleFilter.Resampling(_brick.Ports[InputPort.One].SIValue);
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());


            //Thread.Sleep(5000);
            //await _robotMotion.DriveForward(15);
            //_particleFilter.MoveParticles(15);
            ////_particleFilter.Resampling(_brick.Ports[InputPort.One].SIValue);
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());


            //Thread.Sleep(5000);
            //await _robotMotion.DriveForward(15);
            //_particleFilter.MoveParticles(15);
            ////_particleFilter.Resampling(_brick.Ports[InputPort.One].SIValue);
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());

            //Thread.Sleep(5000);
            //await _robotMotion.DriveForward(15);
            //_particleFilter.MoveParticles(15);
            ////_particleFilter.Resampling(_brick.Ports[InputPort.One].SIValue);
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());

            //Thread.Sleep(5000);
            //await _robotMotion.TurnRight(90);
            //_particleFilter.ParticlesTurnRight(90);
            ////_particleFilter.Resampling(_brick.Ports[InputPort.One].SIValue);
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());

            //Thread.Sleep(5000);
            //await _robotMotion.DriveForward(20);
            //_particleFilter.MoveParticles(20);
            ////_particleFilter.Resampling(_brick.Ports[InputPort.One].SIValue);
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());

            //Thread.Sleep(5000);
            //await _robotMotion.TurnRight(90);
            //_particleFilter.ParticlesTurnRight(90);
            ////_particleFilter.Resampling(_brick.Ports[InputPort.One].SIValue);
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());

            //Thread.Sleep(5000);
            //await _robotMotion.TurnRight(90);
            //_particleFilter.ParticlesTurnRight(90);
            ////_particleFilter.Resampling(_brick.Ports[InputPort.One].SIValue);
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());

            //Thread.Sleep(5000);
            //await _robotMotion.DriveForward(10);
            //_particleFilter.MoveParticles(10);
            ////_particleFilter.Resampling(_brick.Ports[InputPort.One].SIValue);
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());

            //for (int i = 0; i < 360/30; i++)
            //{

            //    Debug.WriteLine("******* Iteration " + i + "*********");
            //    await _robotMotion.TurnRight(30);
            //    _particleFilter.ParticlesTurnRight(30);
            //    //_particleFilter.Resampling(_brick.Ports[InputPort.One].SIValue);
            //    _particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //    Debug.WriteLine("Distance: " +_robotSensing.GetDistanceInCentimeters());
            //    Debug.WriteLine("Gyro: " + _robotSensing.GetGyroData());
            //    Thread.Sleep(5000);
            //}

            //Thread.Sleep(5000);
            //await _robotMotion.DriveForward(15);
            //_particleFilter.MoveParticles(15);
            ////_particleFilter.Resampling(_brick.Ports[InputPort.One].SIValue);
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());

            //Thread.Sleep(5000);
            //await _robotMotion.DriveForward(15);
            //_particleFilter.MoveParticles(15);
            ////_particleFilter.Resampling(_brick.Ports[InputPort.One].SIValue);
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());

            //DrawParticlesOnCanvas();

            //var task = await Task.Factory.StartNew(async () =>
            //                            {                                                                          
            //                                for (int i = 0; i < 360 / 30; i++)
            //                                {
            //                                    await _robotMotion.TurnRight(30);
            //                                    Thread.Sleep(3000);
            //                                    _particleFilter.ParticlesTurnRight(30);
            //                                    //_particleFilter.Resampling(_brick.Ports[InputPort.One].SIValue);
            //                                    _particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //                                    Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());
            //                                }
            //                            });
            //task.Wait();

            //DrawParticlesOnCanvas();

            //task = await Task.Factory.StartNew(async () =>
            //                            {

            //                                Thread.Sleep(5000);
            //                                await _robotMotion.DriveForward(7);
            //                                _particleFilter.MoveParticles(7);
            //                                //_particleFilter.Resampling(_brick.Ports[InputPort.One].SIValue);
            //                                _particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //                                Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());
            //                            });
            //task.Wait();

            //DrawParticlesOnCanvas();

            //task = await Task.Factory.StartNew(async () =>
            //                            {
            //                                Thread.Sleep(5000);
            //                                await _robotMotion.DriveForward(7);
            //                                _particleFilter.MoveParticles(7);
            //                                //_particleFilter.Resampling(_brick.Ports[InputPort.One].SIValue);
            //                                _particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //                                Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());

            //                            });
            //task.Wait();

            //DrawParticlesOnCanvas();


            //task = await Task.Factory.StartNew(async () =>
            //                            {
            //                                for (int i = 0; i < 360 / 30; i++)
            //                                {
            //                                    await _robotMotion.TurnRight(30);
            //                                    Thread.Sleep(3000);
            //                                    _particleFilter.ParticlesTurnRight(30);
            //                                    //_particleFilter.Resampling(_brick.Ports[InputPort.One].SIValue);
            //                                    _particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //                                    Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());
            //                                }
            //                            });
            //task.Wait();






            //foreach (var particle in _particleFilter.Particles)
            //{
            //    DrawPoint(particle.Position.X, particle.Position.Y);
            //}


            //_particleFilter.PrintParticles();
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //_particleFilter.PrintParticles();
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //_particleFilter.PrintParticles();
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            //_particleFilter.PrintParticles();
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());

            //_particleFilter.Resampling(80);

      

            //_particleFilter.ParticlesTurnLeft(90);



            //_particleFilter.ParticlesTurnLeft(90);
            //_particleFilter.Resampling(20);

            //_particleFilter.ParticlesTurnLeft(90);
            //_particleFilter.Resampling(95);

            //_particleFilter.ParticlesTurnLeft(90);
            //_particleFilter.Resampling(65);



            //_particleFilter.MoveParticles(10);
            //_particleFilter.Resampling(55);

            //_particleFilter.MoveParticles(10);
            //_particleFilter.Resampling(45);



            //_particleFilter.ParticlesTurnLeft(90);
            //_particleFilter.Resampling(30);

            //_particleFilter.MoveParticles(10);
            //_particleFilter.Resampling(20);

            //_particleFilter.MoveParticles(3);
            //_particleFilter.Resampling(17);

            //_particleFilter.ParticlesTurnLeft(90);
            //_particleFilter.Resampling(12.5);

            //_particleFilter.ParticlesTurnLeft(90);
            //_particleFilter.Resampling(108);

            //_particleFilter.ParticlesTurnLeft(90);
            //_particleFilter.Resampling(85);

            //_particleFilter.ParticlesTurnLeft(90);
            //_particleFilter.Resampling(17);


            TestParticleFilter();
            DrawParticlesOnCanvas();
            DrawLandmarks();
            DrawSimulatedRobot(20,95);

            _particleFilter.PrintParticles();
        }
        private async void OnBrickChanged(object sender, BrickChangedEventArgs e)
        {
            if (!_flag)
            {
                _flag = true;
                await StartDriving();
            }
        }

        private void DrawPoint(double x, double y, double weight)
        {
            Rectangle rect = new Rectangle { Width = _numberOfParticles*weight, Height = _numberOfParticles*weight, Fill = Brushes.Black };
            canvas.Children.Add(rect);
            Canvas.SetLeft(rect, x- (_numberOfParticles*weight)/2);
            Canvas.SetTop(rect, y - (_numberOfParticles* weight)/2);
        }

        private void DrawParticlesOnCanvas()
        {
            //canvas.Children.Clear();
            _particleFilter.Particles.OrderBy(x => x.Weight);
            Particle currentParticle;
            foreach (var particle in _particleFilter.Particles)
            {
                DrawPoint(particle.Position.X*4, particle.Position.Y*4, particle.Weight);
            }
        }

        private void DrawSimulatedRobot(int x, int y)
        {
            Rectangle rect = new Rectangle { Width = 4, Height = 4, Fill = Brushes.Red };
            canvas.Children.Add(rect);
            Canvas.SetLeft(rect, x*4-2);
            Canvas.SetTop(rect, y*4-2);
        }

        private void DrawLandmarks()
        {
            Rectangle rect1 = new Rectangle { Width = 11.5*4, Height = 3.5*4, Fill = Brushes.DarkOliveGreen };
            canvas.Children.Add(rect1);
            Canvas.SetLeft(rect1, _landmarks[0].BottomLeft.X * 4);
            Canvas.SetTop(rect1, _landmarks[0].BottomLeft.Y * 4);

            Rectangle rect2 = new Rectangle { Width = 11.5 * 4, Height = 3.5 * 4, Fill = Brushes.DarkCyan };
            canvas.Children.Add(rect2);
            Canvas.SetLeft(rect2, _landmarks[1].BottomLeft.X * 4);
            Canvas.SetTop(rect2, _landmarks[1].BottomLeft.Y * 4);

            Rectangle rect3 = new Rectangle { Width = 3.5 * 4, Height = 11.5 * 4, Fill = Brushes.DarkGoldenrod };
            canvas.Children.Add(rect3);
            Canvas.SetLeft(rect3, _landmarks[2].BottomLeft.X * 4);
            Canvas.SetTop(rect3, _landmarks[2].BottomLeft.Y * 4);
        }

        private void TestParticleFilter()
        {
            // Should return approx x: 20, y: 95, -180 degrees
            _particleFilter.Resampling(30);
            _particleFilter.ParticlesTurnRight(90);
            _particleFilter.Resampling(105);
            _particleFilter.Resampling(105);
            _particleFilter.ParticlesTurnRight(90);
            _particleFilter.Resampling(75);
            _particleFilter.Resampling(75);
            _particleFilter.ParticlesTurnRight(90);
            _particleFilter.Resampling(20);
            _particleFilter.Resampling(20);
            _particleFilter.ParticlesTurnRight(90);
            _particleFilter.Resampling(30);
            _particleFilter.Resampling(30);


            _particleFilter.MoveParticles(10);
            _particleFilter.Resampling(20);
            _particleFilter.Resampling(20);
            _particleFilter.MoveParticles(10);
            _particleFilter.Resampling(10);
            _particleFilter.Resampling(10);


            _particleFilter.ParticlesTurnRight(90);
            _particleFilter.Resampling(65.5);
            _particleFilter.Resampling(65.5);
            _particleFilter.ParticlesTurnRight(90);
            _particleFilter.Resampling(92.5);
            _particleFilter.Resampling(92.5);
            _particleFilter.ParticlesTurnRight(90);
            _particleFilter.Resampling(20);
            _particleFilter.Resampling(20);
        }
    }
}
