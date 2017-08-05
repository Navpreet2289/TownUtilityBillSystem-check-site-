using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models
{
    public class CurrencyModel
    {
        private Currency currency;
        public Currency Currency
        {
            get
            {
                return currency;
            }
        }
        public CurrencyModel()
        {
            currency = Currency._Currency;
        }
    }
}