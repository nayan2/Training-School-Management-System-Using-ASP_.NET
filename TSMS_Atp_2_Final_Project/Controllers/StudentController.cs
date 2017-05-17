using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TSMS_Atp_2_Final_Project.Controllers
{
    public class StudentController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Userid"] != null || Session["Password"] != null)
                base.OnActionExecuting(filterContext);
            else
                filterContext.Result = RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        {
            return View();
        }
	}
}