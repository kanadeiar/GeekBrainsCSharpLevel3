using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using MailSender.lib.Interfaces;
using MailSender.lib.Models;

namespace MailSender.lib.Services
{
    /// <summary> Хранилище в файле XML </summary>
    public class XmlFileDataStorage : IServerStorage, ISenderStorage, IRecipientStorage, IMessageStorage
    {
        private readonly string _fileName;
        private DataFileStruct _data = DataFileStruct.CreateInstance(10);
        ICollection<Server> IStorage<Server>.Items => _data.Servers;
        ICollection<Sender> IStorage<Sender>.Items => _data.Senders;
        ICollection<Recipient> IStorage<Recipient>.Items => _data.Recipients;
        ICollection<Message> IStorage<Message>.Items => _data.Messages;
        public XmlFileDataStorage(string fileName)
        {
            _fileName = fileName;
        }
        public void Load()
        {
            if (!File.Exists(_fileName))
            {
                _data = DataFileStruct.CreateInstance(10);
                return;
            }
            using var reader = File.OpenText(_fileName);
            if (reader.BaseStream.Length == 0)
            {
                _data = DataFileStruct.CreateInstance(10);
                return;
            }
            var serializer = new XmlSerializer(typeof(DataFileStruct));
            _data = (DataFileStruct) serializer.Deserialize(reader);
        }
        public void SaveChanges()
        {
            using var writer = File.CreateText(_fileName);
            var serializer = new XmlSerializer(typeof(DataFileStruct));
            serializer.Serialize(writer, _data);
        }
        public class DataFileStruct
        {
            public List<Server> Servers { get; set; }
            public List<Sender> Senders { get; set; }
            public List<Recipient> Recipients { get; set; } 
            public List<Message> Messages { get; set; }
            private DataFileStruct()
            {
            }
            public static DataFileStruct CreateInstance(int count = 0)
            {
                return new DataFileStruct
                {
                    Servers = Enumerable.Range(1, count)
                        .Select(i => new Server
                        {
                            Id = i,
                            Address = $"smtp.server{i}.com",
                            Description = $"Тестовый сервер {i}",
                            Login = $"Password{i}",
                            Name = $"Сервер-{i}",
                            Password = $"Password-{i}".Rfc2898Encode(),
                            Port = 25,
                            UseSsl = i % 2 != 0,
                        }).ToList(),
                    Senders = Enumerable.Range(1, count)
                        .Select(i => new Sender
                        {
                            Id = i,
                            Address = $"sender{i}@server{i}.com",
                            Description = $"Тестовый отправитель{i}",
                            Name = $"Отправитель{i}",
                        }).ToList(),
                    Recipients = Enumerable.Range(1, count)
                        .Select(i => new Recipient
                        {
                            Id = i,
                            Address = $"recipient{i}@server{i}.com",
                            Description = $"Тестовый получатель{i}",
                            Name = $"Получатель{i}",
                        }).ToList(),
                    Messages = Enumerable.Range(1, count * 10)
                        .Select(i => new Message
                        {
                            Id = i,
                            Text = $"Текст сообщения {i}",
                            Subject = $"Заголовок{i}",
                        }).ToList(),
                };
            }
        }
    }
}
