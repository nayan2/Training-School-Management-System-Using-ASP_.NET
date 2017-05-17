using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSMS_Atp_2_Final_Project.Models.Com.Tsms;
using System.Net;
using System.Web.Caching;
using System.Dynamic;
using System.IO;

namespace TSMS_Atp_2_Final_Project.Models.Com.Tsms
{
    public class MyCustomFunctions : Controller
    {
        internal static string ValidateFile(HttpPostedFileBase file, string foldertosave, string [] minetypevalidation, int filesize)
        {
            string CreateNewPath = null;
            if (file != null)
            {
                string pic = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
                CreateNewPath = "~/Content/img/tsms/" + foldertosave + "/" + pic;
                if (!(minetypevalidation.Contains(file.ContentType.ToString())))
                {
                    return "Invalid File Type!";
                }
                else if (file.ContentLength > (filesize * 1024*1024))
                {
                    return "Max File Size Is 5 MB!";
                }
            }
            return CreateNewPath;
        }

        //internal void SaveNewAndRemoveOldFile(HttpPostedFileBase file, string foldertosave, string oldfilepath)
        //{
        //    string pic = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
        //    string CreateNewPath = "~/Content/img/tsms/"+foldertosave+"/" + pic;
        //    if (System.IO.File.Exists(oldfilepath))
        //    {
        //        System.IO.File.Delete(oldfilepath);
        //    }
        //    string path = Path.Combine(Server.MapPath("~/Content/img/tsms/course/"), pic);
        //    file.SaveAs(path);
        //}

        internal static List<SelectListItem> UserActivity ()
        {
            List<SelectListItem> user_active = new List<SelectListItem>
            {
                new SelectListItem { Text = "Active", Value = "Active", Selected = true },
                new SelectListItem { Text = "Inactive", Value = "Inactive" }
            };

            return user_active;
        }

        internal static List<SelectListItem> Day7()
        {
            List<SelectListItem> Day7 = new List<SelectListItem>
            {
                new SelectListItem { Text = "Saturday", Value = "Saturday" },
                new SelectListItem { Text = "Sunday", Value = "Sunday" },
                new SelectListItem { Text = "Monday", Value = "Monday" },
                new SelectListItem { Text = "Tuesday", Value = "Tuesday" },
                new SelectListItem { Text = "Wednesday", Value = "Wednesday" },
                new SelectListItem { Text = "Thursday", Value = "Thursday" },
                new SelectListItem { Text = "Friday", Value = "Friday" }
            };

            return Day7;
        }

        internal static string GenerateUserId()
        {
            string YearLastTwo = DateTime.Now.Year.ToString().Substring(2, 2);
            int month;

            if ((int)DateTime.Now.Month < 4)
            {
                month = 1;
            }
            else if ((int)DateTime.Now.Month > 8)
            {
                month = 3;
            }
            else
            {
                month = 2;
            }

            Random r = new Random();
            int RandNum = r.Next(999999);
            string userid = YearLastTwo + "-" + RandNum.ToString("D5").ToString() + "-" + month.ToString();

            return userid;
        }

    }
}