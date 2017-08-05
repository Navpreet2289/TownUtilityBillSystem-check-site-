using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models
{
    public class CalculatorItemModel
    {
        public int Id { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The usage can not be nagative")]
        [Display(Name = "Electricity")]
        public float ElectricUsage { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The usage can not be nagative")]
        [Display(Name = "Water")]
        public float WaterUsage { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The usage can not be nagative")]
        [Display(Name = "Heating")]
        public float HeatUsage { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The usage can not be nagative")]
        [Display(Name = "Gas")]
        public float GasUsage { get; set; }

        public float ElCharges { get; set; }
        public float WaterCharges { get; set; }
        public float HeatCharges { get; set; }
        public float GasCharges { get; set; }

        public float TotalCharges { get; set; }

        public List<Utility> Utilities { get; set; }
        public Currency Currency;

        public CalculatorItemModel()
        {
            Utilities = new List<Utility>();
            Currency = new Currency();
        }

    }
}