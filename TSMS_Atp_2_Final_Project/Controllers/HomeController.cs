using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TSMS_Atp_2_Final_Project.Models.Com.Tsms;
using System.ComponentModel.DataAnnotations;

namespace TSMS_Atp_2_Final_Project.Controllers
{
    public class HomeController : Controller
    {
        private TsmsDBContext db = new TsmsDBContext();

        [HttpGet]
        public ActionResult Index()
        {
            return View("Index", "_TSMSLayout");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "UserId,password")] User user)
        {
            if (string.IsNullOrEmpty(user.UserId) || string.IsNullOrEmpty(user.password))
            {
                ViewBag.Message = null;
                return View("Index", "_TSMSLayout", user);
            }
            else
            {
                User CurrentUser = db.Users.SingleOrDefault(d => d.UserId == user.UserId && d.password == user.password);

                if (CurrentUser != null)
                {
                    Session["CurrentUser"] = CurrentUser;
                    Session["Userid"] = CurrentUser.UserId;
                    Session["Password"] = CurrentUser.password;
                    if (new string[] { "admin", "Admin" }.Contains(CurrentUser.level))
                    {
                        UserDetail userdetail = db.UserDetails.SingleOrDefault(c => c.UserId == CurrentUser.UserId);
                        Session["UserDetailid"] = userdetail.id;
                        return this.RedirectToAction("Index", "Admin");

                    }
                    else
                    {
                        UserDetail userdetail = db.UserDetails.SingleOrDefault(c => c.UserId == CurrentUser.UserId);
                        Session["userdetail"] = userdetail;
                        Session["UserDetailid"] = userdetail.id;
                        return this.RedirectToAction("Index", "Student");
                    }
                }
                else
                {
                    ViewBag.Message = "Invalid UserId Or Password!";
                    return View("Index", "_TSMSLayout", user);
                }
            }
        }

	}
}