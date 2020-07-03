using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Warehouse
{
    public class UpdateItemModel: RequestItemModel
    {
        public bool Available { get; set; }
    }
}
