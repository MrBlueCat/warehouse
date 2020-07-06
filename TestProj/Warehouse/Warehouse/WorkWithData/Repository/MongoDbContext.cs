using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MongoDB.Driver;

namespace Warehouse
{
    public class MongoDbContext
    {
        MongoClient mongoClient;
        IMongoDatabase db;

        public MongoDbContext(string connectionStr)
        {
            mongoClient = new MongoClient(connectionStr);
            //var dbName = mongoClient.ListDatabaseNames();

            //if (dbName != null)
            db = mongoClient.GetDatabase("TestBase");
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return db.GetCollection<T>(name);
        }
    }
}
