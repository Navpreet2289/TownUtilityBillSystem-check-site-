using System.ComponentModel.DataAnnotations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace TownUtilityBillSystem.Models.PaymentCard
{
    public class PaymentCard
    {
        public int Id { get; set; }

        [Required]        
        [StringLength(19, ErrorMessage = "The {0} must have {2} digits.", MinimumLength = 16)]
        [Display(Name = "Payment card number")]
        public string Number { get; set; }        

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Card owner: surname")]
        public string Surname { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Card owner: name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Expire date")]
        public DateTime ExpireDate { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "The {0} must have {2} digits.", MinimumLength = 3)]
        [Display(Name = "CVV number")]
        public string CVV { get; set; }

        [Required]
        [Display(Name = "Card type")]
        public PaymentCardType Type { get; set; }
    }
}