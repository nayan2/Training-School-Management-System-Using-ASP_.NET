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
    public class InstructorController : Controller
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
            TempData["Batches"] = db.Batches.ToList();
            var instructors = db.Instructors.Include(i => i.Batche).Include(i => i.Course);
            return View(instructors.ToList());
        }

        public ActionResult Details( int id = 0, string name = null)
        {
            Response.Write(id+" "+name);
            if (id == 0 && name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Instructor instructor = db.Instructors.Where(x => x.InstructorId == id || x.faculty_name.Equals(name)).SingleOrDefault();

            if (instructor == null)
            {
                return HttpNotFound();
            }

            ViewBag.faculty_active = MyCustomFunctions.UserActivity();
            return View(instructor);
        }

        public ActionResult Create()
        {
            ViewBag.faculty_active = MyCustomFunctions.UserActivity();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pic_path,first_name,last_name,faculty_name,company_name,city,phone_number,email,zip_code,nationality,sex,religion,blood_group,dob,faculty_active")] Instructor instructor, HttpPostedFileBase pic_path)
        {
            var SupportFileType = new[] { "image/gif", "image/x-png", "image/jpeg", "image/png" };

            if (ModelState.IsValid)
            {
                if (SupportFileType.Contains(pic_path.ContentType.ToLower().ToString()) == false)
                {
                    ModelState.AddModelError("pic_path", "Invalid File Type!");
                    ViewBag.faculty_active = MyCustomFunctions.UserActivity();
                    return View(instructor);
                }
                else if (pic_path.ContentLength > (5 * 1024 * 1024))
                {
                    ModelState.AddModelError("pic_path", "Max File Size Is 5 MB!");
                    ViewBag.faculty_active = MyCustomFunctions.UserActivity();
                    return View(instructor);
                }

                string pic = Guid.NewGuid().ToString() + Path.GetFileName(pic_path.FileName);
                string CreateNewPath = "~/Content/img/tsms/instructor/" + pic;
                db.Instructors.Add(new Instructor
                {
                    faculty_name = instructor.first_name + " " + instructor.last_name,
                    pic_path = CreateNewPath,
                    first_name = instructor.first_name,
                    last_name = instructor.last_name,
                    company_name = instructor.company_name,
                    city = instructor.city,
                    phone_number = instructor.phone_number,
                    email = instructor.email,
                    zip_code = instructor.zip_code,
                    nationality = instructor.nationality,
                    sex = instructor.sex,
                    religion = instructor.religion,
                    blood_group = instructor.blood_group,
                    dob = instructor.dob,
                    faculty_activation_date = DateTime.Now,
                    faculty_active = instructor.faculty_active,
                });
                try
                {
                    db.SaveChanges();
                    if (pic_path != null)
                    {
                        string path = System.IO.Path.Combine(Server.MapPath("~/Content/img/tsms/instructor/"), pic);
                        pic_path.SaveAs(path);
                    }
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    ModelState.AddModelError(null, e.ToString());
                    ViewBag.faculty_active = MyCustomFunctions.UserActivity();
                    return View(instructor);
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                {
                    ModelState.AddModelError(null, e.ToString());
                    ViewBag.faculty_active = MyCustomFunctions.UserActivity();
                    return View(instructor);
                }
                return RedirectToAction("Index");
            }

            ViewBag.faculty_active = MyCustomFunctions.UserActivity();
            return View(instructor);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Instructor instructor = db.Instructors.SingleOrDefault(m => m.InstructorId == id);

            if (instructor == null)
            {
                return HttpNotFound();
            }

            ViewBag.faculty_active = MyCustomFunctions.UserActivity();
            return View(instructor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Modifyedpic_path,InstructorId,pic_path,first_name,last_name,faculty_name,company_name,city,phone_number,email,zip_code,nationality,sex,religion,blood_group,dob,faculty_inactivation_date,faculty_active")] Instructor instructor, HttpPostedFileBase Modifyedpic_path)
        {
            string pic = null;
            var SupportFileType = new[] { "image/gif", "image/x-png", "image/jpeg", "image/png" };

            Instructor oldinstructor = db.Instructors.SingleOrDefault(m => m.InstructorId == instructor.InstructorId);
            string oldPath = Request.MapPath(oldinstructor.pic_path);

            if (Modifyedpic_path != null)
            {
                if (SupportFileType.Contains(Modifyedpic_path.ContentType.ToLower().ToString()) == false)
                {
                    ModelState.AddModelError("pic_path", "Invalid File Type!");
                    ViewBag.faculty_active = MyCustomFunctions.UserActivity();
                    return View(instructor);
                }
                else if (Modifyedpic_path.ContentLength > (5 * 1024 * 1024))
                {
                    ModelState.AddModelError("pic_path", "Max File Size Is 5 MB!");
                    ViewBag.faculty_active = MyCustomFunctions.UserActivity();
                    return View(instructor);
                }

                pic = Guid.NewGuid().ToString() + Path.GetFileName(Modifyedpic_path.FileName);
                string CreateNewPath = "~/Content/img/tsms/instructor/" + pic;

                oldinstructor.faculty_name = instructor.first_name + " " + instructor.last_name;
                oldinstructor.pic_path = CreateNewPath;
                oldinstructor.first_name = instructor.first_name;
                oldinstructor.last_name = instructor.last_name;
                oldinstructor.company_name = instructor.company_name;
                oldinstructor.city = instructor.city;
                oldinstructor.phone_number = instructor.phone_number;
                oldinstructor.email = instructor.email;
                oldinstructor.zip_code = instructor.zip_code;
                oldinstructor.nationality = instructor.nationality;
                oldinstructor.sex = instructor.sex;
                oldinstructor.religion = instructor.religion;
                oldinstructor.blood_group = instructor.blood_group;
                oldinstructor.dob = instructor.dob;
                oldinstructor.faculty_inactivation_date = instructor.faculty_inactivation_date;
                oldinstructor.faculty_active = instructor.faculty_active;
            }
            else
            {
                TryUpdateModel(oldinstructor, new string[] { "pic_path", "first_name", "last_name", "faculty_name", "company_name", "city", "phone_number", "email", "zip_code", "nationality", "sex", "religion", "blood_group", "dob", "faculty_inactivation_date", "faculty_active" });
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
                        string path = Path.Combine(Server.MapPath("~/Content/img/tsms/instructor/"), pic);
                        Modifyedpic_path.SaveAs(path);
                    }
                    return RedirectToAction("Index");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    ModelState.AddModelError(null, ex.ToString());
                    ViewBag.faculty_active = MyCustomFunctions.UserActivity();
                    return View(instructor);
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    ModelState.AddModelError(null, ex.ToString());
                    ViewBag.faculty_active = MyCustomFunctions.UserActivity();
                    return View(instructor);
                }
            }

            ViewBag.faculty_active = MyCustomFunctions.UserActivity();
            return View(instructor);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Instructor instructor = db.Instructors.SingleOrDefault(m => m.InstructorId == id);

            if (instructor == null)
            {
                return HttpNotFound();
            }

            ViewBag.faculty_active = MyCustomFunctions.UserActivity();
            return View(instructor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructor instructor = db.Instructors.SingleOrDefault(m => m.InstructorId == id);
            db.Instructors.Remove(instructor);
            if (db.SaveChanges() > 0)
            {
                if (System.IO.File.Exists(Request.MapPath(instructor.pic_path)))
                {
                    System.IO.File.Delete(Request.MapPath(instructor.pic_path));
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
