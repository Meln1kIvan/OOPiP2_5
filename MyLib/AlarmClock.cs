using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;

namespace MyLib
{
    public delegate void AlarmTriggeredEventHandler(object sender, AlarmTriggeredEventArgs e);

    public class AlarmTriggeredEventArgs : EventArgs
    {
        public DateTime AlarmDateTime { get; }

        public AlarmTriggeredEventArgs(DateTime alarmDateTime)
        {
            AlarmDateTime = alarmDateTime;
        }
    }

    public class AlarmClock
    {
        public event AlarmTriggeredEventHandler AlarmTriggered;

        public void SetAlarm(DateTime alarmDateTime)
        {
            var currentDateTime = DateTime.Now;


            var timeSpan = alarmDateTime - currentDateTime;
            var timer = new DispatcherTimer
            {
                Interval = timeSpan
            };
            timer.Tick += (sender, e) =>
            {
                timer.Stop();
                OnAlarmTriggered(new AlarmTriggeredEventArgs(alarmDateTime));
            };
            timer.Start();
        }

        protected virtual void OnAlarmTriggered(AlarmTriggeredEventArgs e)
        {
            AlarmTriggered?.Invoke(this, e);
        }
    }
}
