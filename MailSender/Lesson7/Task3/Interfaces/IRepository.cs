using System.Collections.Generic;
using Task3.Models.Base;

namespace Task3.Interfaces
{
    interface IRepository<T> where T : Entity
    {
        IEnumerable<T> GetAll();
        T GetById(int Id);
        int Add(T item);
        void Update(T item);
        bool Delete(int Id);
    }
}
