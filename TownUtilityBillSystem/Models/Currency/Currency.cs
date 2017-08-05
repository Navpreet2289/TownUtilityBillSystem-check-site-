using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace TownUtilityBillSystem.Models
{
    public class Currency
    {
        private string name = "DKK";
        public string Name { get { return name; } }
        public override string ToString()
        {
            return Name;
        }
        private static Currency currency;
        public static Currency _Currency
        {
            get
            {
                if (currency == null)
                    currency = new Currency();
                return currency;
            }
        }
    }
}