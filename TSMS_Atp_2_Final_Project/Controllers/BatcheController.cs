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
    public class BatcheController : Controller
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
            TempData["studentassignbatches"] = db.Studentassignbatches.ToList();
            TempData["userdetails"] = db.UserDetails.ToList();
            var batches = db.Batches.Include(b => b.Course);
            return View(batches.ToList());
        }

        [HttpPost]
        public JsonResult Getdata(string batchcode)
        {
            var studentid = (from a in db.Studentassignbatches
                             where a.batch_code == batchcode
                             select new { a.UserId });
            return Json(studentid, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult UserDetailsAsUserId(string UserId)
        {
            var UserDetails = db.UserDetails.SingleOrDefault(x => x.UserId == UserId);
            return Json(UserDetails, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Batche batche = db.Batches.Find(id);

            if (batche == null)
            {
                return HttpNotFound();
            }

            TempData["Courses"] = db.Courses.Where(x => x.name == batche.name).Select(x => x.vendor_heading).First();
            return View(batche);
        }

        public ActionResult Create()
        {
            ViewBag.Day7 = MyCustomFunctions.Day7();
            ViewBag.vendor_heading = new SelectList(db.Courses, "vendor_heading", "vendor_heading");
            ViewBag.faculty_name = new SelectList(db.Instructors, "faculty_name", "faculty_name");
            ViewBag.name = new SelectList(db.Courses, "name", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="batch_code,name,batch_starting_date,admission_last_date,room_number,faculty_name,amount,details,routine")] Batche batche)
        {
            if (ModelState.IsValid)
            {
                if(batche.admission_last_date >= batche.batch_starting_date)
                {
                    ModelState.AddModelError("admission_last_date", "Admission Last Date Cant Be Same As Batch Starting Date Or Greater Than Batch Starting Date!");
                    ViewBag.Day7 = MyCustomFunctions.Day7();
                    ViewBag.vendor_heading = new SelectList(db.Courses, "vendor_heading", "vendor_heading");
                    ViewBag.faculty_name = new SelectList(db.Instructors, "faculty_name", "faculty_name");
                    ViewBag.name = new SelectList(db.Courses, "name", "vendor_heading", batche.name);
                    return View(batche);
                }
                db.Batches.Add(batche);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Day7 = MyCustomFunctions.Day7();
            ViewBag.vendor_heading = new SelectList(db.Courses, "vendor_heading", "vendor_heading");
            ViewBag.faculty_name = new SelectList(db.Instructors, "faculty_name", "faculty_name");
            ViewBag.name = new SelectList(db.Courses, "name", "vendor_heading", batche.name);

            return View(batche);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Batche batche = db.Batches.Find(id);

            if (batche == null)
            {
                return HttpNotFound();
            }

            ViewBag.Day7 = MyCustomFunctions.Day7();
            ViewBag.vendor_headingList = new SelectList(db.Courses, "vendor_heading", "vendor_heading");
            ViewBag.faculty_nameList = new SelectList(db.Instructors, "faculty_name", "faculty_name");
            ViewBag.nameList = new SelectList(db.Courses, "name", "name", batche.name);
            return View(batche);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="batch_code,name,batch_starting_date,admission_last_date,room_number,faculty_name,amount,details,routine")] Batche batche)
        {
            if (ModelState.IsValid)
            {
                db.Entry(batche).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.faculty_name = new SelectList(db.Instructors, "faculty_name", "faculty_name");
            ViewBag.name = new SelectList(db.Courses, "name", "vendor_heading", batche.name);
            return View(batche);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Batche batche = db.Batches.Find(id);

            if (batche == null)
            {
                return HttpNotFound();
            }

            TempData["Courses"] = db.Courses.Where(x => x.name == batche.name).Select(x => x.vendor_heading).First();
            ViewBag.vendor_headingList = new SelectList(db.Courses, "vendor_heading", "vendor_heading");
            return View(batche);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Batche batche = db.Batches.Find(id);
            db.Batches.Remove(batche);
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
