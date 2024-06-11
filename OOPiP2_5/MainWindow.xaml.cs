using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Windows.Threading;
using MyLib;

namespace OOPiP2_5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AlarmClock alarmClock;

        public MainWindow()
        {
            InitializeComponent();
            alarmClock = new AlarmClock();
            alarmClock.AlarmTriggered += AlarmClock_AlarmTriggered;
        }

        private void SetAlarmButton_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime.TryParse(alarmTimeTextBox.Text, out DateTime alarmTime))
            {
                if (alarmTime <= DateTime.Now)
                {
                    MessageBox.Show("Время будильника уже прошло или находится в прошлом. Будильник не активирован.");
                }
                else
                {
                    alarmClock.SetAlarm(alarmTime);
                    SetAlarmButton.IsEnabled = false;
                    alarmTimeTextBox.IsEnabled = false;
                }
            }
            else
            {
                MessageBox.Show("Неверный формат времени.");
            }
        }

        private void AlarmClock_AlarmTriggered(object sender, AlarmTriggeredEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageBox.Show($"Будильник сработал! Дата и время: {e.AlarmDateTime}");
                SetAlarmButton.IsEnabled = true;
                alarmTimeTextBox.IsEnabled = true;
            });
        }
    }
}
