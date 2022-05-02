using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace UoW.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null); // получение всех объектов

        T Get(int id); // получение одного объекта по id
        int Count();

        void Add(T item); // создание объекта
        void Add(params T[] item); // создание объекта
        void Update(T item); // обновление объекта

        void Delete(Expression<Func<T, bool>> predicate);
        void Delete(T entityToDelete); // удаление объект
        void Delete(int id); // удаление объекта по id
    }
}
