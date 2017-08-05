using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models
{
    public class BillModel
    {
        public Bill Bill;
        public List<Bill> Bills;
        public CustomerModel CustomerModel;
        public Currency Currency;
        public UtilitySupplierModel UtilitySupplierModel;
        public List<ChartData> UtilityChargesChartData;
        public List<TemperatureModel> Temperatures;
        public string TempearatureIconPath { get; } = CustomizedMethod.GetTemperatureIconImage();

        public BillModel()
        {
            Bill = new Bill();
            Bills = new List<Bill>();
            CustomerModel = new CustomerModel();
            Currency = new Currency();
            UtilitySupplierModel = new UtilitySupplierModel();
            UtilityChargesChartData = new List<ChartData>();
            Temperatures = new List<TemperatureModel>();            
        }
    }
}