using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string Period { get; set; }
        public decimal Sum { get; set; }
        public bool Paid { get; set; }
        public Customer Customer { get; set; }
    }
}