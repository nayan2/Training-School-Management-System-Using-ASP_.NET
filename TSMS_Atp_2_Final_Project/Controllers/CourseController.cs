using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TSMS_Atp_2_Final_Project.Models.Com.Tsms;

namespace TSMS_Atp_2_Final_Project.Controllers
{
    public class CourseController : Controller
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
            TempData["batches"] = db.Batches.ToList();
            var courses = db.Courses.Include(c => c.Vendor);
            return View(courses.ToList());
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = db.Courses.Find(id);

            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        public ActionResult Create()
        {
            ViewBag.vendor_heading = new SelectList(db.Vendors, "heading", "heading");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name,vendor_heading,code,pic_path,adding_date,details")] Course course, HttpPostedFileBase pic_path)
        {
            string pic = null;
            var SupportFileType = new[] { "image/gif", "image/x-png", "image/jpeg", "image/png" };

            if (ModelState.IsValid)
            {
                if (SupportFileType.Contains(pic_path.ContentType.ToLower().ToString()) == false)
                {
                    ModelState.AddModelError("pic_path", "Invalid File Type!");
                    ViewBag.vendor_heading = new SelectList(db.Vendors, "heading", "heading", course.vendor_heading);
                    return View(course);
                }
                else if (pic_path.ContentLength > (5 * 1024 * 1024))
                {
                    ModelState.AddModelError("pic_path", "Max File Size Is 5 MB!");
                    ViewBag.vendor_heading = new SelectList(db.Vendors, "heading", "heading", course.vendor_heading);
                    return View(course);
                }

                pic = Guid.NewGuid().ToString() + Path.GetFileName(pic_path.FileName);
                string CreateNewPath = "~/Content/img/tsms/course/" + pic;

                db.Courses.Add(new Course {
                    name = course.name,
                    vendor_heading = course.vendor_heading,
                    code = course.code,
                    pic_path = CreateNewPath,
                    adding_date = DateTime.Now,
                    details = course.details
                });
                try
                {
                    db.SaveChanges();
                    if (pic_path != null)
                    {
                        string path = System.IO.Path.Combine(Server.MapPath("~/Content/img/tsms/course/"), pic);
                        pic_path.SaveAs(path);
                    }
                }
                catch(System.Data.SqlClient.SqlException e)
                {
                    ModelState.AddModelError(null, e.ToString());
                    ViewBag.vendor_heading = new SelectList(db.Vendors, "heading", "heading", course.vendor_heading);
                    return View(course);
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                {
                    ModelState.AddModelError(null, e.ToString());
                    ViewBag.vendor_heading = new SelectList(db.Vendors, "heading", "heading", course.vendor_heading);
                    return View(course);
                }
                return RedirectToAction("Index");
            }

            ViewBag.vendor_heading = new SelectList(db.Vendors, "heading", "heading", course.vendor_heading);
            return View(course);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = db.Courses.Find(id);

            if (course == null)
            {
                return HttpNotFound();
            }

            ViewBag.vendor_headingList = new SelectList(db.Vendors, "heading", "heading", course.vendor_heading);
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Modifyedpic_path,name,vendor_heading,code,pic_path,details")] Course course, HttpPostedFileBase Modifyedpic_path)
        {
            string pic = null;
            var SupportFileType = new[] { "image/gif", "image/x-png", "image/jpeg", "image/png" };
            Course oldcourse = db.Courses.SingleOrDefault(m => m.name == course.name);
            string oldPath = Request.MapPath(oldcourse.pic_path);

            if (Modifyedpic_path != null)
            {
                if (SupportFileType.Contains(Modifyedpic_path.ContentType.ToLower().ToString()) == false)
                {
                    ModelState.AddModelError("pic_path", "Invalid File Type!");
                    ViewBag.vendor_headingList = new SelectList(db.Vendors, "heading", "heading", course.vendor_heading);
                    return View(course);
                }
                else if (Modifyedpic_path.ContentLength > (5 * 1024 * 1024))
                {
                    ModelState.AddModelError("pic_path", "Max File Size Is 5 MB!");
                    ViewBag.vendor_headingList = new SelectList(db.Vendors, "heading", "heading", course.vendor_heading);
                    return View(course);
                }

                pic = Guid.NewGuid().ToString() + Path.GetFileName(Modifyedpic_path.FileName);
                string CreateNewPath = "~/Content/img/tsms/course/" + pic;

                oldcourse.vendor_heading = course.vendor_heading;
                oldcourse.code = course.code;
                oldcourse.pic_path = CreateNewPath;
                oldcourse.details = course.details;
            }
            else
            {
                TryUpdateModel(oldcourse, new string[] { "name", "vendor_heading", "code", "pic_path", "details" });
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.SaveChanges();
                    if (Modifyedpic_path != null)
                    {
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                        string path = Path.Combine(Server.MapPath("~/Content/img/tsms/course/"), pic);
                        Modifyedpic_path.SaveAs(path);
                    }
                    return RedirectToAction("Index");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    ModelState.AddModelError("", ex.ToString());
                    ViewBag.vendor_headingList = new SelectList(db.Vendors, "heading", "heading", course.vendor_heading);
                    return View(course);
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    ModelState.AddModelError("", ex.ToString());
                    ViewBag.vendor_headingList = new SelectList(db.Vendors, "heading", "heading", course.vendor_heading);
                    return View(course);
                }
            }

            ViewBag.vendor_heading = new SelectList(db.Vendors, "heading", "heading", course.vendor_heading);
            return View(course);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = db.Courses.Find(id);

            if (course == null)
            {
                return HttpNotFound();
            }

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            if (db.SaveChanges() > 0)
            {
                if (System.IO.File.Exists(Request.MapPath(course.pic_path)))
                {
                    System.IO.File.Delete(Request.MapPath(course.pic_path));
                }
            }
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
