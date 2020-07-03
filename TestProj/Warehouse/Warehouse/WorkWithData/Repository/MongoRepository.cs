using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Warehouse
{
    public class MongoRepository<T> : IRepository<T> where T : Entity
    {
        MongoDbContext context;
        public IMongoCollection<T> Collection { get; private set; }

        public MongoRepository(MongoDbContext context)
        {
            this.context = context;
            Collection = context.GetCollection<T>(typeof(T).Name);
        }

        public bool Delete(int id)
        {
            return Collection.FindOneAndDelete(t => t.Id == id) != null;
        }

        public T GetById(int id)
        {
            return Collection.Find(t => t.Id == id).FirstOrDefault();
        }

        public IEnumerable<T> GetAllElements()
        {
            return Collection.Find(FilterDefinition<T>.Empty).ToList();
        }

        public bool Insert(T entity)
        {
            try
            {
                Collection.InsertOne(entity);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(T entity)
        {
            var filter = Builders<T>.Filter.Eq("_id", entity.Id);
            var result = Collection.ReplaceOne(filter, entity);

            return result.IsAcknowledged;
        }

        public bool UpdateMany(Expression<Func<T, bool>> filter, UpdateDefinition<T> update)
        {
            var result = Collection.UpdateMany(filter, update);

            return result.IsAcknowledged;
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> filter)
        {
            return Collection.Find(filter).ToList();
        }

        public T FindOne(Expression<Func<T, bool>> filter)
        {
            return Collection.Find(filter).FirstOrDefault();
        }

        public bool DeleteMany(Expression<Func<T, bool>> filter)
        {
            var result = Collection.DeleteMany(filter);

            return result.IsAcknowledged; 
        }
    }
}
