using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using CarService.DAL.Interfaces;
using CarService.DAL.EF;

namespace CarService.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private Context DB;

        public Repository(Context DB)
        {
            this.DB = DB;
        }

        public void Create(T item)
        {
            this.DB.Set<T>().Add(item);
        }

        public void Update(T item)
        {
            this.DB.Entry(item).State = EntityState.Modified;
        }

        public void Delete(T item)
        {
            this.DB.Set<T>().Remove(item);
        }

        public void Delete(Func<T, bool> predicate)
        {
            foreach (T item in this.DB.Set<T>().Where(predicate))
                this.DB.Set<T>().Remove(item);
        }

        public List<T> GetAll()
        {
            return this.DB.Set<T>().ToList();
        }

        public List<T> Find(Func<T, bool> predicate)
        {
            return this.DB.Set<T>().Where(predicate).ToList();
        }

        public List<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            return this.DB.Set<T>().Select(selector).ToList();
        }
    }
}
