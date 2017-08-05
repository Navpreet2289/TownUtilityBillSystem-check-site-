using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models
{
    public class Meter
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Serial number")]
        public string SerialNumber { get; set; }

        [Required]
        [Display(Name = "Release date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Varification date")]
        public DateTime VarificationDate { get; set; }

        public float ConsumedMonthValue { get; set; }       

        public Address Address { get; set; }

        public MeterType MeterType { get; set; }

        public List<MeterType> MeterTypes { get; set; }

        public List<MeterItemModel> MeterItemModels { get; set; }

        public List<ChartData> ChartData { get; set; }
    }
}