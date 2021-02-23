using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MailSender.lib.Services
{
    /// <summary> Хранилище в памяти </summary>
    public class DebugDataStorage : IServerStorage, ISenderStorage, IRecipientStorage, IMessageStorage
    {
        private ICollection<Server> _servers { get; set; }
        private ICollection<Sender> _senders { get; set; }
        private ICollection<Recipient> _recipients { get; set; }
        private ICollection<Message> _messages { get; set; }
        ICollection<Server> IStorage<Server>.Items => _servers;
        ICollection<Sender> IStorage<Sender>.Items => _senders;
        ICollection<Recipient> IStorage<Recipient>.Items => _recipients;
        ICollection<Message> IStorage<Message>.Items => _messages;
        public void Load()
        {
            _servers = Enumerable.Range(1, 10)
                .Select(i => new Server
                {
                    Id = i,
                    Address = $"smtp.server{i}.com",
                    Description = $"Тестовый сервер {i}",
                    Login = $"Password{i}",
                    Name = $"Сервер-{i}",
                    Password = $"Password-{i}".Encode(9),
                    Port = 25,
                    UseSsl = i % 2 != 0,
                }).ToList();
            _senders = Enumerable.Range(1, 10)
                .Select(i => new Sender
                {
                    Id = i,
                    Address = $"sender{i}@server{i}.com",
                    Description = $"Тестовый отправитель{i}",
                    Name = $"Отправитель{i}",
                }).ToList();
            _recipients = Enumerable.Range(1, 10)
                .Select(i => new Recipient
                {
                    Id = i,
                    Address = $"recipient{i}@server{i}.com",
                    Description = $"Тестовый получатель{i}",
                    Name = $"Получатель{i}",
                }).ToList();
            _messages = Enumerable.Range(1, 100)
                .Select(i => new Message
                {
                    Id = i,
                    Text = $"Текст сообщения {i}",
                    Subject = $"Заголовок{i}",
                }).ToList();
            Debug.WriteLine("Загрузка данных из ниоткуда прошла успешно!");
        }
        public void SaveChanges()
        {
            Debug.WriteLine("Сохраниение данных в пустоте прошло успешно!");
        }
    }
}
