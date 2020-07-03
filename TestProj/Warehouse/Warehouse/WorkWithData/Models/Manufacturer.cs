using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    public class Manufacturer : Entity
    {
        public string Name { get; set; }

        public Manufacturer(int id, string name) : base(id)
        {
            Name = name;
        }

        public Manufacturer() : base(Utils.GenerateID()) { }
    }
}
