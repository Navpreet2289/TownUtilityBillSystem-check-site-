using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models
{
    public class Street
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Town Town { get; set; }
        public Index Index { get; set; }
    }
}