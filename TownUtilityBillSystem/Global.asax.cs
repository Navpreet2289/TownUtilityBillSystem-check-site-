using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TownUtilityBillSystem.Controllers;

namespace TownUtilityBillSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private InitialDBController initialDbCtrl;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            initialDbCtrl = new InitialDBController();
            initialDbCtrl.FillDataDB();
        }
    }
}
