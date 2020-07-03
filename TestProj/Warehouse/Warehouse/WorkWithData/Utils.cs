using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    public class Utils
    {
        public static int GenerateID()
        {
            return new Random().Next(0, Int32.MaxValue);
        }
    }
}
