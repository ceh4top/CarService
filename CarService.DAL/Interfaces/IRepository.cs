using System;
using System.Collections.Generic;

namespace CarService.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T item);
        void Update(T item);
        void Delete(T item);
        void Delete(Func<T, Boolean> predicate);
        List<T> GetAll();
        List<T> Find(Func<T, Boolean> predicate);
        List<TResult> Select<TResult>(Func<T, TResult> selector);
    }
}
