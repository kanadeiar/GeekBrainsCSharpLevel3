using System;
using System.Collections.Generic;
using MailSender.lib.Models.Base;

namespace MailSender.lib.Models
{
    /// <summary> Задание планировщика </summary>
    public class SchedulerTask : Model
    {
        public int Id { get; set; }
        private DateTime _dateTime;
        /// <summary> Время и дата задания </summary>
        public DateTime DateTime
        {
            get => _dateTime; 
            set => Set(ref _dateTime, value);
        }
        private Server _server;
        /// <summary> Данные сервера, с которого отправлять </summary>
        public Server Server
        {
            get => _server; 
            set => Set(ref _server, value);
        }
        private Sender _sender;
        /// <summary> Отправитель сообщения </summary>
        public Sender Sender
        {
            get => _sender; 
            set => Set(ref _sender, value);
        }
        private List<Recipient> _recipients;
        /// <summary> Получатели сообщения </summary>
        public List<Recipient> Recipients
        {
            get => _recipients; 
            set => Set(ref _recipients, value);
        }
        private Message _message;
        /// <summary> Сообщение отправляемое </summary>
        public Message Message
        {
            get => _message; 
            set => Set(ref _message, value);
        }
    }
}
