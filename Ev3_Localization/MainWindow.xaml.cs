using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        private RobotMotion _robotMotion;
        private RobotSensing _robotSensing;
        private ParticleFilter _particleFilter;
        //Difference in motor speed to calibrate them to drive with equal force.
        private int _motorA = 40;
        private int _motorD = 42;
        private uint _time = 5000;

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
            _particleFilter = new ParticleFilter();

            _particleFilter.GenerateParticles(100,100);
            //await _robotMotion.DriveForward();
            //await _robotMotion.DriveBackwards();
            //await _robotMotion.TurnLeft(90);
            //await _robotMotion.TurnRight(360);
            //await _robotMotion.DriveForward();
            //  await _robotMotion.TurnRight(90);
        }

    }
}
