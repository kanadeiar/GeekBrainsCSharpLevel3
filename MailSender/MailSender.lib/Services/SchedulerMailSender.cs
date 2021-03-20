using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using MailSender.lib.Interfaces;
using MailSender.lib.Models;

namespace MailSender.lib.Services
{
    /// <summary> Задание на рассылку почты </summary>
    public class SchedulerMailSender : ISchedulerMailSender
    {
        private Timer _timer;
        private readonly IMailSender _mailSender;
        /// <summary> Событие - письмо отправлено </summary>
        public event EventHandler MissionCompleted;
        /// <summary> Данные планировщика </summary>
        public Scheduler Scheduler { get; set; }

        public SchedulerMailSender(IMailSender mailSender)
        {
            _mailSender = mailSender;
        }
        /// <summary> Старт работы планировщика отправки почты </summary>


        public void Start(Scheduler Scheduler)
        {
            this.Scheduler = Scheduler;
            _timer = new Timer(1000);
            _timer.Elapsed += TimerOnElapsed;
            _timer.Start();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            if (DateTime.Now > Scheduler.DateTimeSend)
            {
                _mailSender.Send(Scheduler.Sender.Address, Scheduler.Recipients.Select(r => r.Address), Scheduler.Message.Subject, Scheduler.Message.Text);
                _timer.Stop();
                MissionCompleted?.Invoke(null, null);
#if DEBUG
                Debug.WriteLine($"Отправлено запланированное на {Scheduler.DateTimeSend} письмо.");
#endif
            }
        }
        /// <summary> Отмена отправки </summary>
        public void Stop()
        {
            _timer.Stop();
        }
    }
}
