using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownUtilityBillSystem.Models;
using System.Globalization;

namespace TownUtilityBillSystem.Models
{
    public class UtilityModel
    {
        public Utility Utility;
        public Unit Unit;
        public List<Utility> Utilities { get; set; }
        public List<Unit> Units { get; set; }
        public Currency Currency;
        public int MeterQty;
               
        public UtilityModel()
        {
            Utilities = new List<Utility>();
            Units = new List<Unit>();
            Utility = new Utility();
            Unit = new Unit();
            Currency = new Currency();
            MeterQty = 0;
        }
    }
}