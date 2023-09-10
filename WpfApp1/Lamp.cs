using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfCrossLightModel
{
    class Lamp
    {
        private Ellipse view;
        private SolidColorBrush brush;
        private bool switchedOn;

        public int Duration { get; set; }
        public Lamp(Ellipse ellipse, SolidColorBrush color, int duration)
        {
            view = ellipse;
            brush = color;
            Duration = duration;
            switchedOn = false;
        }
        public bool IsSwitchedOn => switchedOn;
        public Lamp SwitchOn()
        {
            switchedOn = true;
            view.Fill = brush;
            return this;
        }
        public Lamp SwitchOff()
        {
            switchedOn = false;
            view.Fill = Brushes.LightGray;
            return this;
        }

    }
}
