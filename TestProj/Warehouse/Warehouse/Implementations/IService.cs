using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Warehouse
{
    public interface IService<T> where T : Entity
    {
        T GetById(int id);

        IEnumerable<T> GetElements();

        IEnumerable<T> GetElementsBy(Expression<Func<T, bool>> filter);

        T GetElementBy(Expression<Func<T, bool>> filter);

        bool Insert(T entity);

        bool Delete(int id);

        bool Update(T entity);
    }
}