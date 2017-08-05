using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models.News
{
    public class NewsModel
    {  
        public News SingleNews;
        public List<News> NewsList;
        public NewsEmployee NewsEmployee;

        public NewsModel()
        {
            SingleNews = new News();
            NewsList = new List<News>();
            NewsEmployee = new NewsEmployee();
            SingleNews.NewsChapters = new List<NewsChapter>();
        }

    }
}