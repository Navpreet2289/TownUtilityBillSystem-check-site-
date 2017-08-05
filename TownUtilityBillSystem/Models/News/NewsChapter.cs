using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models.News
{
    public class NewsChapter
    {
        public int Id { get; set; }
        public string Text{ get; set; }
        public int News_Id { get; set; }
    }
}