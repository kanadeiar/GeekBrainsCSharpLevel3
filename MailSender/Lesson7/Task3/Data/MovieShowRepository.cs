using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Task3.Interfaces;
using Task3.Models;

namespace Task3.Data
{
    class MovieShowRepository : IRepository<MovieShow>
    {
        private readonly CinemaBoxDb _db;
        public MovieShowRepository(CinemaBoxDb db)
        {
            _db = db;
        }
        public IEnumerable<MovieShow> GetAll() => _db.Set<MovieShow>();
        public MovieShow GetById(int Id) => _db.Set<MovieShow>().Find(Id);
        public int Add(MovieShow item)
        {
            _db.Set<MovieShow>().Add(item);
            _db.SaveChanges();
            return item.Id;
        }
        public void Update(MovieShow item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public bool Delete(int Id)
        {
            var item = GetById(Id);
            if (item is null) return false;
            _db.Set<MovieShow>().Remove(item);
            _db.SaveChanges();
            return true;
        }
    }
}
