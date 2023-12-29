using CRUD_Operations.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CRUD_Operations.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly CRUDEntities _Context;
        private DbSet<T> table;
        public GenericRepository(CRUDEntities CRUDTaskEntities6)
        {
            _Context = new CRUDEntities();
            table = _Context.Set<T>();
        }
        public void Delete(int ID)
        {
            T obj = table.Find(ID);
            table.Remove(obj);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(int TId)
        {
            return table.Find(TId);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Save()
        {
            _Context.SaveChanges();
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _Context.Entry(obj).State = EntityState.Modified;
        }
        public void UpdateToDelete(T obj)
        {
            table.Attach(obj);
            _Context.Entry(obj).State = EntityState.Modified;
        }
    }

}