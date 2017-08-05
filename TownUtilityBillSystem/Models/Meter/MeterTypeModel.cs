using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models
{
    public class MeterTypeModel
    {
        public MeterType MeterType;
        public List<MeterType> MeterTypes;
        public Utility Utility;
        public MeterTypeModel()
        {
            MeterType = new MeterType();
            MeterTypes = new List<MeterType>();
            Utility = new Utility();
        }
    }
}