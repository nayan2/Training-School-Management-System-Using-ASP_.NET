using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSMS_Atp_2_Final_Project.Models.Com.Tsms;

namespace TSMS_Atp_2_Final_Project.Controllers
{
    public class StudentExamController : Controller
    {
        // GET: StudentExam
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Userid"] != null || Session["Password"] != null)
                base.OnActionExecuting(filterContext);
            else
                filterContext.Result = RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            User CurrentUser = (User)Session["CurrentUser"];   
           // Session["Userid"] = "13-25519-2";
            //Session["Level"] = "student";
           //   if ((string)Session["Level"] == "student")
                //{
                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        List<ExamAssign> assign = context.ExamAssigns.Where(p => p.StudentID ==CurrentUser.UserId.ToString() ).ToList();
                        return View(assign);
                    }
            //   // }
            //    else
            //    {
            //        return RedirectToAction("Index", "Exam");
            //    }
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Exam");
            //}
        }

        [HttpGet]
        public ActionResult ExamDetail(int id)
        {
            if ((string)Session["Userid"] != null && (string)Session["Level"] != null)
            {
                if ((string)Session["Level"] == "student")
                {
                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        Exam exam = context.Exams.SingleOrDefault(EId => EId.Id == id);
                        ViewBag.QuestionMakerName = context.UserDetails.Where(p => p.UserId == exam.QuestionMaker).Select(y => y.fullname);
                        return View(exam);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Exam");
                }
            }
            else
            {
                return RedirectToAction("Index", "Exam");
            }
        }
    }
}