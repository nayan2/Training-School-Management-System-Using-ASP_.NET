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
    public class AdminsController : Controller
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
            var userdetails = db.UserDetails.Where(x => x.User.level == "admin").Include(u => u.User);
            return View(userdetails.ToList());
        }

        public ActionResult Details(int ? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserDetail userdetail = db.UserDetails.Find(id);

            if (userdetail == null)
            {
                return HttpNotFound();
            }

            ViewBag.user_active = MyCustomFunctions.UserActivity();
            return View(userdetail);
        }

        public ActionResult Create()
        {
            ViewBag.user_active = MyCustomFunctions.UserActivity();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fullname,first_name,last_name,pic_path,company_name,city,phone_number,email,zip_code,nationality,sex,religion,blood_group,dob,user_activation_date")] UserDetail userdetail, HttpPostedFileBase pic_path)
        {
            ///create a student so level is student.....
            var SupportFileType = new[] { "image/gif", "image/x-png", "image/jpeg", "image/png" };

            if (ModelState.IsValid)
            {
                if (SupportFileType.Contains(pic_path.ContentType.ToLower().ToString()) == false)
                {
                    ModelState.AddModelError("pic_path", "Invalid File Type!");
                    ViewBag.user_active = MyCustomFunctions.UserActivity();
                    return View(userdetail);
                }
                else if (pic_path.ContentLength > (5 * 1024 * 1024))
                {
                    ModelState.AddModelError("pic_path", "Max File Size Is 5 MB!");
                    ViewBag.user_active = MyCustomFunctions.UserActivity();
                    return View(userdetail);
                }

                string pic = Guid.NewGuid().ToString() + Path.GetFileName(pic_path.FileName);
                string CreateNewPath = "~/Content/img/tsms/admin/" + pic;
                string userid = MyCustomFunctions.GenerateUserId();
                db.UserDetails.Add(new UserDetail
                {
                    UserId = userid,
                    first_name = userdetail.first_name,
                    last_name = userdetail.last_name,
                    fullname = userdetail.fullname,
                    pic_path = CreateNewPath,
                    company_name = userdetail.company_name,
                    city = userdetail.city,
                    phone_number = userdetail.phone_number,
                    email = userdetail.email,
                    zip_code = userdetail.zip_code,
                    nationality = userdetail.nationality,
                    sex = userdetail.sex,
                    religion = userdetail.religion,
                    blood_group = userdetail.blood_group,
                    dob = userdetail.dob,
                    user_activation_date = DateTime.Now,
                    user_active = userdetail.user_active,
                    User = new User()
                    {
                        UserId = userid,
                        password = "admin",
                        level = "admin"
                    }
                });
                try
                {
                    db.SaveChanges();
                    if (pic_path != null)
                    {
                        string path = System.IO.Path.Combine(Server.MapPath("~/Content/img/tsms/admin/"), pic);
                        pic_path.SaveAs(path);
                    }
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    ModelState.AddModelError(null, e.ToString());
                    ViewBag.user_active = MyCustomFunctions.UserActivity();
                    return View(userdetail);
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                {
                    ModelState.AddModelError(null, e.ToString());
                    ViewBag.user_active = MyCustomFunctions.UserActivity();
                    return View(userdetail);
                }
                return RedirectToAction("Index");
            }

            ViewBag.user_active = MyCustomFunctions.UserActivity();
            return View(userdetail);
        }

        public ActionResult Edit(int ? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserDetail userdetail = db.UserDetails.Find(id);

            if (userdetail == null)
            {
                return HttpNotFound();
            }

            ViewBag.user_active = MyCustomFunctions.UserActivity();
            return View(userdetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Modifyedpic_path,fullname,first_name,last_name,pic_path,company_name,city,phone_number,email,zip_code,nationality,sex,religion,blood_group,dob,user_deactivation_date,user_active")] UserDetail userdetail, HttpPostedFileBase Modifyedpic_path)
        {
            string pic = null;
            var SupportFileType = new[] { "image/gif", "image/x-png", "image/jpeg", "image/png" };

            //find old userdetails//
            UserDetail olduserdetail = db.UserDetails.SingleOrDefault(m => m.id == userdetail.id);
            string oldPath = Request.MapPath(olduserdetail.pic_path);

            if (Modifyedpic_path != null)
            {
                if (SupportFileType.Contains(Modifyedpic_path.ContentType.ToLower().ToString()) == false)
                {
                    ModelState.AddModelError("pic_path", "Invalid File Type!");
                    ViewBag.user_active = MyCustomFunctions.UserActivity();
                    return View(userdetail);
                }
                else if (Modifyedpic_path.ContentLength > (5 * 1024 * 1024))
                {
                    ModelState.AddModelError("pic_path", "Max File Size Is 5 MB!");
                    ViewBag.user_active = MyCustomFunctions.UserActivity();
                    return View(userdetail);
                }

                pic = Guid.NewGuid().ToString() + Path.GetFileName(Modifyedpic_path.FileName);
                string CreateNewPath = "~/Content/img/tsms/admin/" + pic;

                olduserdetail.first_name = userdetail.first_name;
                olduserdetail.last_name = userdetail.last_name;
                olduserdetail.fullname = userdetail.fullname;
                olduserdetail.pic_path = CreateNewPath;
                olduserdetail.company_name = userdetail.company_name;
                olduserdetail.city = userdetail.city;
                olduserdetail.phone_number = userdetail.phone_number;
                olduserdetail.email = userdetail.email;
                olduserdetail.zip_code = userdetail.zip_code;
                olduserdetail.nationality = userdetail.nationality;
                olduserdetail.sex = userdetail.sex;
                olduserdetail.religion = userdetail.religion;
                olduserdetail.blood_group = userdetail.blood_group;
                olduserdetail.dob = userdetail.dob;
                olduserdetail.user_deactivation_date = userdetail.user_deactivation_date;
                olduserdetail.user_active = userdetail.user_active;
            }
            else
            {
                TryUpdateModel(olduserdetail, new string[] { "fullname", "first_name", "last_name", "pic_path", "company_name", "city", "phone_number", "email", "zip_code", "nationality", "sex", "religion", "blood_group", "dob", "user_deactivation_date", "user_active" });
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
                        string path = Path.Combine(Server.MapPath("~/Content/img/tsms/admin/"), pic);
                        Modifyedpic_path.SaveAs(path);
                    }
                    return RedirectToAction("Index");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    ModelState.AddModelError(null, ex.ToString());
                    ViewBag.user_active = MyCustomFunctions.UserActivity();
                    return View(userdetail);
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    ModelState.AddModelError(null, ex.ToString());
                    ViewBag.user_active = MyCustomFunctions.UserActivity();
                    return View(userdetail);
                }
            }

            ViewBag.user_active = MyCustomFunctions.UserActivity();
            return View(userdetail);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserDetail userdetail = db.UserDetails.Find(id);

            if (userdetail == null)
            {
                return HttpNotFound();
            }

            return View(userdetail);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserDetail userdetail = db.UserDetails.Find(id);
            db.UserDetails.Remove(userdetail);
            if (db.SaveChanges() > 0)
            {
                if (System.IO.File.Exists(Request.MapPath(userdetail.pic_path)))
                {
                    System.IO.File.Delete(Request.MapPath(userdetail.pic_path));
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
