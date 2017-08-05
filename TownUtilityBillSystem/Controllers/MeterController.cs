using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Web.Mvc;
using TownUtilityBillSystem;
using TownUtilityBillSystem.Models;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using static TownUtilityBillSystem.Controllers.InitialDBController;

namespace TownUtilityBillSystem.Controllers
{
    [Authorize]
    public class MeterController : Controller
    {
        TownUtilityBillSystemEntities contextDB = new TownUtilityBillSystemEntities();
        private int meterCountToDisplay = 25;
        private static Random rnd = new Random();
        // GET: Meter
        public ActionResult ShowMeterTypesForUtility(string utilityName)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new MeterTypeModel();
                var utilityDB = context.UTILITY.Where(u => u.NAME == utilityName).FirstOrDefault();
                var meterTypesDB = context.METER_TYPE.Where(mt => mt.UTILITY_ID == utilityDB.ID).ToList();

                model.Utility.Id = utilityDB.ID;
                model.Utility.Name = utilityDB.NAME;

                foreach (var mt in meterTypesDB)
                    model.MeterTypes.Add(new MeterType() { Id = mt.ID, Name = mt.NAME, VarificationPeriod = mt.VARIFICATION_PERIOD_YEARS });

                var view = View("~/Views/Meter/ShowMeterTypesForUtility.cshtml", model);

