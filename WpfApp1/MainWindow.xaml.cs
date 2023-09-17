using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WpfCrossLightModel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TrafficLights trafficLights;
        private int[] durations;
        public MainWindow()
        {
            InitializeComponent();
            DurationBox.Visibility = Visibility.Hidden;
            trafficLights = new TrafficLights(new Ellipse[] { RedLamp, YellowLamp, GreenLamp });
            durations = new int[] { 5, 2, 5 };
        }
        private void ManualButton_Click(object sender, RoutedEventArgs e)
        {
            trafficLights.Stop();
            trafficLights.ChangeLamp();
        }
        private void AutoButton_Click(object sender, RoutedEventArgs e)
        {
            trafficLights.Stop();
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
                trafficLights.Run();
            }
        }
        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
        private void NumberValidationTextBox(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
