using System.Collections.Generic;
using System.Linq;
using MailSender.lib.Interfaces;
using MailSender.lib.Models.Base;

namespace MailSender.Data.Stores.InMemory.Base
{
    /// <summary> Базовый класс для репозиториев в памяти </summary>
    /// <typeparam name="T">сущность</typeparam>
    public abstract class RepositoryInMem<T> : IRepository<T> where T : Entity
    {
        private readonly List<T> _items;
        private int _maxId; //макс ид уже находящегося в коллекции элемента
        public RepositoryInMem(IEnumerable<T> items)
        {
            _items = items.ToList();
            if (_items.Any())
                _maxId = _items.Max(i => i.Id);
            else
                _maxId = 0;
        }
        public IEnumerable<T> GetAll() => _items;
        public T GetById(int id) => _items.FirstOrDefault(i => i.Id == id);
        public int Add(T item)
        {
            if (_items.Contains(item))
                return item.Id;
            item.Id = ++_maxId;
            _items.Add(item);
            return item.Id;
        }
        public abstract void Update(T item);
        public bool Remove(int id) => _items.RemoveAll(i => i.Id == id) > 0;
    }
}
