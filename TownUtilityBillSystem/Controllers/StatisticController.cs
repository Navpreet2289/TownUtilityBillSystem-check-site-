using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TownUtilityBillSystem.Models;

namespace TownUtilityBillSystem.Controllers
{
    public class StatisticController : Controller
    {
        public ActionResult ShowStatisticMenu()
        {
            return View();         
        }
    }
}