using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Warehouse
{
    public class DataBaseService<T> : IService<T> where T:Entity
    {
        protected readonly IRepository<T> repository;

        public DataBaseService(IRepository<T> itemRepository)
        {
            this.repository = itemRepository;
        }

        public bool Delete(int id)
        {
            return repository.Delete(id);
        }

        public T GetById(int id)
        {
            return repository.GetById(id);
        }

        public T GetElementBy(Expression<Func<T, bool>> filter)
        {
            return repository.FindOne(filter);
        }

        public IEnumerable<T> GetElements()
        {
            return repository.GetAllElements();
        }

        public IEnumerable<T> GetElementsBy(Expression<Func<T, bool>> filter)
        {
            return repository.FindAll(filter);
        }

        public bool Insert(T entity)
        {
            return repository.Insert(entity);
        }

        public bool Update(T entity)
        {
            return repository.Update(entity);
        }
    }
}
