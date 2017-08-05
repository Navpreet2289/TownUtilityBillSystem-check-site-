using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TownUtilityBillSystem.Models;
using System.Data;
using System.Data.Entity;
using TownUtilityBillSystem;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using TownUtilityBillSystem.Controllers;
using static TownUtilityBillSystem.Controllers.InitialDBController;

namespace TownUtilityBillSystem.Controllers
{
    public class CustomerController : Controller
    {
        TownUtilityBillSystemEntities contextDB = new TownUtilityBillSystemEntities();
        private int customerCountToDisplay = 25;
        private static Random rnd = new Random();

        private static void CreateCustomerModelFromCustomerList(TownUtilityBillSystemEntities context, CustomerModel model, List<CUSTOMER> customersDB)
        {
            foreach (var c in customersDB)
            {
                var customerTypeDB = context.CUSTOMER_TYPE.Where(ct => ct.ID == c.CUSTOMER_TYPE_ID).FirstOrDefault();

                var addressDB = context.ADDRESS.Where(a => a.ID == c.ADDRESS_ID).FirstOrDefault();
                var indexDB = context.INDEX.Where(i => i.ID == addressDB.INDEX_ID).FirstOrDefault();
                var townDB = context.TOWN.Where(t => t.ID == addressDB.TOWN_ID).FirstOrDefault();
                var streetDB = context.STREET.Where(s => s.ID == addressDB.STREET_ID).FirstOrDefault();
                var buildingDB = context.BUILDING.Where(b => b.ID == addressDB.BUILDING_ID).FirstOrDefault();
                var flatPartDB = context.FLAT_PART.Where(fp => fp.ID == addressDB.FLAT_PART_ID).FirstOrDefault();

                var customerType = new CustomerType() { Id = customerTypeDB.ID, Name = customerTypeDB.NAME };
                var index = new Index() { Id = indexDB.ID, Value = indexDB.VALUE };
                var town = new Town() { Id = townDB.ID, Name = townDB.NAME };
                var street = new Street() { Id = streetDB.ID, Name = streetDB.NAME };
                var building = new Building() { Id = buildingDB.ID, Number = buildingDB.NUMBER };

                FlatPart flatPart = null;

                if (flatPartDB != null)
                {
                    if (!String.IsNullOrEmpty(flatPartDB.NAME) && !flatPartDB.NUMBER.HasValue)
                        flatPart = new FlatPart() { Id = flatPartDB.ID, Name = flatPartDB.NAME };

                    else if (String.IsNullOrEmpty(flatPartDB.NAME) && flatPartDB.NUMBER.HasValue)
                        flatPart = new FlatPart() { Id = flatPartDB.ID, Number = (int)flatPartDB.NUMBER };

                    else if (!String.IsNullOrEmpty(flatPartDB.NAME) && flatPartDB.NUMBER.HasValue)
                        flatPart = new FlatPart() { Id = flatPartDB.ID, Number = (int)flatPartDB.NUMBER, Name = flatPartDB.NAME };
                }

                var address = new Address() { Id = addressDB.ID, Index = index, Town = town, Street = street, Building = building, FlatPart = flatPart };

                model.Customers.Add(new Customer() { Id = c.ID, Account = c.ACCOUNT, Surname = c.SURNAME, Name = c.NAME, Email = c.EMAIL, Phone = c.PHONE, CustomerType = customerType, Address = address });
            }
        }
        private static List<int> GetPrivateCustomerTypeIds(TownUtilityBillSystemEntities context)
        {
            var privateTypesDB = (from ct in context.CUSTOMER_TYPE
                                  where
                                      ct.NAME.Contains("Apartment") ||
                                      ct.NAME.Contains("House")

                                  select ct
                              ).ToList();

            List<int> privateTypeIds = new List<int>();

            foreach (var t in privateTypesDB)
                privateTypeIds.Add(t.ID);
            return privateTypeIds;
        }
        
