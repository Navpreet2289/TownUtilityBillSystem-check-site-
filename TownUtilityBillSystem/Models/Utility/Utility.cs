using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models
{
    public class Utility
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Range(0.1, int.MaxValue, ErrorMessage = "The {0} must be bigger than 0.")]
        public decimal Price { get; set; }


        [Required]
        [Display(Name = "Usage for standart price")]
        [Range(0.1, int.MaxValue, ErrorMessage = "The {0} must be bigger than 0.")]
        public decimal UsageForStandartPrice { get; set; }

        [Required]
        [Range(0.1, int.MaxValue, ErrorMessage = "The {0} must be bigger than 0.")]
        [Display(Name = "Higher price")]
        public decimal BigUsagePrice { get; set; }

        public string ImageIconPath { get; set; }

        public Unit Unit { get; set; }
    }
}