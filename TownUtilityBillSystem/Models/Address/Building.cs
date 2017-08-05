using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models
{
    public class Building
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public float Square { get; set; }
        public Street Street { get; set; }
        public string ImagePath { get; set; }
    }
}