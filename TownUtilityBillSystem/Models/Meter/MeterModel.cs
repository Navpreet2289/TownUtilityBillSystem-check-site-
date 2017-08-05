using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models
{
    public class MeterModel
    {
        public Meter Meter;
        public List<Meter> Meters;
        public int TotalCount { get; set; }
       
        public MeterModel()
        {
            Meter = new Meter();
            Meters = new List<Meter>();
            TotalCount = 0;
        }
    }
}