        private static List<CustomerType> GetCustomerTypesList(TownUtilityBillSystemEntities context)
        {
            var customerTypesDB = context.CUSTOMER_TYPE.ToList();
            List<CustomerType> CustomerTypesList = new List<CustomerType>();

            foreach (var ct in customerTypesDB)
                CustomerTypesList.Add(new CustomerType() { Id = ct.ID, Name = ct.NAME });
            return CustomerTypesList;
        }

        public ActionResult ShowCustomerMenu()
        {
            return View();
        }
        public ActionResult ShowAllCustomers()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new CustomerModel();

                var customersDB = context.CUSTOMER.OrderBy(c => c.SURNAME).ThenBy(c => c.NAME).ToList();

                foreach (var c in customersDB)
                {
                    var customerTypeDB = context.CUSTOMER_TYPE.Where(ct => ct.ID == c.CUSTOMER_TYPE_ID).FirstOrDefault();
                    var customerType = new CustomerType() { Id = customerTypeDB.ID, Name = customerTypeDB.NAME };

                    model.Customers.Add(new Customer() { Id = c.ID, Account = c.ACCOUNT, Surname = c.SURNAME, Name = c.NAME, Email = c.EMAIL, Phone = c.PHONE, CustomerType = customerType });

                }
                var view = View("~/Views/Customer/ShowAllCustomers.cshtml", model);

