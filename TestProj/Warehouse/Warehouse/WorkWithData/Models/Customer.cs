using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    public class Customer : Entity
    {
        public string Name { get; set; }

        public Customer(int id, string name) : base(id)
        {
            Name = name;
        }

        public Customer() : base(Utils.GenerateID()) { }
    }
}
