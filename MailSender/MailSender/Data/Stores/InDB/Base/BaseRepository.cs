using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.lib.Interfaces;
using MailSender.lib.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace MailSender.Data.Stores.InDB.Base
{
    public class BaseRepository<T> : IRepository<T> where T : Entity
    {
        private readonly MailSenderDb _db;
        private DbSet<T> Set { get; }
        public BaseRepository(MailSenderDb db)
        {
            _db = db;
            Set = db.Set<T>();
        }
        public IEnumerable<T> GetAll() => Set;
        public T GetById(int id) => Set.Find(id);
        public int Add(T item)
        {
            Set.Add(item);
            _db.SaveChanges();
            return item.Id;
        }
        public void Update(T item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public bool Remove(int id)
        {
            var item = GetById(id);
            if (item is null) return false;
            Set.Remove(item);
            _db.SaveChanges();
            return true;
        }
    }
}
