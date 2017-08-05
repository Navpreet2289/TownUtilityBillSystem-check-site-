using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models
{
    public class CustomerModel
    {
        public List<Customer> Customers;
        public List<CustomerType> CustomerTypes;
        public List<Meter> Meters;
        public Customer Customer;
        public int TotalCount { get; set; }
        public AddressModel AddressModel;


        public CustomerModel()
        {
            Customers = new List<Customer>();
            AddressModel = new AddressModel();
            Meters = new List<Meter>();
            Customer = new Customer();
            CustomerTypes = new List<CustomerType>();
            TotalCount = 0;
        }
    }
}