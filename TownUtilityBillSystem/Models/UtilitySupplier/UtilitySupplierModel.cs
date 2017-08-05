using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models
{
    public class UtilitySupplierModel
    {
        private UtilitySupplier utilitySupplier;

        public UtilitySupplier UtilitySupplier
        {
            get
            {
                return utilitySupplier;
            }
        }


        public UtilitySupplierModel()
        {
            utilitySupplier = UtilitySupplier.Instance;
        }
    }
}