using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Warehouse
{
    public class ResponseItemModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public Customer Customer { get; set; }

        public DateTime Date { get; set; }
    }
}
