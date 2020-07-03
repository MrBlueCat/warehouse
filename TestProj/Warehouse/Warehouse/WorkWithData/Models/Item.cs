using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    public class Item : Entity
    {
        public DateTime Date { get; set; }

        public string Name { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public Customer Customer { get; set; }

        public bool Available { get; set; }

        public override string ToString()
        {
            return Id + "\t" +
                Date.ToShortDateString() + ", " + Date.ToShortTimeString() + "\t" +
                Name + "\t\t" +
                (Manufacturer == null ? "" : Manufacturer.Name) + "\t" +
                (Customer == null ? "" : Customer.Name);
        }

        public Item(int id, DateTime date, string name, Manufacturer manufacturer, Customer customer = null, bool available = true) : base(id)
        {
            Date = date;
            Name = name;
            Manufacturer = manufacturer;
            Customer = customer;
            Available = available;
        }

        public Item() : base(Utils.GenerateID())
        {
            Date = DateTime.UtcNow;
            Available = true;
        }
    }
}
