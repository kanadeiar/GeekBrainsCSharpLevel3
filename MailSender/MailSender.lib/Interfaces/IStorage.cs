using System.Collections.Generic;
using MailSender.lib.Models;

namespace MailSender.lib.Interfaces
{
    /// <summary> Хранилище данных </summary>
    public interface IStorage<T>
    {
        ICollection<T> Items { get; }
        /// <summary> Загрузить </summary>
        void Load();
        /// <summary> Сохранить </summary>
        void SaveChanges();
    }

    public interface IServerStorage : IStorage<Server> {}
    public interface ISenderStorage : IStorage<Sender> {}
    public interface IRecipientStorage : IStorage<Recipient> {}
    public interface IMessageStorage : IStorage<Message> {}
}
