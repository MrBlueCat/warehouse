using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    public class RequestItemModel
    {
        public string Name { get; set; }

        public int? ManufacturerId { get; set; }

        public int? CustomerId { get; set; }
    }
}
