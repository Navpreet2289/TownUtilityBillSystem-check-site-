using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TownUtilityBillSystem.Models;
using TownUtilityBillSystem.Models.PaymentCard;
using static TownUtilityBillSystem.Controllers.InitialDBController;

namespace TownUtilityBillSystem.Controllers
{
    public class BillController : Controller
    {
        private int billCountToDisplay = 25;
        TownUtilityBillSystemEntities contextDB = new TownUtilityBillSystemEntities();

        public ActionResult ShowAllBillsForCustomer(int customerId)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new BillModel();
                var billsDB = context.BILL.Where(b => b.CUSTOMER_ID == customerId).ToList();
                var customerDB = context.CUSTOMER.Find(customerId);

                if (customerDB != null)
                    GetCustomerDataForView(context, model, customerDB);
                else
                    model.CustomerModel.Customer = null;

                foreach (var b in billsDB)
                    model.Bills.Add(new Bill() { Id = b.ID, Date = b.DATE, Number = b.NUMBER, Period = CustomizedMethod.GetFullMonthName(b), Sum = b.SUM, Paid = b.PAID });

                return View(model);
            }
        }

        private void GetCustomerDataForView(TownUtilityBillSystemEntities context, BillModel model, CUSTOMER customerDB)
        {
            CustomerType customerType;
            Address address;
            GetAddressAndCustomerTypeForCustomer(context, customerDB, out customerType, out address);

            model.CustomerModel.Customer = new Customer() { Id = customerDB.ID, Account = customerDB.ACCOUNT, Surname = customerDB.SURNAME, Name = customerDB.NAME, Email = customerDB.EMAIL, Phone = customerDB.PHONE, CustomerType = customerType, Address = address };
        }

        private void GetAddressAndCustomerTypeForCustomer(TownUtilityBillSystemEntities context, CUSTOMER customerDB, out CustomerType customerType, out Address address)
        {
            var customerTypeDB = context.CUSTOMER_TYPE.Where(mt => mt.ID == customerDB.CUSTOMER_TYPE_ID).FirstOrDefault();
            var addressDB = context.ADDRESS.Where(a => a.ID == customerDB.ADDRESS_ID).FirstOrDefault();
            var indexDB = context.INDEX.Where(i => i.ID == addressDB.INDEX_ID).FirstOrDefault();
            var townDB = context.TOWN.Where(t => t.ID == addressDB.TOWN_ID).FirstOrDefault();
            var streetDB = context.STREET.Where(s => s.ID == addressDB.STREET_ID).FirstOrDefault();
            var buildingDB = context.BUILDING.Where(b => b.ID == addressDB.BUILDING_ID).FirstOrDefault();
            var flatPartDB = context.FLAT_PART.Where(fp => fp.ID == addressDB.FLAT_PART_ID).FirstOrDefault();

            customerType = new CustomerType() { Id = customerTypeDB.ID, Name = customerTypeDB.NAME };
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

            address = new Address() { Id = addressDB.ID, Index = index, Town = town, Street = street, Building = building, FlatPart = flatPart };
        }

        public ActionResult ShowBill(int bill_Id)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new BillModel();

                int customerId = 0;
                float consumedMonthValue = 0;
                float utilityCharges = 0;

                var billDB = context.BILL.Find(bill_Id);

                if (billDB != null)
                {

                    model.Bill = new Bill() { Id = billDB.ID, Number = billDB.NUMBER, Date = billDB.DATE, Period = CustomizedMethod.GetFullMonthName(billDB), Sum = billDB.SUM, Paid = billDB.PAID };
                    customerId = billDB.CUSTOMER_ID;
                }

                DateTime startDate = new DateTime();
                DateTime finishDate = new DateTime();
                DateTime startDateForChart = new DateTime();
                DateTime finishDateForChart = new DateTime();

                startDate = Convert.ToDateTime(model.Bill.Period + "-01");
                finishDate = startDate.AddMonths(1);

                int monthDifferenceBillPeriodAndStartUsageChart = 1;
                int presChartYear = startDate.Year;
                int startChartDay = 1;
                int prevChartYear = presChartYear - 1;
                int presChartMonth = startDate.Month;

                float valueDifference;
                string monthName;

                var customerDB = context.CUSTOMER.Where(c => c.ID == customerId).FirstOrDefault();

                if (customerDB != null)
                    GetCustomerDataForView(context, model, customerDB);
                else
                    model.CustomerModel.Customer = null;

                var metersDB = context.METER.Where(m => m.ADDRESS_ID == model.CustomerModel.Customer.Address.Id).ToList();

                List<MeterItem> meterListDB = new List<MeterItem>();

                foreach (var m in metersDB)
                {
                    List<ChartData> chartData = new List<ChartData>();
                    meterListDB.Clear();

                    startDateForChart = new DateTime(prevChartYear, presChartMonth + monthDifferenceBillPeriodAndStartUsageChart, startChartDay);
                    finishDateForChart = startDate.AddMonths(1);

                    var meterTypeDB = context.METER_TYPE.Where(mt => mt.ID == m.METER_TYPE_ID).FirstOrDefault();
                    var utilityDB = context.UTILITY.Where(u => u.ID == meterTypeDB.UTILITY_ID).FirstOrDefault();
                    var unitDB = context.UNIT.Where(u => u.ID == utilityDB.UNIT_ID).FirstOrDefault();

                    var unit = new Unit() { Id = unitDB.ID, Name = unitDB.NAME };
                    var utility = new Utility() { Id = utilityDB.ID, Name = utilityDB.NAME, Unit = unit, Price = utilityDB.PRICE, ImageIconPath = CustomizedMethod.GetUtilityImage(utilityDB.ID) };

                    if (utilityDB.USAGEFORSTANDARTPRICE != null)
                        utility.UsageForStandartPrice = (decimal)utilityDB.USAGEFORSTANDARTPRICE;
                    if (utilityDB.BIGUSAGEPRICE != null)
                        utility.BigUsagePrice = (decimal)utilityDB.BIGUSAGEPRICE;

                    if(utility.Id == (int)Utilities.Heating)
                        GetTemperatureHistory(context, model, m);

                    var meterItemsDB = (from mi in context.METER_ITEM
                                        where mi.METER_ID == m.ID
                                        select mi).ToList();

                    foreach (var i in meterItemsDB)
                        meterListDB.Add(new MeterItem() { Id = i.ID, Date = i.DATE, Value = i.VALUE });

                    if (meterItemsDB.Count != 0)
                    {
                        for (; startDateForChart <= finishDateForChart; startDateForChart = startDateForChart.AddMonths(1))
                        {
                            var startElValue = meterListDB.Where(ml => ml.Date == startDateForChart.AddMonths(-1)).FirstOrDefault().Value;
                            var finishElValue = meterListDB.Where(ml => ml.Date == startDateForChart).FirstOrDefault().Value;
                            valueDifference = (float)Math.Round(finishElValue - startElValue, 2);

                            if (startDateForChart.Month != 1)
                            {
                                Months month = (Months)(startDateForChart.Month - 1);
                                monthName = month.ToString() + " " + startDateForChart.Year;
                            }
                            else
                            {
                                Months month = Months.December;
                                monthName = month.ToString() + " " + startDateForChart.AddYears(-1).Year;
                            }

                            chartData.Add(new ChartData() { MonthName = monthName, Value = valueDifference });
                        }
                    }

                    var meterType = new MeterType() { Id = meterTypeDB.ID, Name = meterTypeDB.NAME, Utility = utility };

                    var startMeterItem = (from item in context.METER_ITEM
                                          where item.METER_ID == m.ID && item.DATE == startDate
                                          select item).FirstOrDefault();

                    var finishMeterItem = (from item in context.METER_ITEM
                                           where item.METER_ID == m.ID && item.DATE == finishDate
                                           select item).FirstOrDefault();

                    consumedMonthValue = finishMeterItem.VALUE - startMeterItem.VALUE;

                    if (m.METER_TYPE.UTILITY_ID == (int)Utilities.Electricity && consumedMonthValue > (float)m.METER_TYPE.UTILITY.USAGEFORSTANDARTPRICE)
                        utilityCharges = (float)m.METER_TYPE.UTILITY.USAGEFORSTANDARTPRICE * (float)m.METER_TYPE.UTILITY.PRICE + (consumedMonthValue - (float)m.METER_TYPE.UTILITY.USAGEFORSTANDARTPRICE) * (float)m.METER_TYPE.UTILITY.BIGUSAGEPRICE;

                    else
                        utilityCharges = consumedMonthValue * (float)m.METER_TYPE.UTILITY.PRICE;

                    model.UtilityChargesChartData.Add(new ChartData() { UtilityCharges = (float)Math.Round(utilityCharges, 2) });

                    model.CustomerModel.Meters.Add(
                         new Meter()
                         {
                             Id = m.ID,
                             SerialNumber = m.SERIAL_NUMBER,
                             ReleaseDate = m.RELEASE_DATE,
                             VarificationDate = m.VARIFICATION_DATE,
                             MeterType = meterType,
                             ConsumedMonthValue = consumedMonthValue,
                             ChartData = chartData
                         });
                }
                return View(model);
            }
        }

        private static void GetTemperatureHistory(TownUtilityBillSystemEntities context, BillModel model, METER m)
        {
            List<Temperature> temperaturesDB = new List<Temperature>();
            DateTime temperatureStartDate = new DateTime();
            DateTime temperatureFinishDate = new DateTime();
            int temperatureYearsHistory = 2;
            float valueSum = 0;
            float averageValue;
            string fullMonthName = "";

            temperatureStartDate = Convert.ToDateTime(model.Bill.Period + "-01");
            temperatureStartDate = temperatureStartDate.AddYears(-1);
            temperatureFinishDate = temperatureStartDate.AddMonths(1);

            var temperatureItemsDB = context.TEMPERATURE.Where(t => t.TOWN_ID == m.ADDRESS.TOWN_ID).ToList();

            foreach (var d in temperatureItemsDB)
                temperaturesDB.Add(new Temperature() { Id = d.ID, Date = d.DATE, MinValue = d.MINVALUE, MaxValue = d.MAXVALUE });

            for (int j = 0; j < temperatureYearsHistory; j++)
            {
                for (; temperatureStartDate < temperatureFinishDate; temperatureStartDate = temperatureStartDate.AddDays(1))
                    valueSum += (float)(temperaturesDB.Where(t => t.Date == temperatureStartDate).FirstOrDefault().MinValue + temperaturesDB.Where(t => t.Date == temperatureStartDate).FirstOrDefault().MaxValue) / 2;

                temperatureStartDate = temperatureStartDate.AddMonths(-1);
                fullMonthName = temperatureStartDate.ToString("MMMM yyyy");
                averageValue = valueSum / System.DateTime.DaysInMonth(temperatureStartDate.Year, temperatureStartDate.Month);

                model.Temperatures.Add(new TemperatureModel() { AverageValue = (float)Math.Round(averageValue, 1), MonthName = fullMonthName });

                temperatureStartDate = temperatureStartDate.AddYears(1);
                temperatureFinishDate = temperatureStartDate.AddMonths(1);
                valueSum = 0;
            }
        }

        public string GetBuildingImage(int buildingId)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                IMAGEBUILDING imageDB = null;
                string imageName = "";
                string imagePathForHtml = "";
                string imagePathDB = "";
                string folderName = "";

                var buildingDB = context.BUILDING.Where(b => b.ID == buildingId).FirstOrDefault();

                if (buildingDB != null)
                    imageDB = context.IMAGEBUILDING.Where(i => i.ID == buildingDB.IMAGE_ID).FirstOrDefault();

                if (imageDB != null)
                {
                    imagePathDB = imageDB.PATH.ToString();
                    folderName = Path.GetFileName(Path.GetDirectoryName(imagePathDB));
                    imageName = Path.GetFileName(imagePathDB);
                    imagePathForHtml = "/Images/" + folderName + "/" + imageName;
                }
                return imagePathForHtml;
            }
        }

        public ActionResult PayOnLineBill()
        {
            var model = new BillModel();
            //delee method
            return View(model);
        }

        public ActionResult DeleteBill(int bill_Id)
        {
            ViewBag.Message = CustomizedMessage.NoAdministratorRightsMessage;
            return View();
        }

        public ActionResult CreateBill()
        {
            ViewBag.Message = CustomizedMessage.NoAdministratorRightsMessage;
            return View();
        }

        public ActionResult ShowBills()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new BillModel();
                var billsDB = context.BILL.ToList();

                CustomizedMethod.Shuffle(billsDB);
                billsDB.RemoveRange(billCountToDisplay, billsDB.Count - billCountToDisplay);

                CreateBillModelFromBillList(context, model, billsDB);

                return View(model);
            }
        }

        private void CreateBillModelFromBillList(TownUtilityBillSystemEntities context, BillModel model, List<BILL> billsDB)
        {
            foreach (var b in billsDB)
            {
                Customer customer = new Customer();

                var customerDB = context.CUSTOMER.Where(c => c.ID == b.CUSTOMER_ID).FirstOrDefault();

                CustomerType customerType;
                Address address;
                GetAddressAndCustomerTypeForCustomer(context, customerDB, out customerType, out address);

                customer.Id = customerDB.ID;
                customer.Surname = customerDB.SURNAME;
                customer.Name = customerDB.NAME;

                customer.Address = address;

                model.Bills.Add(new Bill() { Id = b.ID, Number = b.NUMBER, Period = CustomizedMethod.GetFullMonthName(b), Date = b.DATE, Sum = b.SUM, Paid = b.PAID, Customer = customer });
            }
        }

        public ActionResult ShowFoundBills(string searchString)
        {
            DateTime billPeriodDate = new DateTime();
            string billPeriod = null;

            if (DateTime.TryParse(searchString, out billPeriodDate))
                billPeriod = billPeriodDate.ToString("yyyy-MM");

            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new BillModel();

                var billsDB = (from b in context.BILL
                               where
                                   b.NUMBER.Contains(searchString) ||
                                   b.PERIOD.Contains(billPeriod) ||
                                   b.CUSTOMER.SURNAME.Contains(searchString) ||
                                   b.CUSTOMER.NAME.Contains(searchString) ||
                                   (b.CUSTOMER.SURNAME + " " + b.CUSTOMER.NAME).Contains(searchString) ||
                                   b.CUSTOMER.ADDRESS.TOWN.NAME.Contains(searchString) ||
                                   b.CUSTOMER.ADDRESS.STREET.NAME.Contains(searchString) ||
                                   b.CUSTOMER.ADDRESS.BUILDING.NUMBER.Contains(searchString) ||
                                   b.CUSTOMER.ADDRESS.FLAT_PART.NAME.Contains(searchString) ||
                                   b.CUSTOMER.CUSTOMER_TYPE.NAME.Contains(searchString) ||
                                   b.CUSTOMER.ADDRESS.INDEX.VALUE.ToString().Contains(searchString)
                               select b
                                    ).ToList();


                CreateBillModelFromBillList(context, model, billsDB);

                ViewBag.SearchString = searchString;

                return View(model);
            }
        }

        public JsonResult GetBillData(string billNumber)
        {
            var model = new BillModel();
            int customerId = 0;
            var billDB = contextDB.BILL.Where(b => b.NUMBER == billNumber).FirstOrDefault();

            if (billDB != null)
            {
                model.Bill.Id = billDB.ID;
                model.Bill.Number = billDB.NUMBER;
                model.Bill.Sum = billDB.SUM;
                model.Bill.Paid = billDB.PAID;
                model.Bill.Period = CustomizedMethod.GetFullMonthName(billDB);

                customerId = billDB.CUSTOMER_ID;
                var customerDB = contextDB.CUSTOMER.Where(c => c.ID == customerId).FirstOrDefault();

                if (customerDB != null)
                {
                    model.CustomerModel.Customer.Surname = customerDB.SURNAME;
                    model.CustomerModel.Customer.Name = customerDB.NAME;
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CallPaymentCardForm(int bill_Id)
        {
            return Json(Url.Action("PaymentCardForm", "Bill", bill_Id));
        }

        public ActionResult PaymentCardForm(int bill_Id)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new PaymentCardModel();
                var billDB = context.BILL.Find(bill_Id);

                if (billDB != null)
                    model.Bill = new Bill() { Id = billDB.ID, Number = billDB.NUMBER, Date = billDB.DATE, Period = CustomizedMethod.GetFullMonthName(billDB), Sum = billDB.SUM, Paid = billDB.PAID };

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PaymentCardForm(PaymentCardModel payment)
        {
            Regex regexCardNumber = new Regex("^[0-9]{1,16}$");
            Regex regexCardCVV = new Regex("^[0-9]{1,3}$");
            DateTime presentDate = DateTime.Today;

            if (payment.PaymentCard.Number != null)
            {
                payment.PaymentCard.Number = payment.PaymentCard.Number.Replace(" ", "");
                if (!regexCardNumber.IsMatch(payment.PaymentCard.Number))
                    ViewBag.WrongCardNumberError = "The Credit card number must have 16 digits.";
            }

            if (payment.PaymentCard.CVV != null)
            {
                if (!regexCardCVV.IsMatch(payment.PaymentCard.CVV))
                    ViewBag.WrongCVVError = "The CVV number must have 3 digits.";
            }           

            if (payment.PaymentCard.ExpireDate.ToString() != "1/1/0001 12:00:00 AM" && payment.PaymentCard.ExpireDate < presentDate)
                ViewBag.WrongExpireDateError = "The card is expired";

            if (ModelState.IsValid)
            {
                using (var context = new TownUtilityBillSystemEntities())
                {
                    var billDB = context.BILL.Find(payment.Bill.Id);
                    billDB.PAID = true;

                    context.SaveChanges();

                    return RedirectToAction("ShowBillPaidInfo", "Bill", new { bill_Id = billDB.ID });
                }
            }

            return View(payment);
        }

        public ActionResult ShowBillPaidInfo(int bill_Id)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new BillModel();
                var billDB = context.BILL.Find(bill_Id);

                if (billDB != null)
                {
                    model.Bill = new Bill() { Id = billDB.ID, Date = billDB.DATE, Number = billDB.NUMBER, Sum = billDB.SUM, Paid = billDB.PAID, Period = CustomizedMethod.GetFullMonthName(billDB) };
                    var customerDB = context.CUSTOMER.Find(billDB.CUSTOMER_ID);

                    model.CustomerModel.Customer.Surname = customerDB.SURNAME;
                    model.CustomerModel.Customer.Name = customerDB.NAME;
                }

                return View(model);
            }
        }
    }
}