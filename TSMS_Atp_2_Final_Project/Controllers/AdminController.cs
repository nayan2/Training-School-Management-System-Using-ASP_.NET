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
using System.ComponentModel.DataAnnotations;

namespace TSMS_Atp_2_Final_Project.Controllers
{
    public class AdminController : Controller
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
           int? userid = (int)Session["UserDetailid"];
           UserDetail userdetail = db.UserDetails.Find(userid);

           if(userdetail == null)
           {
              return HttpNotFound("Invalid User!");
           }
           
           Session["userdetail"] = userdetail;
           return View(db.Vendors.ToList());
        }

        public ActionResult Anotherlink()
        {
            return this.View();
        }

        public ActionResult Logout()
        {
            Session["Userid"] = null;
            Session["Password"] = null;
            Session.Clear();
            Session.Abandon();
            return this.RedirectToAction("Index", "Home");
        }

        public ActionResult UserProfile()
        {
            int? id = (int)Session["UserDetailid"];

            UserDetail userdetail = db.UserDetails.Find(id); 

            if(userdetail == null)
            {
                //return HttpNotFound("Invalid User!");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(userdetail);

        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            int ? id = (int)Session["UserDetailid"];

            UserDetail userdetail = db.UserDetails.Find(id);
            User user = db.Users.SingleOrDefault(c=> c.UserId == userdetail.UserId);

            if(userdetail == null || user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = "password,password0,password1")] User user, string password0, string password1)
        {
            if (password1 != user.password)
            {
                ViewBag.Message = "New Password And Confirm Password Is Not Match!";
                return View(user);
            }
            else
            {
                int? id = (int)Session["UserDetailid"];

                UserDetail userdetail = db.UserDetails.Find(id);

                User olduser = db.Users.SingleOrDefault(c => c.UserId == userdetail.UserId);

                if (userdetail == null || user == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (olduser.password != password0)
                {
                    ViewBag.Message = "Old Password Is Not Match!";
                    return View(user);
                }
                else
                {
                    TryUpdateModel(olduser, new string[] { "password" });
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "heading,pic_path,adding_date,body")] Vendor newvendor, HttpPostedFileBase pic_path)
        {
            var SupportFileType = new[] { "image/gif", "image/x-png", "image/jpeg", "image/png" };

            if (ModelState.IsValid)
            {
                if (SupportFileType.Contains(pic_path.ContentType.ToLower().ToString()) == false)
                {
                    ModelState.AddModelError("pic_path", "Invalid File Type!");
                    return View(newvendor);
                }
                else if (pic_path.ContentLength > (5 * 1024 * 1024))
                {
                    ModelState.AddModelError("pic_path", "Max File Size Is 5 MB!");
                    return View(newvendor);
                }

                string pic = Guid.NewGuid().ToString() + Path.GetFileName(pic_path.FileName);
                string CreateNewPath = "~/Content/img/tsms/vendor/" + pic;
                db.Vendors.Add(new Vendor
                {
                    heading = newvendor.heading,
                    pic_path = CreateNewPath,
                    adding_date = newvendor.adding_date,
                    body = newvendor.body
                });

                try
                {
                    db.SaveChanges();
                    if (pic_path != null)
                    {
                        string path = System.IO.Path.Combine(Server.MapPath("~/Content/img/tsms/vendor/"), pic);
                        pic_path.SaveAs(path);
                    }
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    ModelState.AddModelError(null, e.ToString());
                    return View(newvendor);
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    ModelState.AddModelError("heading", "A Vendor With Same Name Already Available!");
                    return View(newvendor);
                }
                return RedirectToAction("Index");
            }
            return View(newvendor);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            if(id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Vendor findvendor = db.Vendors.SingleOrDefault(c => c.heading == id);

                if(findvendor == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    return View(findvendor);
                }
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Vendor vendor = db.Vendors.Find(id);
            db.Vendors.Remove(vendor);
            if (db.SaveChanges() > 0)
            {
                if (System.IO.File.Exists(Request.MapPath(vendor.pic_path)))
                {
                    System.IO.File.Delete(Request.MapPath(vendor.pic_path));
                }
            }
            return RedirectToAction("Manage");
        }

        [HttpGet]
        public ActionResult Manage()
        {
            var course = from a in db.Courses select a;
            TempData["course"] = course;
            return View(db.Vendors.ToList());
        }

        [HttpGet]
        public ActionResult EditUserProfile(int ? id)
        {
            if((id ?? 0) == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }

            UserDetail userdetail = db.UserDetails.SingleOrDefault(m=> m.id == id);
            ViewBag.user_activeList = MyCustomFunctions.UserActivity();
            return View(userdetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserProfile([Bind(Include = "id,pic_path,fullname,first_name,last_name,company_name,city,phone_number,email,zip_code,nationality,sex,religion,blood_group,dob,user_active,Modifyedpic_path")] UserDetail userdetail, HttpPostedFileBase Modifyedpic_path)
        {
            string pic = null;
            var SupportFileType = new[] { "image/gif", "image/x-png", "image/jpeg", "image/png" };
            UserDetail Olduserdetail = db.UserDetails.SingleOrDefault(m => m.id == userdetail.id);
            string oldPath = Request.MapPath(Olduserdetail.pic_path);

            if (Modifyedpic_path != null)
            {
                if (SupportFileType.Contains(Modifyedpic_path.ContentType.ToLower().ToString()) == false)
                {
                    ModelState.AddModelError("pic_path", "Invalid File Type!");
                    ViewBag.user_activeList = MyCustomFunctions.UserActivity();
                    return View(userdetail);
                }
                else if (Modifyedpic_path.ContentLength > (5 * 1024 * 1024))
                {
                    ModelState.AddModelError("pic_path", "Max File Size Is 5 MB!");
                    ViewBag.user_activeList = MyCustomFunctions.UserActivity();
                    return View(userdetail);
                }

                pic = Guid.NewGuid().ToString() + Path.GetFileName(Modifyedpic_path.FileName);
                string CreateNewPath = "~/Content/img/tsms/admin/" + pic;

                Olduserdetail.pic_path = CreateNewPath;
                Olduserdetail.fullname = userdetail.fullname;
                Olduserdetail.first_name = userdetail.first_name;
                Olduserdetail.last_name = userdetail.last_name;
                Olduserdetail.company_name = userdetail.company_name;
                Olduserdetail.city = userdetail.city;
                Olduserdetail.phone_number = userdetail.phone_number;
                Olduserdetail.email = userdetail.email;
                Olduserdetail.zip_code = userdetail.zip_code;
                Olduserdetail.nationality = userdetail.nationality;
                Olduserdetail.sex = userdetail.sex;
                Olduserdetail.religion = userdetail.religion;
                Olduserdetail.blood_group = userdetail.blood_group;
                Olduserdetail.dob = userdetail.dob;
            }
            else
            {
                TryUpdateModel(Olduserdetail, new string[] { "pic_path", "fullname", "first_name", "last_name", "company_name", "city", "phone_number", "email", "zip_code", "nationality", "sex", "religion", "blood_group", "dob" });
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
                    return RedirectToAction("UserProfile");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    ModelState.AddModelError(null, ex.ToString());
                    ViewBag.user_activeList = MyCustomFunctions.UserActivity();
                    return View(userdetail);
                }
                catch(System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    ModelState.AddModelError(null, ex.ToString());
                    ViewBag.user_activeList = MyCustomFunctions.UserActivity();
                    return View(userdetail);
                }
            }
            ViewBag.user_activeList = MyCustomFunctions.UserActivity();
            return View(userdetail);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Vendor vendor = db.Vendors.Find(id);

            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Modifyedpic_path,heading,pic_path,body")] Vendor newvendor, HttpPostedFileBase Modifyedpic_path)
        {
            string pic = null;
            var SupportFileType = new[] { "image/gif", "image/x-png", "image/jpeg", "image/png" };

            Vendor oldvendor = db.Vendors.SingleOrDefault(m => m.heading == newvendor.heading);
            string fullPath = Request.MapPath(oldvendor.pic_path);

            if(Modifyedpic_path != null)
            {
                if (SupportFileType.Contains(Modifyedpic_path.ContentType.ToLower().ToString()) == false)
                {
                    ModelState.AddModelError("pic_path", "Invalid File Type!");
                    return View(newvendor);
                }
                else if (Modifyedpic_path.ContentLength > (5 * 1024 * 1024))
                {
                    ModelState.AddModelError("pic_path", "Max File Size Is 5 MB!");
                    return View(newvendor);
                }

                pic = Guid.NewGuid().ToString()+Path.GetFileName(Modifyedpic_path.FileName);
                string CreateNewPath = "~/Content/img/tsms/vendor/" + pic;

                oldvendor.pic_path = CreateNewPath;
                oldvendor.body = newvendor.body;
            }
            else
            {
                TryUpdateModel(oldvendor, new string[] { "pic_path", "body" });
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.SaveChanges();
                    if (Modifyedpic_path != null)
                    {
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                        string path = Path.Combine(Server.MapPath("~/Content/img/tsms/vendor/"), pic);
                        Modifyedpic_path.SaveAs(path);
                    }
                    return RedirectToAction("Manage");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    ModelState.AddModelError(null, ex.ToString());
                    return View(newvendor);
                }
                catch(System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    ModelState.AddModelError(null, ex.ToString());
                    return View(newvendor);
                }
            }
            return View(newvendor);
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            Vendor vendor = db.Vendors.SingleOrDefault(model=> model.heading == id);
            return View(vendor);
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