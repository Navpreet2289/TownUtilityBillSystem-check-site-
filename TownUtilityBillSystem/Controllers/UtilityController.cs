using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TownUtilityBillSystem.Models;

namespace TownUtilityBillSystem.Controllers
{
    public class UtilityController : Controller
    {
        public ActionResult ShowUtilities()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new UtilityModel();
                var utilitiesDB = context.UTILITY.ToList();

                foreach (var item in utilitiesDB)
                    model.Utilities.Add(new Utility { Id = item.ID, Name = item.NAME });
                
                return View(model);
            }
        }

        public ActionResult ShowAllUtilitiesPrices()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new UtilityModel();
                var utilitiesDB = context.UTILITY.ToList();

                foreach (var item in utilitiesDB)
                {
                    var unitDB = context.UNIT.Where(u => u.ID == item.UNIT_ID).FirstOrDefault();
                    var unit = new Unit() { Id = unitDB.ID, Name = unitDB.NAME};

                    if(item.USAGEFORSTANDARTPRICE != null && item.BIGUSAGEPRICE != null)
                        model.Utilities.Add(new Utility { Id = item.ID, Name = item.NAME, Price = item.PRICE, BigUsagePrice = (decimal)item.BIGUSAGEPRICE, UsageForStandartPrice = Math.Round((decimal)item.USAGEFORSTANDARTPRICE,0), ImageIconPath = CustomizedMethod.GetUtilityImage(item.ID), Unit = unit});
                    
                    else
                        model.Utilities.Add(new Utility { Id = item.ID, Name = item.NAME, Price = item.PRICE, ImageIconPath = CustomizedMethod.GetUtilityImage(item.ID), Unit = unit });
                }
                return View(model);
            }
        }

        public ActionResult ShowUtility(string utilityName)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var utilityDB = context.UTILITY.Where(u => u.NAME == utilityName).FirstOrDefault();
                var unitDB = context.UNIT.Find(utilityDB.UNIT_ID);
                var utilitiesDB = context.UTILITY.ToList();

                var model = new UtilityModel();
                model.Unit.Id = unitDB.ID;
                model.Unit.Name = unitDB.NAME;

                model.Utility.Id = utilityDB.ID;
                model.Utility.Name = utilityDB.NAME;
                model.Utility.Price = utilityDB.PRICE;
                model.Utility.ImageIconPath = CustomizedMethod.GetUtilityImage(utilityDB.ID);

                if (utilityDB.USAGEFORSTANDARTPRICE != null)
                    model.Utility.UsageForStandartPrice = (decimal)utilityDB.USAGEFORSTANDARTPRICE;
                if (utilityDB.BIGUSAGEPRICE != null)
                    model.Utility.BigUsagePrice = (decimal)utilityDB.BIGUSAGEPRICE;

                model.Utility.Unit = model.Unit;

                foreach (var u in utilitiesDB)
                {
                    if(u.NAME != model.Utility.Name)
                        model.Utilities.Add(new Utility() { Name = u.NAME});
                }

                var meterTypesDB = context.METER_TYPE.Where(mt => mt.UTILITY_ID == model.Utility.Id).ToList();
                var metersDB = context.METER.ToList();
                HashSet<int> meterTypesIds = new HashSet<int>();
                
                foreach (var m in meterTypesDB)
                    meterTypesIds.Add(m.ID);

                foreach (var m in metersDB)
                {
                    if (meterTypesIds.Contains(m.METER_TYPE_ID))
                        model.MeterQty += 1;
                }
              
                var view = View("~/Views/Utility/ShowUtility.cshtml", model);
                return view;
            }
        }       

        public ActionResult EditUtility(int utilityId)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new Utility();

                var utilityDB = context.UTILITY.Find(utilityId);

                if (utilityDB != null)
                {
                    var unitDB = context.UNIT.Where(u => u.ID == utilityDB.UNIT_ID).FirstOrDefault();
                    var unit = new Unit() { Id = unitDB.ID, Name = unitDB.NAME };

                    model.Id = utilityDB.ID;
                    model.Name = utilityDB.NAME;
                    model.Price = utilityDB.PRICE;
                    if(utilityDB.BIGUSAGEPRICE != null)
                        model.BigUsagePrice = (decimal)utilityDB.BIGUSAGEPRICE;
                    if (utilityDB.USAGEFORSTANDARTPRICE != null)
                        model.UsageForStandartPrice = (decimal)utilityDB.USAGEFORSTANDARTPRICE;
                    model.Unit = unit;
                }
                else
                    model = null;

                var view = View("~/Views/Utility/EditUtility.cshtml", model);

                return view;
            }
        }
        
        public ActionResult DeleteUtility(int utilityId)
        {
            ViewBag.Message = CustomizedMessage.NoAdministratorRightsMessage;
            return View();
        }

        public ActionResult CreateUtility()
        {
            ViewBag.Message = CustomizedMessage.NoAdministratorRightsMessage;
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUtility(Utility utility)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                if (ModelState.IsValid)
                {
                    var utilityDB = context.UTILITY.Find(utility.Id);

                    utilityDB.NAME = utility.Name;
                    utilityDB.PRICE= utility.Price;
                    if (utilityDB.USAGEFORSTANDARTPRICE != null)
                        utilityDB.USAGEFORSTANDARTPRICE = utility.UsageForStandartPrice;
                    if (utilityDB.BIGUSAGEPRICE != null)
                        utilityDB.BIGUSAGEPRICE = utility.BigUsagePrice;

                    context.SaveChanges();
                    return RedirectToAction("ShowUtility", "Utility", new { utilityName = utilityDB.NAME });
                }
                return View();
            }
        }       
    }
}