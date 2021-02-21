using System.Collections.Generic;
using System.Linq;
using MailSender.Models;

namespace MailSender.Data
{
    /// <summary> Тестовые данные </summary>
    internal static class TestData
    {
        public static List<Server> Servers { get; } = Enumerable.Range(1, 10)
            .Select(i => new Server
        {
            Id = i,
            Address = $"smtp.server{i}.com",
            Description = $"Тестовый сервер {i}",
            Login = $"Password{i}",
            Name = $"Сервер-{i}",
            Password = $"Password-{i}",
            Port = 25,
            UseSsl = i % 2 != 0,
        }).ToList();
        public static List<Sender> Senders { get; } = Enumerable.Range(1, 10)
            .Select(i => new Sender
            {
                Id = i,
                Address = $"sender{i}@server{i}.com",
                Description = $"Тестовый отправитель{i}",
                Name = $"Отправитель{i}",
            }).ToList();
        public static List<Recipient> Recipients { get; } = Enumerable.Range(1, 10)
            .Select(i => new Recipient
            {
                Id = i,
                Address = $"recipient{i}@server{i}.com",
                Description = $"Тестовый получатель{i}",
                Name = $"Получатель{i}",
            }).ToList();
        public static List<Message> Messages { get; set; } = Enumerable.Range(1, 100)
            .Select(i => new Message
            {
                Id = i,
                Text = $"Текст сообщения {i}",
                Subject = $"Заголовок{i}",
            }).ToList();
    }
}
