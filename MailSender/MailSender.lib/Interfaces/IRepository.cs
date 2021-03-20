using System.Collections.Generic;
using MailSender.lib.Models.Base;

namespace MailSender.lib.Interfaces
{
    /// <summary> Базовый класс репозитория объектов-сущностей </summary>
    /// <typeparam name="T">Сущность</typeparam>
    public interface IRepository<T> where T : Entity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        int Add(T item);
        void AddRange(IEnumerable<T> items);
        void Update(T item);
        bool Delete(int id);
        void RemoveRange(IEnumerable<T> items);
    }
}
