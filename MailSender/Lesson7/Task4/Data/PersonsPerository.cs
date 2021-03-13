using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Task4.Interfaces;
using Task4.Model;

namespace Task4.Data
{
    class PersonsPerository : IRepository<Person>
    {
        private readonly PersonDB _db;
        public PersonsPerository(PersonDB db)
        {
            _db = db;
        }
        public IEnumerable<Person> GetAll() => _db.Set<Person>();
        public Person GetById(int Id) => _db.Set<Person>().Find(Id);
        public int Add(Person item)
        {
            _db.Set<Person>().Add(item);
            _db.SaveChanges();
            return item.Id;
        }
        public void Update(Person item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public bool Delete(int Id)
        {
            var item = GetById(Id);
            if (item is null) return false;
            _db.Set<Person>().Remove(item);
            _db.SaveChanges();
            return true;
        }
    }
}
