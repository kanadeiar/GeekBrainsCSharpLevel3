using System.Collections.Generic;
using Task4.Model.Base;

namespace Task4.Interfaces
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
