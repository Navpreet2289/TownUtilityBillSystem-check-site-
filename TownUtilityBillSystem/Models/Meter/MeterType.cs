using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models
{
    public class MeterType
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The {0} must be bigger than {1}.")]
        [Display(Name = "Varification period")]

        public int VarificationPeriod { get; set; }

        [Required]
        public Utility Utility { get; set; }

        public List<Utility> Utilities { get; set; }
    }
}