                return view;
            }
        }
        public ActionResult ShowCustomersDeleteMethod()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var customersDB = context.CUSTOMER.Include(c => c.ADDRESS).Include(m => m.CUSTOMER_TYPE);

                return View(customersDB.ToList());
            }
        }
        public ActionResult ShowPrivateCustomers()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                List<Customer> customers = new List<Customer>();
                var model = new CustomerModel();
                List<int> privateTypeIds = GetPrivateCustomerTypeIds(context);
                var customersDB = context.CUSTOMER.ToList();

                foreach (var c in customersDB)
                {
                    if (privateTypeIds.Contains(c.CUSTOMER_TYPE_ID))
                    {
                        var customerTypeDB = context.CUSTOMER_TYPE.Where(ct => ct.ID == c.CUSTOMER_TYPE_ID).FirstOrDefault();
                        var customerType = new CustomerType() { Id = customerTypeDB.ID, Name = customerTypeDB.NAME };

                        customers.Add(new Customer() { Id = c.ID, Account = c.ACCOUNT, Surname = c.SURNAME, Name = c.NAME, Email = c.EMAIL, Phone = c.PHONE, CustomerType = customerType });
                    }

                }
                model.Customers = customers.OrderBy(c => c.Surname).ThenBy(c => c.Name).ToList();

                var view = View("~/Views/Customer/ShowPrivateCustomers.cshtml", model);

                return view;
            }
        }
        public ActionResult ShowLegalCustomers()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                List<Customer> customers = new List<Customer>();
                var model = new CustomerModel();
                List<int> privateTypeIds = GetPrivateCustomerTypeIds(context);
                var customersDB = context.CUSTOMER.ToList();

                foreach (var c in customersDB)
                {
                    if (!privateTypeIds.Contains(c.CUSTOMER_TYPE_ID))
                    {
                        var customerTypeDB = context.CUSTOMER_TYPE.Where(ct => ct.ID == c.CUSTOMER_TYPE_ID).FirstOrDefault();
                        var customerType = new CustomerType() { Id = customerTypeDB.ID, Name = customerTypeDB.NAME };

                        customers.Add(new Customer() { Id = c.ID, Account = c.ACCOUNT, Surname = c.SURNAME, Name = c.NAME, Email = c.EMAIL, Phone = c.PHONE, CustomerType = customerType });
                    }

                }
                model.Customers = customers.OrderBy(c => c.Surname).ThenBy(c => c.Name).ToList();

                var view = View("~/Views/Customer/ShowLegalCustomers.cshtml", model);

                return view;
            }
        }
        public ActionResult FindCustomerBy()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new CustomerModel();
                var customersDB = context.CUSTOMER.ToList();

                model.TotalCount = customersDB.Count;

                CustomizedMethod.Shuffle(customersDB);
                customersDB.RemoveRange(customerCountToDisplay, customersDB.Count - customerCountToDisplay);
                CreateCustomerModelFromCustomerList(context, model, customersDB);

                var view = View("~/Views/Customer/FindCustomerBy.cshtml", model);

                return view;
            }
        }
        public ActionResult ShowFoundCustomers(string searchString)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new CustomerModel();

                var customersDB = (from c in context.CUSTOMER
                                   where
                                       c.ACCOUNT.Contains(searchString) ||
                                       c.SURNAME.Contains(searchString) ||
                                       (c.SURNAME + " " + c.NAME).Contains(searchString) ||
                                       c.NAME.Contains(searchString) ||
                                       c.PHONE.Contains(searchString) ||
                                       c.EMAIL.Contains(searchString) ||
                                       c.CUSTOMER_TYPE.NAME.Contains(searchString) ||
                                       c.ADDRESS.STREET.NAME.Contains(searchString) ||
                                       c.ADDRESS.TOWN.NAME.Contains(searchString) ||
                                       c.ADDRESS.FLAT_PART.NAME.Contains(searchString) ||
                                       c.ADDRESS.INDEX.VALUE.ToString().Contains(searchString)
                                   select c
                                    ).ToList();
                CreateCustomerModelFromCustomerList(context, model, customersDB);

                ViewBag.SearchString = searchString;

                return View(model);
            }
        }
        public ActionResult ShowDetailsForCustomer(int customerId)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new CustomerModel();

                var customerDB = context.CUSTOMER.Find(customerId);

                if (customerDB != null)
                {
                    var customerTypeDB = context.CUSTOMER_TYPE.Where(mt => mt.ID == customerDB.CUSTOMER_TYPE_ID).FirstOrDefault();
                    var addressDB = context.ADDRESS.Where(a => a.ID == customerDB.ADDRESS_ID).FirstOrDefault();
                    var indexDB = context.INDEX.Where(i => i.ID == addressDB.INDEX_ID).FirstOrDefault();
                    var townDB = context.TOWN.Where(t => t.ID == addressDB.TOWN_ID).FirstOrDefault();
                    var streetDB = context.STREET.Where(s => s.ID == addressDB.STREET_ID).FirstOrDefault();
                    var buildingDB = context.BUILDING.Where(b => b.ID == addressDB.BUILDING_ID).FirstOrDefault();
                    var flatPartDB = context.FLAT_PART.Where(fp => fp.ID == addressDB.FLAT_PART_ID).FirstOrDefault();
                    var metersDB = context.METER.Where(m => m.ADDRESS_ID == addressDB.ID).ToList();

                    foreach (var m in metersDB)
                    {
                        var meterTypeDB = contextDB.METER_TYPE.Where(mt => mt.ID == m.METER_TYPE_ID).FirstOrDefault();

                        var utilityDB = contextDB.UTILITY.Where(u => u.ID == meterTypeDB.UTILITY_ID).FirstOrDefault();

                        var utility = new Utility() { Id = utilityDB.ID, Name = utilityDB.NAME };

                        var meterType = new MeterType() { Id = meterTypeDB.ID, Name = meterTypeDB.NAME, Utility = utility };

                        model.Meters.Add(new Meter() { Id = m.ID, SerialNumber = m.SERIAL_NUMBER, ReleaseDate = m.RELEASE_DATE, VarificationDate = m.VARIFICATION_DATE, MeterType = meterType });
                    }

                    var customerType = new CustomerType() { Id = customerTypeDB.ID, Name = customerTypeDB.NAME };
                    var index = new Index() { Id = indexDB.ID, Value = indexDB.VALUE };
                    var town = new Town() { Id = townDB.ID, Name = townDB.NAME };
                    var street = new Street() { Id = streetDB.ID, Name = streetDB.NAME };
                    var building = new Building() { Id = buildingDB.ID, Number = buildingDB.NUMBER, Square = (float)buildingDB.SQUARE, ImagePath = GetBuildingImage(buildingDB.ID) };

                    FlatPart flatPart = null;

                    if (flatPartDB != null)
                    {
                        if (!String.IsNullOrEmpty(flatPartDB.NAME) && !flatPartDB.NUMBER.HasValue)
                            flatPart = new FlatPart() { Id = flatPartDB.ID, Name = flatPartDB.NAME };

                        else if (String.IsNullOrEmpty(flatPartDB.NAME) && flatPartDB.NUMBER.HasValue)
                            flatPart = new FlatPart() { Id = flatPartDB.ID, Number = (int)flatPartDB.NUMBER };

                        else if (!String.IsNullOrEmpty(flatPartDB.NAME) && flatPartDB.NUMBER.HasValue)
                            flatPart = new FlatPart() { Id = flatPartDB.ID, Number = (int)flatPartDB.NUMBER, Name = flatPartDB.NAME };
                    }

                    var address = new Address() { Id = addressDB.ID, Index = index, Town = town, Street = street, Building = building, FlatPart = flatPart };

                    model.Customer = new Customer() { Id = customerDB.ID, Account = customerDB.ACCOUNT, Surname = customerDB.SURNAME, Name = customerDB.NAME, Email = customerDB.EMAIL, Phone = customerDB.PHONE, CustomerType = customerType, Address = address };
                    ViewBag.currentAddressForJS = address;
                }
                else
                    model.Customer = null;

                var view = View("~/Views/Customer/ShowDetailsForCustomer.cshtml", model);

                return view;
            }
        }
        public string GetBuildingImage(int buildingId)
        {
            IMAGEBUILDING imageDB = null;
            string imageName = "";
            string imagePath = "";
            string imagePathDB = "";
            string folderName = "";

            var buildingDB = contextDB.BUILDING.Where(b => b.ID == buildingId).FirstOrDefault();

            if (buildingDB != null)
                imageDB = contextDB.IMAGEBUILDING.Where(i => i.ID == buildingDB.IMAGE_ID).FirstOrDefault();

            if (imageDB != null)
            {
                imagePathDB = imageDB.PATH.ToString();
                folderName = Path.GetFileName(Path.GetDirectoryName(imagePathDB));
                imageName = Path.GetFileName(imagePathDB);
                imagePath = "/Images/" + folderName + "/" + imageName;
            }
            else
                imagePath = "/Images/NoImageIcon.jpg";

            return imagePath;
        }
        public ActionResult EditCustomer(int customerId)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new Customer();

                var customerDB = context.CUSTOMER.Find(customerId);

                if (customerDB != null)
                {
                    var customerTypeDB = context.CUSTOMER_TYPE.Where(mt => mt.ID == customerDB.CUSTOMER_TYPE_ID).FirstOrDefault();
                    var addressDB = context.ADDRESS.Where(a => a.ID == customerDB.ADDRESS_ID).FirstOrDefault();
                    var indexDB = context.INDEX.Where(i => i.ID == addressDB.INDEX_ID).FirstOrDefault();
                    var townDB = context.TOWN.Where(t => t.ID == addressDB.TOWN_ID).FirstOrDefault();
                    var streetDB = context.STREET.Where(s => s.ID == addressDB.STREET_ID).FirstOrDefault();
                    var buildingDB = context.BUILDING.Where(b => b.ID == addressDB.BUILDING_ID).FirstOrDefault();
                    var flatPartDB = context.FLAT_PART.Where(fp => fp.ID == addressDB.FLAT_PART_ID).FirstOrDefault();


                    var customerType = new CustomerType() { Id = customerTypeDB.ID, Name = customerTypeDB.NAME };
                    var index = new Index() { Id = indexDB.ID, Value = indexDB.VALUE };
                    var town = new Town() { Id = townDB.ID, Name = townDB.NAME };
                    var street = new Street() { Id = streetDB.ID, Name = streetDB.NAME };
                    var building = new Building() { Id = buildingDB.ID, Number = buildingDB.NUMBER, Square = (float)buildingDB.SQUARE, ImagePath = GetBuildingImage(buildingDB.ID) };

                    FlatPart flatPart = null;

                    if (flatPartDB != null)
                    {
                        if (!String.IsNullOrEmpty(flatPartDB.NAME) && !flatPartDB.NUMBER.HasValue)
                            flatPart = new FlatPart() { Id = flatPartDB.ID, Name = flatPartDB.NAME };

                        else if (String.IsNullOrEmpty(flatPartDB.NAME) && flatPartDB.NUMBER.HasValue)
                            flatPart = new FlatPart() { Id = flatPartDB.ID, Number = (int)flatPartDB.NUMBER };

                        else if (!String.IsNullOrEmpty(flatPartDB.NAME) && flatPartDB.NUMBER.HasValue)
                            flatPart = new FlatPart() { Id = flatPartDB.ID, Number = (int)flatPartDB.NUMBER, Name = flatPartDB.NAME };
                    }

                    var address = new Address() { Id = addressDB.ID, Index = index, Town = town, Street = street, Building = building, FlatPart = flatPart };

                    model.Id = customerDB.ID;
                    model.Account = customerDB.ACCOUNT;
                    model.Surname = customerDB.SURNAME;
                    model.Name = customerDB.NAME;
                    model.Email = customerDB.EMAIL;
                    model.Phone = customerDB.PHONE;
                    model.CustomerType = customerType;
                    model.Address = address;

                    var customerTypesDB = context.CUSTOMER_TYPE.ToList();
                    model.CustomerTypes = new List<CustomerType>();

                    foreach (var ct in customerTypesDB)
                        model.CustomerTypes.Add(new CustomerType() { Id = ct.ID, Name = ct.NAME });

                }
                else
                    model = null;

                var view = View("~/Views/Customer/EditCustomer.cshtml", model);

                return view;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer(Customer customer)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                if (ModelState.IsValid)
                {
                    var customerDB = context.CUSTOMER.Find(customer.Id);

                    customerDB.ACCOUNT = customer.Account;
                    if (customerDB.SURNAME != null)
                        customerDB.SURNAME = customer.Surname;
                    customerDB.NAME = customer.Name;
                    customerDB.EMAIL = customer.Email;
                    customerDB.PHONE = customer.Phone;
                    customerDB.CUSTOMER_TYPE_ID = customer.CustomerType.Id;

                    context.SaveChanges();

                    List<int> privateTypeIds = GetPrivateCustomerTypeIds(context);

                    if (privateTypeIds.Contains(customer.CustomerType.Id))
                        return RedirectToAction("ShowPrivateCustomers", "Customer");
                    else
                        return RedirectToAction("ShowLegalCustomers", "Customer");

                }

                var customerTypesDB = context.CUSTOMER_TYPE.ToList();
                customer.CustomerTypes = new List<CustomerType>();

                foreach (var ct in customerTypesDB)
                    customer.CustomerTypes.Add(new CustomerType() { Id = ct.ID, Name = ct.NAME });

                return View();
            }
        }

        public ActionResult DeleteCustomer(int customerId)
        {
            ViewBag.Message = CustomizedMessage.NoAdministratorRightsMessage;
            return View();
        }

        public ActionResult CreateCustomer()
        {
            ViewBag.Message = CustomizedMessage.NoAdministratorRightsMessage;
            return View();
        }
    }
}