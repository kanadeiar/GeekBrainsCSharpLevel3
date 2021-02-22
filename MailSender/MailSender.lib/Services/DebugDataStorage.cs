using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MailSender.lib.Services
{
    public class DebugDataStorage : IServerStorage, ISenderStorage, IRecipientStorage, IMessageStorage
    {
        public ICollection<Server> Servers { get; set; }
        public ICollection<Sender> Senders { get; set; }
        public ICollection<Recipient> Recipients { get; set; }
        public ICollection<Message> Messages { get; set; }
        ICollection<Server> IStorage<Server>.Items => Servers;
        ICollection<Sender> IStorage<Sender>.Items => Senders;
        ICollection<Recipient> IStorage<Recipient>.Items => Recipients;
        ICollection<Message> IStorage<Message>.Items => Messages;
        public void Load()
        {
            Servers = Enumerable.Range(1, 10)
                .Select(i => new Server
                {
                    Id = i,
                    Address = $"smtp.server{i}.com",
                    Description = $"Тестовый сервер {i}",
                    Login = $"Password{i}",
                    Name = $"Сервер-{i}",
                    Password = TextEncoder.Encode($"Password-{i}", 9),
                    Port = 25,
                    UseSsl = i % 2 != 0,
                }).ToList();
            Senders = Enumerable.Range(1, 10)
                .Select(i => new Sender
                {
                    Id = i,
                    Address = $"sender{i}@server{i}.com",
                    Description = $"Тестовый отправитель{i}",
                    Name = $"Отправитель{i}",
                }).ToList();
            Recipients = Enumerable.Range(1, 10)
                .Select(i => new Recipient
                {
                    Id = i,
                    Address = $"recipient{i}@server{i}.com",
                    Description = $"Тестовый получатель{i}",
                    Name = $"Получатель{i}",
                }).ToList();
            Messages = Enumerable.Range(1, 100)
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
