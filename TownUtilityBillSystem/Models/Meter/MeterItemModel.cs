using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models
{
    public class MeterItemModel
    {
        public List<MeterItem> MeterItems;
        public Meter Meter;
        public MeterItem MeterItem;
        public string Period { get; set; }

        public float Value { get; set; }

        public MeterItemModel()
        {
            MeterItems = new List<MeterItem>();
            Meter = new Meter();
            MeterItem = new MeterItem();
            Period = "";
            Value = 0;
        }
    }
}