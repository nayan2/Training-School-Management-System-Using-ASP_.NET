using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TSMS_Atp_2_Final_Project.Models.Com.Tsms;

namespace TSMS_Atp_2_Final_Project.Controllers
{
    public class FinanceController : Controller
    {
        private TsmsDBContext db = new TsmsDBContext();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Userid"] != null || Session["Password"] != null)
                base.OnActionExecuting(filterContext);
            else
                filterContext.Result = RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        {
            var financeofstudents = db.Financeofstudents.Include(f => f.Batche).Include(f => f.User);
            return View(financeofstudents.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Financeofstudent financeofstudent = db.Financeofstudents.Find(id);

            if (financeofstudent == null)
            {
                return HttpNotFound();
            }

            return View(financeofstudent);
        }

        public ActionResult Create()
        {
            ViewBag.batch_code = new SelectList(db.Batches, "batch_code", "name");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "password");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,UserId,batch_code,debit,credit,balance,lastTrunsaction")] Financeofstudent financeofstudent)
        {
            if (ModelState.IsValid)
            {
                db.Financeofstudents.Add(financeofstudent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.batch_code = new SelectList(db.Batches, "batch_code", "name", financeofstudent.batch_code);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "password", financeofstudent.UserId);
            return View(financeofstudent);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Financeofstudent financeofstudent = db.Financeofstudents.Find(id);
            if (financeofstudent == null)
            {
                return HttpNotFound();
            }
            ViewBag.batch_code = new SelectList(db.Batches, "batch_code", "name", financeofstudent.batch_code);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "password", financeofstudent.UserId);
            return View(financeofstudent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,UserId,batch_code,debit,credit,balance,lastTrunsaction")] Financeofstudent financeofstudent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(financeofstudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.batch_code = new SelectList(db.Batches, "batch_code", "name", financeofstudent.batch_code);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "password", financeofstudent.UserId);

            return View(financeofstudent);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Financeofstudent financeofstudent = db.Financeofstudents.Find(id);

            if (financeofstudent == null)
            {
                return HttpNotFound();
            }

            return View(financeofstudent);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Financeofstudent financeofstudent = db.Financeofstudents.Find(id);
            db.Financeofstudents.Remove(financeofstudent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
