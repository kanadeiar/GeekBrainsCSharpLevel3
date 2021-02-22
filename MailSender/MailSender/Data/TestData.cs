using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using MailSender.lib.Models;
using MailSender.lib.Services;

namespace MailSender.Data
{
    /// <summary> Тестовые данные </summary>
    public class TestData
    {
        /// <summary> Фабрика создания тестовых данных </summary>
        public static TestData CreateInstance(int countServers = 0, int countSenders = 0, int countRecipients = 0, int countMessages = 0)
        {
            return new TestData
            {
                Servers = Enumerable.Range(1, countServers)
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
                    }).ToList(),
                Senders = Enumerable.Range(1, countSenders)
                    .Select(i => new Sender
                    {
                        Id = i,
                        Address = $"sender{i}@server{i}.com",
                        Description = $"Тестовый отправитель{i}",
                        Name = $"Отправитель{i}",
                    }).ToList(),
                Recipients = Enumerable.Range(1, countRecipients)
                    .Select(i => new Recipient
                    {
                        Id = i,
                        Address = $"recipient{i}@server{i}.com",
                        Description = $"Тестовый получатель{i}",
                        Name = $"Получатель{i}",
                    }).ToList(),
                Messages = Enumerable.Range(1, countMessages)
                    .Select(i => new Message
                    {
                        Id = i,
                        Text = $"Текст сообщения {i}",
                        Subject = $"Заголовок{i}",
                    }).ToList(),
            };
        }
        public List<Server> Servers { get; set; }
        public List<Sender> Senders { get; set; }
        public List<Recipient> Recipients { get; set; }
        public List<Message> Messages { get; set; }
        private TestData()
        {
        }
        /// <summary> Загрузка данных из файла </summary>
        public static TestData LoadFromXml(string fileName)
        {
            var serializer = new XmlSerializer(typeof(TestData));
            using var reader = File.OpenText(fileName);
            var data = (TestData)serializer.Deserialize(reader);
            Debug.WriteLine($"count = {data.Servers.Count}");
            return data;
        }
        /// <summary> Сохраниение данных в файл </summary>
        public void SaveToXml(string fileName)
        {
            var serializer = new XmlSerializer(typeof(TestData));
            using var writer = File.Create(fileName);
            serializer.Serialize(writer, this);
        }
    }
}
