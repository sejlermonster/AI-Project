﻿using System;
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
        private int _simulationCounter = 0;
        private Brick _brick;
        private bool _flag;
        private double _robotGyroPreviousMeasurement;
        private double _robotDistancePreviousMeasurement;
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
        private int _numberOfParticles = 10000;
        private int _resampleCounter = 0;
        private int _xStart = 0;
        private int _yStart = 0;
        private int _xEnd = 4;
        private int _yEnd = 4;

        private AStarSearch _aStarSearch;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            //_brick = new Brick(new BluetoothCommunication("COM3"));
            //await _brick.ConnectAsync(TimeSpan.FromMilliseconds(20));
            //await _brick.DirectCommand.PlayToneAsync(100, 1000, 300);
            //_robotSensing = new RobotSensing(_brick);
            //_robotMotion = new RobotMotion(_brick, _time, _motorA, _motorD, _robotSensing);
            var world = new double[125, 125];

            _landmarks = new List<Landmark>
                         {
                             new Landmark(new Point(18.5, 98), new Point(30.5, 98), new Point(18.5, 94), new Point(30.5, 94)),
                             //new Landmark(new Point(24.5,56), new Point(36,56), new Point(24.5, 52.5), new Point(36,52.5)),
                             new Landmark(new Point(87.5, 34.5), new Point(99.5, 34.5), new Point(87.5, 30.5), new Point(99.5, 30.5)),
                             new Landmark(new Point(85, 91), new Point(89, 91), new Point(85, 79), new Point(89, 79))
                         };

            var AStarSearch = new AStarSearch(5, 5, _landmarks);
            AStarSearch.PrintMap(_xStart,_yStart,_xEnd,_yEnd);
            AStarSearch.FindPath(_xStart, _yStart, _xEnd, _yEnd);
            _particleFilter = new ParticleFilter(_landmarks, world);

            _particleFilter.GenerateParticles(_numberOfParticles, world.GetLength(0), world.GetLength(1));

            //_aStarSearch = new AStarSearch(world.GetLength(0), world.GetLength(1), _landmarks);
            //_aStarSearch.PrintMap();

            // When everything is setup initialize
            // _brick.BrickChanged += OnBrickChanged;

            //TestParticleFilter();
            //Dispatcher.Invoke(DrawParticlesOnCanvas);
            //Dispatcher.Invoke(DrawLandmarks);
            //DrawSimulatedRobot(20,95);
            //_particleFilter.PrintParticles();

        }

        private async Task StartDriving()
        {

            //await StartLocalization();
            TestParticleFilter();
            Dispatcher.Invoke(DrawParticlesOnCanvas);
            Dispatcher.Invoke(DrawLandmarks);
            //DrawSimulatedRobot(20,95);

            _particleFilter.PrintParticles();
        }

        private async Task StartLocalization()
        {
            Dispatcher.Invoke(DrawLandmarks);
            Dispatcher.Invoke(DrawParticlesOnCanvas);

            Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            _particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());


            for (int i = 0; i < 360 / 90; i++)
            {
                await _robotMotion.TurnRight(90);
                _particleFilter.ParticlesTurnRight(90);
                //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
                _particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
                Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());
            }

            Dispatcher.Invoke(DrawParticlesOnCanvas);

            await _robotMotion.DriveForward(20);
            _particleFilter.MoveParticles(20);
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            _particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());

            Dispatcher.Invoke(DrawParticlesOnCanvas);

            
            for (int i = 0; i < 360 / 90; i++)
            {
                await _robotMotion.TurnRight(90);
                _particleFilter.ParticlesTurnRight(90);
                //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
                _particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
                Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());
            }

            Dispatcher.Invoke(DrawParticlesOnCanvas);


            await _robotMotion.DriveForward(20);
            _particleFilter.MoveParticles(20);
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            _particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());

            Dispatcher.Invoke(DrawParticlesOnCanvas);

            for (int i = 0; i < 360 / 90; i++)
            {
                await _robotMotion.TurnRight(90);
                _particleFilter.ParticlesTurnRight(90);
                //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
                _particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
                Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());
            }

            Dispatcher.Invoke(DrawParticlesOnCanvas);

   
        }

        private async void OnBrickChanged(object sender, BrickChangedEventArgs e)
        {
            var gyroMeasuremnt = _robotSensing.GetGyroData();


            if (!_flag && gyroMeasuremnt != 0)
            {
                _flag = true;
               // Task.Factory.StartNew(() => StartDriving());
            }
        }

        private void DrawPoint(double x, double y, double weight)
        {
            Rectangle rect = new Rectangle
            {
                Width = 2,
                Height = 2,
                Fill = Brushes.Black
            };
            canvas.Children.Add(rect);
            Canvas.SetLeft(rect, x*4 - 1);
            Canvas.SetTop(rect, y*4 - 1);
            //Rectangle rect = new Rectangle
            //{
            //    Width = weight/25,
            //    Height = weight/25,
            //    Fill = Brushes.Black
            //};
            //canvas.Children.Add(rect);
            //Canvas.SetLeft(rect, x-rect.Width/2);
            //Canvas.SetTop(rect, y-rect.Height/2);
        }

        private void DrawParticlesOnCanvas()
        {
            canvas.Children.Clear();
            foreach (var particle in _particleFilter.Particles)
            {
                DrawPoint(particle.Position.X, particle.Position.Y, 1);
            }
            //var particleList = new List<Pair<Particle, int>>();
            //foreach (var particle in _particleFilter.Particles)
            //{
            //    try
            //    {
            //        var uniqueParticle = particleList.Where(x => x.Item1.Position.X == particle.Position.X).Where(x => x.Item1.Position.Y == particle.Position.Y).Where(x => x.Item1.OrientationInRadians == particle.OrientationInRadians).Select(x => x.Item2 = x.Item2 + 1);
            //        if(uniqueParticle.ToList().Count == 0)
            //            particleList.Add(new Pair<Particle, int>(particle, 1));
            //    }
            //    catch (Exception)
            //    {
            //        particleList.Add(new Pair<Particle, int>(particle, 1));
            //    }
            //}

            //foreach (var tuple in particleList)
            //{
            //    DrawPoint(tuple.Item1.Position.X * 4, tuple.Item1.Position.Y * 4, tuple.Item2);

            //}
        }

        private void DrawSimulatedRobot(int x, int y)
        {
            Rectangle rect = new Rectangle { Width = 6, Height = 6, Fill = Brushes.Red };
            canvas.Children.Add(rect);
            Canvas.SetLeft(rect, x * 4 - 3);
            Canvas.SetTop(rect, y * 4 - 3);

            Rectangle rectA = new Rectangle { Width = 2, Height = 14, Fill = Brushes.Red };
            canvas.Children.Add(rectA);
            Canvas.SetLeft(rectA, x * 4 - 1);
            Canvas.SetTop(rectA, y * 4 - 1);
        }

        private void DrawLandmarks()
        {
            Rectangle rect1 = new Rectangle { Width = 11.5 * 4, Height = 3.5 * 4, Fill = Brushes.DarkOliveGreen };
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
         
        }

        private async void Button_Click_forward(object sender, RoutedEventArgs e)
        {
            await _robotMotion.DriveForward(10);
            _particleFilter.MoveParticles(10);
            //_particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            _particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());
            Dispatcher.Invoke(DrawLandmarks);
            Dispatcher.Invoke(DrawParticlesOnCanvas);
        }

        private async void Button_Click_backward(object sender, RoutedEventArgs e)
        {

        }

        private async void Button_Click_right(object sender, RoutedEventArgs e)
        {
            await _robotMotion.TurnRight(90);
            _particleFilter.ParticlesTurnRight(90);
            _particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            _particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
            Dispatcher.Invoke(DrawLandmarks);
            Dispatcher.Invoke(DrawParticlesOnCanvas);
        }

        private async void Button_Click_left(object sender, RoutedEventArgs e)
        {

        }
        private async void Button_Click_scan(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 360 / 90; i++)
            {
                await _robotMotion.TurnRight(90);
                _particleFilter.ParticlesTurnRight(90);
                _particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
                _particleFilter.Resampling(_robotSensing.GetDistanceInCentimeters());
                Debug.WriteLine(_robotSensing.GetDistanceInCentimeters());
            }
            Dispatcher.Invoke(DrawLandmarks);
            Dispatcher.Invoke(DrawParticlesOnCanvas);
        }
        private async void Button_Click_simulate(object sender, RoutedEventArgs e)
        {
            // Should return approx x: 20, y: 95, -180 degrees
            if (_simulationCounter == 0)
            {
                Dispatcher.Invoke(DrawParticlesOnCanvas);
                Dispatcher.Invoke(DrawLandmarks);
                DrawSimulatedRobot(23, 64);
            }

            if (_simulationCounter == 1)
            {
                //SCAN.. taking measuremnts from 360
                _particleFilter.Resampling(30);
                _particleFilter.Resampling(30);

                _particleFilter.ParticlesTurnRight(90);
                _particleFilter.Resampling(102);
                _particleFilter.Resampling(102);

                _particleFilter.ParticlesTurnRight(90);
                _particleFilter.Resampling(64);
                _particleFilter.Resampling(64);

                _particleFilter.ParticlesTurnRight(90);
                _particleFilter.Resampling(20);
                _particleFilter.Resampling(20);

                _particleFilter.ParticlesTurnRight(90);
                _particleFilter.Resampling(30);
                _particleFilter.Resampling(30);
                DrawParticlesOnCanvas();
                Dispatcher.Invoke(DrawLandmarks);
                DrawSimulatedRobot(23, 64);
            }

            if (_simulationCounter == 2)
            {
                _particleFilter.MoveParticles(10);
                _particleFilter.Resampling(20);
                _particleFilter.Resampling(20);
                //Dispatcher.Invoke(DrawParticlesOnCanvas);
                DrawParticlesOnCanvas();
                Dispatcher.Invoke(DrawLandmarks);
                DrawSimulatedRobot(23, 74);
            }

            if (_simulationCounter == 3)
            {
                _particleFilter.MoveParticles(10);
                _particleFilter.Resampling(10);
                _particleFilter.Resampling(10);
                DrawParticlesOnCanvas();
                Dispatcher.Invoke(DrawLandmarks);
                DrawSimulatedRobot(23, 84);
            }

            if (_simulationCounter == 4)
            {
                // SCAN
                _particleFilter.ParticlesTurnRight(90);
                _particleFilter.Resampling(62);
                _particleFilter.Resampling(62);

                _particleFilter.ParticlesTurnRight(90);
                _particleFilter.Resampling(84);
                _particleFilter.Resampling(84);


                _particleFilter.ParticlesTurnRight(90);
                _particleFilter.Resampling(20);
                _particleFilter.Resampling(20);
                DrawParticlesOnCanvas();
                Dispatcher.Invoke(DrawLandmarks);
                DrawSimulatedRobot(23, 84);
                Console.WriteLine("Simulation Done");
            }


            _simulationCounter++;
            //if (_simulationCounter > 4)
            //    _simulationCounter = 0;
        }
    }
}
