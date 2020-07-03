using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    abstract public class Entity
    {
        [BsonId]
        public int Id { get; set; }

        public Entity(int id)
        {
            Id = id;
        }
    }
}
