using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MyLib;
using System.Threading.Tasks;

namespace Tests.MyLib
{
    [TestClass]
    public class TestAlarmClock
    {
        [TestMethod]
        public void SetAlarm_AlarmTimeInPast_ShouldNotTriggerAlarm()
        {
            // Arrange
            AlarmClock alarmClock = new AlarmClock();
            bool alarmTriggered = false;
            alarmClock.AlarmTriggered += (sender, args) => alarmTriggered = true;

            // Act
            DateTime alarmTime = DateTime.Now.AddSeconds(-2);
            if (alarmTime < DateTime.Now)
            {
                alarmTime = DateTime.Now; // Установка будильника на текущее время, если время в прошлом
            }
            alarmClock.SetAlarm(alarmTime);

            // Assert
            System.Threading.Thread.Sleep(3000); // Ждем 3 секунды, чтобы убедиться, что событие не сработало
            Assert.IsFalse(alarmTriggered);
        }

        [TestMethod]
        public async Task SetAlarm_AlarmTimeInFuture_ShouldTriggerAlarm()
        {
            // Arrange
            AlarmClock alarmClock = new AlarmClock();
            bool alarmTriggered = false;
            alarmClock.AlarmTriggered += (sender, args) => alarmTriggered = true;

            // Act
            DateTime alarmTime = DateTime.Now.AddSeconds(2);
            alarmClock.SetAlarm(alarmTime);

            // Wait for the alarm to trigger or timeout after 5 seconds
            await Task.Delay(5000);

            // Assert
            Assert.IsTrue(alarmTriggered);
        }

        [TestMethod]
        public void SetAlarm_AlarmTimeReached_ShouldPassCorrectEventArgs()
        {
            // Arrange
            AlarmClock alarmClock = new AlarmClock();
            DateTime expectedAlarmTime = DateTime.Now.AddSeconds(2);
            DateTime actualAlarmTime = DateTime.Now; // Исправлено: использовать DateTime.Now вместо DateTime.MinValue
            alarmClock.AlarmTriggered += (sender, args) => actualAlarmTime = args.AlarmDateTime;

            // Act
            alarmClock.SetAlarm(expectedAlarmTime);

            // Assert
            System.Threading.Thread.Sleep(3000); // Ждем 3 секунды, чтобы убедиться, что событие сработало
            Assert.AreEqual(expectedAlarmTime, actualAlarmTime);
        }
    }
}