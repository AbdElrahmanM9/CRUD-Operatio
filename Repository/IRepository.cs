using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_Operations.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int TId);
        void Insert(T obj);
        void Update(T obj);
        void Delete(int TID);
        void Save();
        void UpdateToDelete(T obj);
    }
}