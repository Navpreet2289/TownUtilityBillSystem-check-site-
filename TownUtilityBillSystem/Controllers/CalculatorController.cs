using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TownUtilityBillSystem.Models;
using static TownUtilityBillSystem.Controllers.InitialDBController;

namespace TownUtilityBillSystem.Controllers
{
    public class CalculatorController : Controller
    {
        public ActionResult ShowCalculatorOnLine()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                CalculatorItemModel model = AddUtilitiesToModel(context);
                return View(model);
            }
        }

        private static CalculatorItemModel AddUtilitiesToModel(TownUtilityBillSystemEntities context)
        {
            var model = new CalculatorItemModel();
            var utilitiesDB = context.UTILITY.ToList();

            foreach (var item in utilitiesDB)
            {
                var unitDB = context.UNIT.Where(u => u.ID == item.UNIT_ID).FirstOrDefault();
                var unit = new Unit() { Id = unitDB.ID, Name = unitDB.NAME };

                if (item.USAGEFORSTANDARTPRICE != null && item.BIGUSAGEPRICE != null)
                    model.Utilities.Add(new Utility { Id = item.ID, Name = item.NAME, Price = item.PRICE, BigUsagePrice = (decimal)item.BIGUSAGEPRICE, UsageForStandartPrice = Math.Round((decimal)item.USAGEFORSTANDARTPRICE, 0), ImageIconPath = CustomizedMethod.GetUtilityImage(item.ID), Unit = unit });

                else
                    model.Utilities.Add(new Utility { Id = item.ID, Name = item.NAME, Price = item.PRICE, ImageIconPath = CustomizedMethod.GetUtilityImage(item.ID), Unit = unit });
            }

            return model;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowCalculatorOnLine(CalculatorItemModel inputModel)
        {
            var model = new CalculatorItemModel();

            using (var context = new TownUtilityBillSystemEntities())
            {
                model = AddUtilitiesToModel(context);

                if (ModelState.IsValid)
                {
                    if (inputModel.ElectricUsage == 0 && inputModel.WaterUsage == 0 && inputModel.HeatUsage == 0 && inputModel.GasUsage == 0)
                    {
                        ViewBag.ErrorMessage = "You must enter at least 1 utility usage";
                        return View(model);
                    }

                    var elUtilityDB = context.UTILITY.Find((int)Utilities.Electricity);
                    var waterUtilityDB = context.UTILITY.Find((int)Utilities.Water);
                    var heatUtilityDB = context.UTILITY.Find((int)Utilities.Heating);
                    var gasUtilityDB = context.UTILITY.Find((int)Utilities.Gas);

                    if (inputModel.ElectricUsage > (float)elUtilityDB.USAGEFORSTANDARTPRICE)
                        model.ElCharges = (float)elUtilityDB.USAGEFORSTANDARTPRICE * (float)elUtilityDB.PRICE + (inputModel.ElectricUsage - (float)elUtilityDB.USAGEFORSTANDARTPRICE) * (float)elUtilityDB.BIGUSAGEPRICE;
                    else
                        model.ElCharges = inputModel.ElectricUsage * (float)elUtilityDB.PRICE;

                    model.WaterCharges = inputModel.WaterUsage * (float)waterUtilityDB.PRICE;
                    model.HeatCharges = inputModel.HeatUsage * (float)heatUtilityDB.PRICE;
                    model.GasCharges = inputModel.GasUsage * (float)gasUtilityDB.PRICE;

                    model.ElectricUsage = inputModel.ElectricUsage;
                    model.WaterUsage = inputModel.WaterUsage;
                    model.HeatUsage = inputModel.HeatUsage;
                    model.GasUsage = inputModel.GasUsage;

                    model.TotalCharges = model.ElCharges + model.WaterCharges + model.HeatCharges + model.GasCharges;

                    var view = View("~/Views/Calculator/ShowCharges.cshtml", model);

                    return view;
                }

                return View(model);
            }
        }


    }
}