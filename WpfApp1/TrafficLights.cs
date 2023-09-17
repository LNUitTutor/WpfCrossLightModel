using System;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfCrossLightModel
{
    class TrafficLights
    {
        private const int countOfLamps = 4;
        private Lamp[] lamps;
        private int activeLamp;
        private DispatcherTimer timer;

        public TrafficLights(Ellipse[] views, int activeNo = 1)
        {
            activeLamp = activeNo;
            lamps = new Lamp[countOfLamps]
            {
                new Lamp(views[0], Brushes.Red, 5),
                new Lamp(views[1], Brushes.Yellow, 2),
                new Lamp(views[2], Brushes.Green, 5),
                null
            };
            lamps[3] = lamps[1];
            lamps[activeLamp].SwitchOn();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(GetDuration());
            timer.Tick += timer_Tick;
        }
        public void ChangeLamp()
        {
            lamps[activeLamp].SwitchOff();
            activeLamp = ++activeLamp % countOfLamps;
            lamps[activeLamp].SwitchOn();
        }
        public int GetDuration() => lamps[activeLamp].Duration;
        public void SetDurations(int[] durations)
        {
            for (int i = 0; i < countOfLamps - 1; ++i) lamps[i].Duration = durations[i];
        }
        public void Run() => timer.Start();
        public void Stop() => timer.Stop();
        private void timer_Tick(object sender, EventArgs e)
        {
            ChangeLamp();
            timer.Interval = TimeSpan.FromSeconds(GetDuration());
        }
    }
}
