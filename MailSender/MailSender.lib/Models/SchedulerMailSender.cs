using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using MailSender.lib.Interfaces;
using MailSender.lib.Models.Base;

namespace MailSender.lib.Models
{
    /// <summary> Задание на рассылку почты </summary>
    public class SchedulerMailSender : Entity, ISchedulerMailSender
    {
        private Timer _timer;
        private IMailSender _mailSender;
        /// <summary> Событие - письмо отправлено </summary>
        public event EventHandler EmailSended;
        /// <summary> Время отправления письма </summary>
        public DateTime DateTimeSend { get; private set; }
        /// <summary> Адрес отправителя </summary>
        public string From { get; private set; }
        /// <summary> Адрес получателя </summary>
        public IEnumerable<string> Tos { get; private set; }
        /// <summary> Тема письма </summary>
        public string Subject { get; private set; }
        /// <summary> Текст письма </summary>
        public string Text { get; private set; }
        public SchedulerMailSender(IMailSender mailSender)
        {
            _mailSender = mailSender;
        }
        /// <summary> Установка задания отправки письма </summary>
        /// <param name="DateTimeSend">дата и время</param>
        /// <param name="from">отправитель</param>
        /// <param name="to">получатель</param>
        /// <param name="subject">заголовок</param>
        /// <param name="text">сообщение</param>
        public void AddTaskSend(DateTime DateTimeSend, string from, IEnumerable<string> tos, string subject, string text)
        {
            this.DateTimeSend = DateTimeSend;
            From = from;
            Tos = tos;
            Subject = subject;
            Text = text;
            _timer = new Timer(1000);
            _timer.Elapsed += (_, _) =>
            {
                if (DateTime.Now > this.DateTimeSend)
                {
                    _mailSender.Send(From, Tos, Subject, Text);
                    _timer.Stop();
                    EmailSended?.Invoke(null,null);
                    #if DEBUG
                    Debug.WriteLine($"Отправлено запланированное на {DateTimeSend} письмо.");
                    #endif
                }
            };
            _timer.Start();
        }
        /// <summary> Отмена отправки </summary>
        public void Stop()
        {
            _timer.Stop();
        }
    }
}
