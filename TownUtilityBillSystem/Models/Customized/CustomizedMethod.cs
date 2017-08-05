using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models
{
    public static class CustomizedMethod
    {
        private static Random rnd = new Random();

        public static void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static string GetFullMonthName(BILL b)
        {
            DateTime billPeriod = new DateTime();
            string fullMonthName = "";

            billPeriod = Convert.ToDateTime(b.PERIOD);
            fullMonthName = billPeriod.ToString("MMMM yyyy");
            return fullMonthName;
        }

        public static string GetUtilityImage(int utilityId)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                IMAGEUTILITY imageDB = null;
                string imageName = "";
                string imagePathForHtml = "";
                string imagePathDB = "";
                string folderName = "";

                var utilityDB = context.UTILITY.Where(b => b.ID == utilityId).FirstOrDefault();

                if (utilityDB != null)
                    imageDB = context.IMAGEUTILITY.Where(i => i.ID == utilityDB.IMAGE_ID).FirstOrDefault();

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

        public static string GetTemperatureIconImage()
        {
            string path = "/Images/UtilityIcons/TemperatureIcon.jpg";

            return path;
        }


        public static string GetNewsImage(int? imageId)
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                string imageName = "";
                string imagePathForHtml = "";
                string imagePathDB = "";
                string folderName = "";

                var imageDB = context.IMAGENEWS.Find(imageId) ?? null;

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

    }
}