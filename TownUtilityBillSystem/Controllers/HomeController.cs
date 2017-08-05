using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TownUtilityBillSystem.Models;
using TownUtilityBillSystem.Models.News;

namespace TownUtilityBillSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new NewsModel();
            using (var context = new TownUtilityBillSystemEntities())
            {
                var newsDB = context.NEWS.ToList();

                foreach (var n in newsDB)
                    model.NewsList.Add(new News() { Id = n.ID, Date = n.DATE, Title = n.TITLE, ImagePath = CustomizedMethod.GetNewsImage(n.IMAGE_ID) });
            }
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            UtilitySupplierModel utilitySupplierModel = new UtilitySupplierModel();

            return View(utilitySupplierModel.UtilitySupplier);
        }

        public ActionResult ShowSingleNews(int newsId)
        {
            var model = new NewsModel();

            using (var context = new TownUtilityBillSystemEntities())
            {
                var newsDB = context.NEWS.Find(newsId);
                var chaptersDB = context.NEWSCHAPTER.Where(nc => nc.NEWS_ID == newsId).ToList();

                model.SingleNews.Title = newsDB.TITLE;
                model.SingleNews.Date = newsDB.DATE;

                foreach (var ch in chaptersDB)
                    model.SingleNews.NewsChapters.Add(new NewsChapter() { Id = ch.ID, Text = ch.TEXT });
            }

            return View(model);
        }

        public ActionResult News()
        {
            var model = new NewsModel();
            using (var context = new TownUtilityBillSystemEntities())
            {
                var newsDB = context.NEWS.ToList();

                foreach (var n in newsDB)
                    model.NewsList.Add(new News() { Id = n.ID, Date = n.DATE, Title = n.TITLE, ImagePath = CustomizedMethod.GetNewsImage(n.IMAGE_ID) });
            }
            return View(model);
        }
    }
}