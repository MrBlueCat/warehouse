using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Warehouse
{
    public interface IRepository<T> where T : Entity
    {
        T GetById(int id);
        IEnumerable<T> GetAllElements();
        IEnumerable<T> FindAll(Expression<Func<T, bool>> filter);
        T FindOne(Expression<Func<T, bool>> filter);
        bool Insert(T entity);
        bool Delete(int id);
        bool Update(T entity);
        bool UpdateMany(Expression<Func<T, bool>> filter, UpdateDefinition<T> update);
        bool DeleteMany(Expression<Func<T, bool>> filter);
    }
}
