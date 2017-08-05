using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models
{
    public class FlatPart
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public float Square { get; set; }
        public Building Building { get; set; }
    }
}