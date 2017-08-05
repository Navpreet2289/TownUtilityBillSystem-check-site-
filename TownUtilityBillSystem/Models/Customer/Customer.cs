using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Account Number")]
        public string Account { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Surname { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(13, ErrorMessage = "The {0} must have from {2} to {1} characters long.", MinimumLength = 11)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        public Address Address { get; set; }

        [Required]
        public CustomerType CustomerType { get; set; }

        public List<CustomerType> CustomerTypes { get; set; }
    }
}