                return view;
            }
        }
        public ActionResult ShowAllMeterTypes()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new MeterTypeModel();
                var meterTypesDB = context.METER_TYPE.ToList();

                foreach (var mt in meterTypesDB)
                {
                    var utilityDB = context.UTILITY.Where(u => u.ID == mt.UTILITY_ID).FirstOrDefault();
                    var utility = new Utility() { Id = utilityDB.ID, Name = utilityDB.NAME, ImageIconPath = CustomizedMethod.GetUtilityImage(utilityDB.ID) };

                    model.MeterTypes.Add(new MeterType() { Id = mt.ID, Name = mt.NAME, VarificationPeriod = mt.VARIFICATION_PERIOD_YEARS, Utility = utility });
                }
                var view = View("~/Views/Meter/ShowAllMeterTypes.cshtml", model);

                return view;
            }
        }
        public ActionResult ShowRandomMeters()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new MeterModel();
                var metersDB = context.METER.ToList();

                model.TotalCount = metersDB.Count;
                CustomizedMethod.Shuffle(metersDB);
                metersDB.RemoveRange(meterCountToDisplay, metersDB.Count - meterCountToDisplay);

                CreateMeterModelFromMeterList(context, model, metersDB);

                var view = View("~/Views/Meter/ShowRandomMeters.cshtml", model);

                return view;
            }
        }
        public ActionResult ShowAllMeters()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var metersDB = context.METER.Include(m => m.ADDRESS).Include(m => m.METER_TYPE);

                return View(metersDB.ToList());
            }
        }
        private static void CreateMeterModelFromMeterList(TownUtilityBillSystemEntities context, MeterModel model, List<METER> metersDB)
        {
            foreach (var m in metersDB)
            {
                var meterTypeDB = context.METER_TYPE.Where(mt => mt.ID == m.METER_TYPE_ID).FirstOrDefault();
                var utilityDB = context.UTILITY.Where(u => u.ID == meterTypeDB.UTILITY_ID).FirstOrDefault();
                var addressDB = context.ADDRESS.Where(a => a.ID == m.ADDRESS_ID).FirstOrDefault();
                var indexDB = context.INDEX.Where(i => i.ID == addressDB.INDEX_ID).FirstOrDefault();
                var townDB = context.TOWN.Where(t => t.ID == addressDB.TOWN_ID).FirstOrDefault();
                var streetDB = context.STREET.Where(s => s.ID == addressDB.STREET_ID).FirstOrDefault();
                var buildingDB = context.BUILDING.Where(b => b.ID == addressDB.BUILDING_ID).FirstOrDefault();
                var flatPartDB = context.FLAT_PART.Where(fp => fp.ID == addressDB.FLAT_PART_ID).FirstOrDefault();

                var utility = new Utility() { Id = utilityDB.ID, Name = utilityDB.NAME };

                var meterType = new MeterType() { Id = meterTypeDB.ID, Name = meterTypeDB.NAME, Utility = utility };

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

                model.Meters.Add(new Meter() { Id = m.ID, SerialNumber = m.SERIAL_NUMBER, ReleaseDate = m.RELEASE_DATE, VarificationDate = m.VARIFICATION_DATE, MeterType = meterType, Address = address });
            }
        }
        
        public ActionResult FindMetersBy()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new MeterModel();
                var metersDB = context.METER.ToList();

                model.TotalCount = metersDB.Count;

                CustomizedMethod.Shuffle(metersDB);
                metersDB.RemoveRange(meterCountToDisplay, metersDB.Count - meterCountToDisplay);

                CreateMeterModelFromMeterList(context, model, metersDB);

                var view = View("~/Views/Meter/FindMetersBy.cshtml", model);

                return view;
            }
        }
        public ActionResult ShowFoundMeters(string searchString)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new MeterModel();
                var metersDB = (from m in context.METER
                                where
                                    m.SERIAL_NUMBER.Contains(searchString) ||
                                    m.METER_TYPE.NAME.Contains(searchString) ||
                                    m.METER_TYPE.UTILITY.NAME.Contains(searchString) ||
                                    m.ADDRESS.STREET.NAME.Contains(searchString) ||
                                    m.ADDRESS.TOWN.NAME.Contains(searchString) ||
                                    m.ADDRESS.FLAT_PART.NAME.Contains(searchString) ||
                                    m.ADDRESS.INDEX.VALUE.ToString().Contains(searchString)
                                select m
                                    ).ToList();

                CreateMeterModelFromMeterList(context, model, metersDB);

                ViewBag.SearchString = searchString;

                return View(model);
            }
        }
        public ActionResult FindMeterByAddress()
        {
            GetTowns();
            return View();
        }
        private void GetTowns()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                List<Town> TownList = new List<Town>();

                var townsDB = context.TOWN.ToList();

                foreach (var t in townsDB)
                    TownList.Add(new Town { Id = t.ID, Name = t.NAME });

                ViewBag.TownList = new SelectList(TownList, "Id", "Name");
            }
        }
        public JsonResult GetStreetList(int townId)
        {
            contextDB.Configuration.ProxyCreationEnabled = false;
            List<Street> StreetList = new List<Street>();

            var streetsDB = contextDB.STREET.Where(s => s.TOWN_ID == townId).ToList();

            foreach (var s in streetsDB)
            {
                StreetList.Add(new Street { Id = s.ID, Name = s.NAME });
            }
            return Json(StreetList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBuildingList(int streetId)
        {
            contextDB.Configuration.ProxyCreationEnabled = false;
            List<Building> BuildingList = new List<Building>();

            var buildingsDB = contextDB.BUILDING.Where(b => b.STREET_ID == streetId).ToList();

            foreach (var b in buildingsDB)
            {
                BuildingList.Add(new Building { Id = b.ID, Number = b.NUMBER });
            }
            return Json(BuildingList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFlatPartList(int buildingId)
        {
            contextDB.Configuration.ProxyCreationEnabled = false;
            List<FlatPart> FlatPartList = new List<FlatPart>();
            string imagePath = null;

            var flatPartsDB = contextDB.FLAT_PART.Where(f => f.BUILDING_ID == buildingId).ToList();
            var buildingDB = contextDB.BUILDING.Where(b => b.ID == buildingId).FirstOrDefault();
            var imageDB = contextDB.IMAGEBUILDING.Where(i => i.ID == buildingDB.IMAGE_ID).FirstOrDefault();

            if (imageDB != null)
            {
                imagePath = imageDB.PATH.ToString();
            }

            foreach (var f in flatPartsDB)
            {
                if (!String.IsNullOrEmpty(f.NAME) && !f.NUMBER.HasValue)
                    FlatPartList.Add(new FlatPart() { Id = f.ID, Name = f.NAME });

                else if (String.IsNullOrEmpty(f.NAME) && f.NUMBER.HasValue)
                    FlatPartList.Add(new FlatPart() { Id = f.ID, Number = (int)f.NUMBER });

                else if (!String.IsNullOrEmpty(f.NAME) && f.NUMBER.HasValue)
                    FlatPartList.Add(new FlatPart() { Id = f.ID, Number = (int)f.NUMBER, Name = f.NAME });
            }

            return Json(FlatPartList, JsonRequestBehavior.AllowGet);
        }
        public string GetBuildingImage(int buildingId)
        {
            IMAGEBUILDING imageDB = null;
            string imageName = "";
            string imagePathForHtml = "";
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
                imagePathForHtml = "<img src = '/Images/" + folderName + "/" + imageName + "'" + "id = 'buildingImage'/> <br /> <br /><strong>Building image</strong>";
            }
            else
                imagePathForHtml = "<img src = '/Images/NoImageIcon.jpg' id = 'buildingImage'/>";

            return imagePathForHtml;
        }
        public JsonResult GetMetersForBuilding(int buildingId)
        {
            contextDB.Configuration.ProxyCreationEnabled = false;

            List<Meter> meters = new List<Meter>();

            var addressDB = contextDB.ADDRESS.Where(a => a.BUILDING_ID == buildingId).FirstOrDefault();
            if (addressDB.FLAT_PART_ID == null)
            {
                var metersDB = contextDB.METER.Where(m => m.ADDRESS_ID == addressDB.ID).ToList();

                foreach (var m in metersDB)
                {
                    var meterTypeDB = contextDB.METER_TYPE.Where(mt => mt.ID == m.METER_TYPE_ID).FirstOrDefault();

                    var utilityDB = contextDB.UTILITY.Where(u => u.ID == meterTypeDB.UTILITY_ID).FirstOrDefault();

                    var utility = new Utility() { Id = utilityDB.ID, Name = utilityDB.NAME };

                    var meterType = new MeterType() { Id = meterTypeDB.ID, Name = meterTypeDB.NAME, Utility = utility };

                    meters.Add(new Meter() { Id = m.ID, SerialNumber = m.SERIAL_NUMBER, ReleaseDate = m.RELEASE_DATE, VarificationDate = m.VARIFICATION_DATE, MeterType = meterType });
                }
            }

            return Json(meters, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMetersForFlatPart(int flatPartId)
        {
            contextDB.Configuration.ProxyCreationEnabled = false;

            List<Meter> meters = new List<Meter>();

            var addressDB = contextDB.ADDRESS.Where(a => a.FLAT_PART_ID == flatPartId).FirstOrDefault();
            if (addressDB != null)
            {
                var metersDB = contextDB.METER.Where(m => m.ADDRESS_ID == addressDB.ID).ToList();

                foreach (var m in metersDB)
                {
                    var meterTypeDB = contextDB.METER_TYPE.Where(mt => mt.ID == m.METER_TYPE_ID).FirstOrDefault();

                    var utilityDB = contextDB.UTILITY.Where(u => u.ID == meterTypeDB.UTILITY_ID).FirstOrDefault();

                    var utility = new Utility() { Id = utilityDB.ID, Name = utilityDB.NAME };

                    var meterType = new MeterType() { Id = meterTypeDB.ID, Name = meterTypeDB.NAME, Utility = utility };

                    meters.Add(new Meter() { Id = m.ID, SerialNumber = m.SERIAL_NUMBER, ReleaseDate = m.RELEASE_DATE, VarificationDate = m.VARIFICATION_DATE, MeterType = meterType });
                }
            }
            return Json(meters, JsonRequestBehavior.AllowGet);
        }
        public string GetTownName(int townId)
        {
            string townName = "";

            var townDB = contextDB.TOWN.Where(t => t.ID == townId).FirstOrDefault();
            if (townDB != null)
                townName = townDB.NAME;

            return townName;
        }
        public string GetStreetName(int streetId)
        {
            string streetName = "";

            var streetDB = contextDB.STREET.Where(s => s.ID == streetId).FirstOrDefault();
            if (streetDB != null)
                streetName = streetDB.NAME;

            return streetName;
        }
        public string GetBuildingNumber(int buildingId)
        {
            string buildingNumber = "";

            var buildingDB = contextDB.BUILDING.Where(b => b.ID == buildingId).FirstOrDefault();
            if (buildingDB != null)
                buildingNumber = buildingDB.NUMBER.ToString();

            return buildingNumber;
        }
        public ActionResult ShowMeterData(int meterId)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new MeterItemModel();

                var meterItemsDB = from item in context.METER_ITEM.ToList()
                                   where item.METER_ID == meterId
                                   select item;

                CreateMeterModelWithOneMeter(meterId, context, model);

                foreach (var item in meterItemsDB)
                    model.MeterItems.Add(new MeterItem() { Id = item.ID, Date = item.DATE, Meter = model.Meter, Value = item.VALUE });

                var view = View("~/Views/Meter/ShowMeterData.cshtml", model);

                return view;
            }
        }
        private static void CreateMeterModelWithOneMeter(int meterId, TownUtilityBillSystemEntities context, MeterItemModel model)
        {
            var meterDB = context.METER.Where(m => m.ID == meterId).FirstOrDefault();
            var meterTypeDB = context.METER_TYPE.Where(mt => mt.ID == meterDB.METER_TYPE_ID).FirstOrDefault();
            var utilityDB = context.UTILITY.Where(u => u.ID == meterTypeDB.UTILITY_ID).FirstOrDefault();
            var addressDB = context.ADDRESS.Where(a => a.ID == meterDB.ADDRESS_ID).FirstOrDefault();
            var indexDB = context.INDEX.Where(i => i.ID == addressDB.INDEX_ID).FirstOrDefault();
            var townDB = context.TOWN.Where(t => t.ID == addressDB.TOWN_ID).FirstOrDefault();
            var streetDB = context.STREET.Where(s => s.ID == addressDB.STREET_ID).FirstOrDefault();
            var buildingDB = context.BUILDING.Where(b => b.ID == addressDB.BUILDING_ID).FirstOrDefault();
            var flatPartDB = context.FLAT_PART.Where(fp => fp.ID == addressDB.FLAT_PART_ID).FirstOrDefault();
            var unitDB = context.UNIT.Where(u => u.ID == utilityDB.UNIT_ID).FirstOrDefault();
            var unit = new Unit() { Id = unitDB.ID, Name = unitDB.NAME };

            var utility = new Utility() { Id = utilityDB.ID, Name = utilityDB.NAME, Unit = unit };

            var meterType = new MeterType() { Id = meterTypeDB.ID, Name = meterTypeDB.NAME, Utility = utility };

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

            model.Meter = new Meter() { Id = meterDB.ID, SerialNumber = meterDB.SERIAL_NUMBER, ReleaseDate = meterDB.RELEASE_DATE, VarificationDate = meterDB.VARIFICATION_DATE, MeterType = meterType, Address = address };
        }
        public ActionResult EditMeterData(int meterItemId)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new MeterItemModel();
                var meterItemDB = context.METER_ITEM.Find(meterItemId);
                var meterDB = context.METER.Where(m => m.ID == meterItemDB.METER_ID).FirstOrDefault();

                CreateMeterModelWithOneMeter(meterDB.ID, context, model);

                model.MeterItem = new MeterItem() { Id = meterItemDB.ID, Date = meterItemDB.DATE, Value = meterItemDB.VALUE };

                var view = View("~/Views/Meter/EditMeterData.cshtml", model);

                return view;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMeterData(MeterItem meterItem)
        {
            if (ModelState.IsValid)
            {
                using (var context = new TownUtilityBillSystemEntities())
                {
                    var meterItemDB = context.METER_ITEM.Find(meterItem.Id);

                    meterItemDB.VALUE = meterItem.Value;
                    context.SaveChanges();

                    return RedirectToAction("ShowMeterData", "Meter", new { meterId = meterItemDB.METER_ID });
                }
            }
            return View();
        }

        public ActionResult EditMeter(int meterId)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new Meter();

                var meterDB = context.METER.Find(meterId);

                if (meterDB != null)
                {
                    var meterTypeDB = context.METER_TYPE.Where(mt => mt.ID == meterDB.METER_TYPE_ID).FirstOrDefault();
                    var utilityDB = context.UTILITY.Where(u => u.ID == meterTypeDB.UTILITY_ID).FirstOrDefault();

                    var utility = new Utility() { Id = utilityDB.ID, Price = utilityDB.PRICE, Name = utilityDB.NAME, BigUsagePrice = utilityDB.BIGUSAGEPRICE ?? 0, UsageForStandartPrice = utilityDB.USAGEFORSTANDARTPRICE ?? 0 };
                    var meterType = new MeterType() { Id = meterTypeDB.ID, Name = meterTypeDB.NAME, VarificationPeriod = meterTypeDB.VARIFICATION_PERIOD_YEARS , Utility = utility};

                    model.Id = meterDB.ID;
                    model.MeterType = meterType;
                    model.SerialNumber = meterDB.SERIAL_NUMBER;
                    model.ReleaseDate = meterDB.RELEASE_DATE;
                    model.VarificationDate = meterDB.VARIFICATION_DATE;

                    var meterTypesDB = context.METER_TYPE.ToList();
                    model.MeterTypes = new List<MeterType>();

                    foreach (var ct in meterTypesDB)
                        model.MeterTypes.Add(new MeterType() { Id = ct.ID, Name = ct.NAME, VarificationPeriod = ct.VARIFICATION_PERIOD_YEARS });

                }
                else
                    model = null;

                var view = View("~/Views/Meter/EditMeter.cshtml", model);

                return view;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMeter(Meter meter)
        {           
            using (var context = new TownUtilityBillSystemEntities())
            {
                if (ModelState.IsValid)
                {
                    var meterDB = context.METER.Find(meter.Id);

                    meterDB.SERIAL_NUMBER = meter.SerialNumber;
                    meterDB.RELEASE_DATE = meter.ReleaseDate;
                    meterDB.VARIFICATION_DATE = meter.VarificationDate;
                    meterDB.METER_TYPE_ID = meter.MeterType.Id;

                    context.SaveChanges();

                    return RedirectToAction("ShowFoundMeters", "Meter", new { searchString = meter.SerialNumber });
                }

                var meterTypesDB = context.METER_TYPE.ToList();
                meter.MeterTypes = new List<MeterType>();

                foreach (var ct in meterTypesDB)
                    meter.MeterTypes.Add(new MeterType() { Id = ct.ID, Name = ct.NAME });

                return View(meter);
            }
        }

        public ActionResult EditMeterType(int meterTypeId)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var model = new MeterType();

                var meterTypeDB = context.METER_TYPE.Find(meterTypeId);

                if (meterTypeDB != null)
                {
                    var utilityDB = context.UTILITY.Where(u => u.ID == meterTypeDB.UTILITY_ID).FirstOrDefault();

                    var utility = new Utility() { Id = utilityDB.ID, Name = utilityDB.NAME };

                    model.Id = meterTypeDB.ID;
                    model.Name = meterTypeDB.NAME;
                    model.VarificationPeriod = meterTypeDB.VARIFICATION_PERIOD_YEARS;
                    model.Utility = utility;

                    var utilitiesDB = context.UTILITY.ToList();
                    model.Utilities = new List<Utility>();

                    foreach (var u in utilitiesDB)
                        model.Utilities.Add(new Utility() { Id = u.ID, Name = u.NAME });

                }
                else
                    model = null;

                var view = View("~/Views/Meter/EditMeterType.cshtml", model);

                return view;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMeterType(MeterType meterType)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                if (ModelState.IsValid)
                {
                    var meterTypeDB = context.METER_TYPE.Find(meterType.Id);

                    meterTypeDB.NAME = meterType.Name;
                    meterTypeDB.UTILITY_ID = meterType.Utility.Id;
                    meterTypeDB.VARIFICATION_PERIOD_YEARS = meterType.VarificationPeriod;

                    context.SaveChanges();

                    return RedirectToAction("ShowAllMeterTypes", "Meter");
                }

                var utilitiesDB = context.UTILITY.ToList();
                meterType.Utilities = new List<Utility>();

                foreach (var u in utilitiesDB)
                    meterType.Utilities.Add(new Utility() { Id = u.ID, Name = u.NAME });

                return View();
            }
        }

        public ActionResult GetMetertDataForChart(int meterId)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                List<ChartData> chartData = new List<ChartData>();
                List<MeterItem> meterItemsDB = new List<MeterItem>();

                DateTime presDate = DateTime.Today;
                DateTime startDate = new DateTime();
                DateTime finishDate = new DateTime();

                int startDay = 1;
                int presYear = presDate.Year;
                int presMonth = presDate.Month;
                int prevYear = presYear - 1;
                int prevMonth = presMonth - 1;
                int nextMonth = presMonth + 1;
                float valueDifference;
                string monthName;

                startDate = new DateTime(prevYear, nextMonth, startDay);
                finishDate = new DateTime(presYear, presMonth, startDay);

                var allMeterItemsForMeterDB = context.METER_ITEM.Where(m => m.METER_ID == meterId).ToList();

                foreach (var d in allMeterItemsForMeterDB)
                    meterItemsDB.Add(new MeterItem() { Id = d.ID, Date = d.DATE, Value = d.VALUE });

                if (meterItemsDB.Count != 0)
                {
                    for (; startDate <= finishDate; startDate = startDate.AddMonths(1))
                    {
                        var startValue = meterItemsDB.Where(m => m.Date == startDate.AddMonths(-1)).FirstOrDefault().Value;
                        var finishValue = meterItemsDB.Where(m => m.Date == startDate).FirstOrDefault().Value;

                        valueDifference = (float)Math.Round(finishValue - startValue, 2);

                        if (startDate.Month != 1)
                        {
                            Months month = (Months)(startDate.Month - 1);
                            monthName = month.ToString() + " " + startDate.Year;
                        }
                        else
                        {
                            Months month = Months.December;
                            monthName = month.ToString() + " " + startDate.AddYears(-1).Year;
                        }

                        chartData.Add(new ChartData() { MonthName = monthName, Value = valueDifference });
                    }

                    return Json(chartData, JsonRequestBehavior.AllowGet);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAllUtilitiesDataForChart(int addressId)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                List<ChartData> chartData = new List<ChartData>();
                List<MeterItem> elMeterListDB = new List<MeterItem>();
                List<MeterItem> waterMeterListDB = new List<MeterItem>();
                List<MeterItem> heatMeterListDB = new List<MeterItem>();
                List<MeterItem> gasMeterListDB = new List<MeterItem>();
                List<MeterItem> electricMeterItemsDB = new List<MeterItem>();
                List<MeterItem> waterMeterItemsDB = new List<MeterItem>();
                List<MeterItem> heatMeterItemsDB = new List<MeterItem>();
                List<MeterItem> gasMeterItemsDB = new List<MeterItem>();

                List<int> metersListIds = new List<int>();

                DateTime presDate = DateTime.Today;
                DateTime startDate = new DateTime();
                DateTime finishDate = new DateTime();

                int startDay = 1;
                int presYear = presDate.Year;
                int presMonth = presDate.Month;
                int prevYear = presYear - 1;
                int prevMonth = presMonth - 1;
                int nextMonth = presMonth + 1;
                float valueElDifference;
                float valueWaterDifference;
                float valueHeatDifference;
                float valueGasDifference;
                int elIndex;
                int waterIndex;
                int heatIndex;
                int gasIndex;
                string monthName;

                startDate = new DateTime(prevYear, nextMonth, startDay);
                finishDate = new DateTime(presYear, presMonth, startDay);

                var metersDB = context.METER.Where(m => m.ADDRESS_ID == addressId).ToList();

                foreach (var m in metersDB)
                    metersListIds.Add(m.ID);

                elIndex = metersListIds[(int)Utilities.Electricity - 1];
                waterIndex = metersListIds[(int)Utilities.Water - 1];

                var electricItemsDB = (from m in context.METER_ITEM
                                       where m.METER_ID == elIndex
                                       select m).ToList();

                var waterItemsDB = (from m in context.METER_ITEM
                                    where m.METER_ID == waterIndex
                                    select m).ToList();

                foreach (var i in electricItemsDB)
                    elMeterListDB.Add(new MeterItem() { Id = i.ID, Date = i.DATE, Value = i.VALUE });

                foreach (var i in waterItemsDB)
                    waterMeterListDB.Add(new MeterItem() { Id = i.ID, Date = i.DATE, Value = i.VALUE });

                if (metersListIds.Count > 2)
                {
                    heatIndex = metersListIds[(int)Utilities.Heating - 1];

                    var heatItemsDB = (from m in context.METER_ITEM
                                       where m.METER_ID == heatIndex
                                       select m).ToList();
                    foreach (var i in heatItemsDB)
                        heatMeterListDB.Add(new MeterItem() { Id = i.ID, Date = i.DATE, Value = i.VALUE });
                }

                if (metersListIds.Count > 3)
                {
                    gasIndex = metersListIds[(int)Utilities.Gas - 1];

                    var gasItemsDB = (from m in context.METER_ITEM
                                      where m.METER_ID == gasIndex
                                      select m).ToList();
                    foreach (var i in gasItemsDB)
                        gasMeterListDB.Add(new MeterItem() { Id = i.ID, Date = i.DATE, Value = i.VALUE });
                }
                
                if (electricItemsDB.Count != 0 && waterMeterListDB.Count != 0 && heatMeterListDB.Count != 0 && gasMeterListDB.Count != 0)
                {
                    for (; startDate <= finishDate; startDate = startDate.AddMonths(1))
                    {
                        var startElValue = elMeterListDB.Where(m => m.Date == startDate.AddMonths(-1)).FirstOrDefault().Value;
                        var finishElValue = elMeterListDB.Where(m => m.Date == startDate).FirstOrDefault().Value;
                        valueElDifference = (float)Math.Round(finishElValue - startElValue, 2);


                        var startWaterValue = waterMeterListDB.Where(m => m.Date == startDate.AddMonths(-1)).FirstOrDefault().Value;
                        var finishWaterValue = waterMeterListDB.Where(m => m.Date == startDate).FirstOrDefault().Value;
                        valueWaterDifference = (float)Math.Round(finishWaterValue - startWaterValue, 2);

                        var startHeatValue = heatMeterListDB.Where(m => m.Date == startDate.AddMonths(-1)).FirstOrDefault().Value;
                        var finishHeatValue = heatMeterListDB.Where(m => m.Date == startDate).FirstOrDefault().Value;
                        valueHeatDifference = (float)Math.Round(finishHeatValue - startHeatValue, 2);

                        var startGasValue = gasMeterListDB.Where(m => m.Date == startDate.AddMonths(-1)).FirstOrDefault().Value;
                        var finishGasValue = gasMeterListDB.Where(m => m.Date == startDate).FirstOrDefault().Value;
                        valueGasDifference = (float)Math.Round(finishGasValue - startGasValue, 2);

                        if (startDate.Month != 1)
                        {
                            Months month = (Months)(startDate.Month - 1);
                            monthName = month.ToString() + " " + startDate.Year;
                        }
                        else
                        {
                            Months month = Months.December;
                            monthName = month.ToString() + " " + startDate.AddYears(-1).Year;
                        }

                        chartData.Add(new ChartData() { MonthName = monthName, ElectricValue = valueElDifference, WaterValue = valueWaterDifference, HeatValue = valueHeatDifference, GasValue = valueGasDifference });
                    }

                    var dataforchart = chartData.Select(x => new { name = x.MonthName, elValue = x.ElectricValue, waterValue = x.WaterValue, heatValue = x.HeatValue, gasValue = x.GasValue });

                    return Json(dataforchart, JsonRequestBehavior.AllowGet);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ShowAllUtilityCharts(int customerId)
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

                var view = View("~/Views/Chart/ShowAllUtilityCharts.cshtml", model);

                return view;
            }
        }

        public ActionResult RedirectShowAllUtilityCharts(int meterId)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var meterDB = context.METER.Find(meterId);
                int addressId;
                int customerIdDB = 0;

                if (meterDB != null)
                {
                    addressId = meterDB.ADDRESS_ID;

                    customerIdDB = context.CUSTOMER.Where(c => c.ADDRESS_ID == addressId).FirstOrDefault().ID;

                    return RedirectToAction("ShowAllUtilityCharts", "Meter", new { customerId = customerIdDB });
                }

                var view = View("~/Views/Meter/ShowMeterData.cshtml", meterId);

                return view;


            }

        }


        public ActionResult DeleteMeterType(int meterTypeId)
        {
            ViewBag.Message = CustomizedMessage.NoAdministratorRightsMessage;
            return View();
        }

        public ActionResult CreateMeterType()
        {
            ViewBag.Message = CustomizedMessage.NoAdministratorRightsMessage;
            return View();
        }

        public ActionResult DeleteMeter(int meterId)
        {
            ViewBag.Message = CustomizedMessage.NoAdministratorRightsMessage;
            return View();
        }

        public ActionResult CreateMeter()
        {
            ViewBag.Message = CustomizedMessage.NoAdministratorRightsMessage;
            return View();
        }

        public ActionResult DeleteMeterItem(int meterItemId)
        {
            ViewBag.Message = CustomizedMessage.NoAdministratorRightsMessage;
            return View();
        }

        public ActionResult CreateMeterItem()
        {
            ViewBag.Message = CustomizedMessage.NoAdministratorRightsMessage;
            return View();
        }
    }
}