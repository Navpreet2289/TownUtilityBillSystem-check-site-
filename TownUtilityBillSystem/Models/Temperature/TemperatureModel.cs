using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models
{
    public class TemperatureModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }       
        public float AverageValue { get; set; }       
        public string MonthName { get; set; }
    }
}