using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Task3.Interfaces;
using Task3.Models;

namespace Task3.Data
{
    class OrdersReporitory : IRepository<Order>
    {
        private readonly CinemaBoxDb _db;
        public OrdersReporitory(CinemaBoxDb db)
        {
            _db = db;
        }
        public IEnumerable<Order> GetAll() => _db.Set<Order>();
        public Order GetById(int Id) => _db.Set<Order>().Find(Id);
        public int Add(Order item)
        {
            _db.Set<Order>().Add(item);
            _db.SaveChanges();
            return item.Id;
        }
        public void Update(Order item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public bool Delete(int Id)
        {
            var item = GetById(Id);
            if (item is null) return false;
            _db.Set<Order>().Remove(item);
            _db.SaveChanges();
            return true;
        }
    }
}
