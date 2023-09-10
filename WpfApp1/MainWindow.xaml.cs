using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfCrossLightModel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TrafficLights trafficLights;
        private DispatcherTimer timer;
        private int[] durations;
        public MainWindow()
        {
            InitializeComponent();
            DurationBox.Visibility = Visibility.Hidden;
            trafficLights = new TrafficLights(new Ellipse[] { RedLamp, YellowLamp, GreenLamp });
            durations = new int[] { 5, 2, 5 };
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(trafficLights.GetDuration());
            timer.Tick += timer_Tick;
        }

        private void ManualButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            trafficLights.ChangeLamp();
        }

        private void AutoButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            DurationBox.Visibility = Visibility.Visible;
            RedTextBox.Focus();
        }

        private void ReadyButton_Click(object sender, RoutedEventArgs e)
        {
            if (GetNumber(RedTextBox, out durations[0])
                && GetNumber(YellowTextBox, out durations[1])
                && GetNumber(GreenTextBox, out durations[2]))
            {
                trafficLights.SetDurations(durations);
                DurationBox.Visibility = Visibility.Hidden;
                timer.Start();
            }
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            trafficLights.ChangeLamp();
            timer.Interval = TimeSpan.FromSeconds(trafficLights.GetDuration());
        }
        private bool GetNumber(TextBox box, out int number)
        {
            try
            {
                number = int.Parse(box.Text);
                return true;
            }
            catch
            {
                MessageBox.Show($"An integer expected: duration in seconds,\nbut you had input \"{box.Text}\"\nIt will be corrected to \"1\"",
                    "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                box.Text = "1";
                number = 1;
                box.Focus();
            }
            return false;
        }
    }
}
