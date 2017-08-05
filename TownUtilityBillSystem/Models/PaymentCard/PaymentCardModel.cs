using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models.PaymentCard
{
    public class PaymentCardModel
    {
        [Required]
        public PaymentCard PaymentCard { get; set; }

        public Bill Bill { get; set; }
        public List<PaymentCardType> PaymentCardTypes;
        public Currency Currency;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The paying sum can not be nagative")]
        [Display(Name = "Paying sum")]
        public decimal PayingSum { get; set; }

        public PaymentCardModel()
        {
            PaymentCard = new PaymentCard();
            Bill = new Bill();

            PaymentCardTypes = new List<PaymentCardType>()
            {
                new PaymentCardType(){Id = 1, Name = "MasterCard" },
                new PaymentCardType(){Id = 2, Name = "Maestro" },
                new PaymentCardType(){Id = 2, Name = "Visa" },
                new PaymentCardType(){Id = 2, Name = "Visa Electron" }
            };
            Currency = new Currency();
        }
    }
}