using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models
{
    public class MeterItem
    {
        public int Id { get; set; }
        public  Meter Meter { get; set; }
        public DateTime Date { get; set; }

        [Required]
        [Range(0.1, int.MaxValue, ErrorMessage = "The {0} must be bigger than 0.")]
        [Display(Name = "New value")]
        public float Value { get; set; }        
    }
}