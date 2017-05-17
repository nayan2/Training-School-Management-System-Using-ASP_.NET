using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSMS_Atp_2_Final_Project.Models.Com.Tsms;

namespace TSMS_Atp_2_Final_Project.Controllers
{
    public class TeachersExamController : Controller
    {
        // GET: TeachersExam

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Userid"] != null || Session["Password"] != null)
                base.OnActionExecuting(filterContext);
            else
                filterContext.Result = RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        {
            User CurrentUser = (User)Session["CurrentUser"];

            if (CurrentUser.level == "admin")
            {
                using (TsmsDBContext context = new TsmsDBContext())
                {
                    string user = CurrentUser.UserId.ToString();
                    List<Exam> exam = context.Exams.Where(p => p.QuestionMaker == user).ToList();
                    return View(exam);
                }
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }

        [HttpGet]
        public ActionResult CreateExam()
        {
            User CurrentUser = (User)Session["CurrentUser"];
            //if (Session["Userid"] != null && Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {
                    return View();
            //    }
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
        [HttpPost]
        public ActionResult CreateExam(Exam exam)
        {
            //if (Session["Userid"] != null && Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {
                    //if (ModelState.IsValid)
                    //{
                       using (TsmsDBContext context = new TsmsDBContext())
                        {
                            exam.QuestionMaker = (string)Session["Userid"];
                            exam.TotalQues = 0;
                            context.Exams.Add(exam);
                            context.SaveChanges();
                            return RedirectToAction("ExamDetail", new { id = exam.Id });
                        }
        //            }
        //            else
        //            {
        //                return View();
        //            }
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "Exam");
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Exam");
         }
        

        [NonAction]
        public ActionResult GetCourses(string term)
        {
            //if (Session["Userid"] != null && Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {
                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        List<string> courses = context.Courses.Where(x => x.name.Contains(term)).
                            Select(y => y.name).ToList();
                        return Json(courses, JsonRequestBehavior.AllowGet);
                    }

            //    }
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
        [NonAction]
        public ActionResult GetStudents(string term)
        {
            //if (Session["Userid"] != null && Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {
                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        List<string> students = context.Users.Where(x => x.UserId.StartsWith(term)).
                            Select(y => y.UserId).ToList();
                        return Json(students, JsonRequestBehavior.AllowGet);
                    }

            //    }
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
        public ActionResult EditExam(int id)
        {
            //if (Session["Userid"] != null && Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {
                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        Exam exam = context.Exams.SingleOrDefault(p => p.Id == id);
                        return View(exam);
                    }
            //    }
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
        [HttpPost]
        public ActionResult EditExam(Exam exam)
        {
            //if (Session["Userid"] != null && Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {
                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        context.Exams.Attach(exam);
                        var entry = context.Entry(exam);
                        entry.Property(e => e.Course).IsModified = true;
                        entry.Property(e => e.Date).IsModified = true;
                        entry.Property(e => e.ExamState).IsModified = true;
                        entry.Property(e => e.FinishTime).IsModified = true;
                        entry.Property(e => e.MarkPerQues).IsModified = true;
                        entry.Property(e => e.Name).IsModified = true;
                        entry.Property(e => e.StartTime).IsModified = true;
                        entry.Property(e => e.Type).IsModified = true;
                        context.SaveChanges();
                        return RedirectToAction("ExamDetail", new { id = exam.Id });
                    }
            //    }
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
            //if ((string)Session["Userid"] != null && (string)Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {
                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        Exam exam = context.Exams.SingleOrDefault(EId => EId.Id == id);
                        ViewBag.QuestionMakerName = context.UserDetails.Where(p => p.UserId == exam.QuestionMaker).Select(y => y.fullname);
                        return View(exam);
                    }
            //    }
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
        public ActionResult DeleteExam(int id)
        {
            //if ((string)Session["Userid"] != null && (string)Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {
                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        Exam exam = context.Exams.SingleOrDefault(EId => EId.Id == id);
                        ViewBag.QuestionMakerName = context.UserDetails.Where(p => p.UserId == exam.QuestionMaker).Select(y => y.fullname);
                        return View(exam);
                    }
               // }
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

        [HttpPost]
        [ActionName("DeleteExam")]
        public ActionResult DeleteExamPost(int id)
        {
            //if ((string)Session["Userid"] != null && (string)Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {
                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        Exam exam = context.Exams.SingleOrDefault(EId => EId.Id == id);
                        context.Exams.Attach(exam);
                        context.Exams.Remove(exam);
                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }
            //    }
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
        //Create Question
        [HttpGet]
        public ActionResult CreateQuestion(int id)
        {
            //if ((string)Session["Userid"] != null && (string)Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {
                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        Exam exam = context.Exams.SingleOrDefault(EId => EId.Id == id);
                        //if (exam.QuestionMaker == (string)Session["Userid"])
                        //{

                            ViewBag.ExamId = id;
                            ViewBag.Total = Convert.ToInt32(exam.TotalQues);
                            ViewBag.Mark = Convert.ToString(exam.MarkPerQues);
                            List<Question> questions = context.Questions.Where(p => p.ExamID == id).ToList();
                            return View(questions);
                        //}
                        //else
                        //{
                        //    return RedirectToAction("Index", "Exam");
                        //}
                    }

            //    }
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
        public ActionResult AddQuestion(int id)
        {
            //if ((string)Session["Userid"] != null && (string)Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {
                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        Exam exam = context.Exams.SingleOrDefault(EId => EId.Id == id);
                       // if (exam.QuestionMaker == (string)Session["Userid"])
                       // {
                            ViewBag.ExamId = id;
                            ViewBag.Total = exam.TotalQues + 1;
                            ViewBag.Mark = Convert.ToString(exam.MarkPerQues);
                            return View();
                        //}
                        //else
                        //{
                        //    return RedirectToAction("Index", "Exam");
                        //}
                    }

               // }
               // else
            //    {
            //        return RedirectToAction("Index", "Exam");
            //    }
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Exam");
            //}

        }

        [HttpPost]
        public ActionResult AddQuestion(Question question)
        {
            //if ((string)Session["Userid"] != null && (string)Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {
                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        Exam exam = context.Exams.SingleOrDefault(EId => EId.Id == question.ExamID);
                        exam.TotalQues = (int)exam.TotalQues+1;
                        question.QuestionNumber = exam.TotalQues;
                        context.Exams.Attach(exam);
                        var entry = context.Entry(exam);
                        entry.Property(e => e.TotalQues).IsModified = true;
                        context.SaveChanges();
                       
                        context.Questions.Add(question);
                        context.SaveChanges();

                       
                        return RedirectToAction("CreateQuestion", new { id = question.ExamID });
                    }
            //    }
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
        public ActionResult EditQuestion(int id)
        {
            //if ((string)Session["Userid"] != null && (string)Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {
                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        Question ques = context.Questions.SingleOrDefault(p => p.Id == id);
                        return View(ques);
                    }
            //    }
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

        [HttpPost]
        public ActionResult EditQuestion(Question ques)
        {
            //if ((string)Session["Userid"] != null && (string)Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {
                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        context.Questions.Attach(ques);
                        var entry = context.Entry(ques);
                        entry.Property(e => e.QuestionTitle).IsModified = true;
                        entry.Property(e => e.Option1).IsModified = true;
                        entry.Property(e => e.Option2).IsModified = true;
                        entry.Property(e => e.Option3).IsModified = true;
                        entry.Property(e => e.Option4).IsModified = true;
                        entry.Property(e => e.Option5).IsModified = true;
                        entry.Property(e => e.CorrectAns).IsModified = true;
                        context.SaveChanges();
                        return RedirectToAction("CreateQuestion", new { id = ques.ExamID });
                    }
            //    }
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
        public ActionResult DeleteQuestion(int id)
        {
            //if ((string)Session["Userid"] != null && (string)Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {
                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        Exam exam = context.Exams.SingleOrDefault(EId => EId.Id == id);
                        Question ques = context.Questions.SingleOrDefault(p => p.ExamID == id && p.QuestionNumber == exam.TotalQues);
                        return View(ques);
                    }
            //    }
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
        [HttpPost]
        [ActionName("DeleteQuestion")]
        public ActionResult DeleteQuestionPost(int id)
        {
            //if ((string)Session["Userid"] != null && (string)Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {
                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        Question ques = context.Questions.SingleOrDefault(p => p.Id == id);
                        context.Questions.Remove(ques);
                        context.SaveChanges();
                        Exam exam = context.Exams.SingleOrDefault(p => p.Id == ques.ExamID);
                        exam.TotalQues = exam.TotalQues - 1;
                        context.Exams.Attach(exam);
                        var entry = context.Entry(exam);
                        entry.Property(e => e.TotalQues).IsModified = true;
                        context.SaveChanges();
                        return RedirectToAction("CreateQuestion", new { id = ques.ExamID });
                    }
            //    }
            //    else
            //    {
            //        return RedirectToAction("Index", "Exam");
            //    }
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Exam");
           // }
        }
        //Show Question
        [HttpGet]
        public ActionResult ShowQuestion(int id)
        {
            User CurrentUser = (User)Session["CurrentUser"];
            //if (Session["Userid"] != null)
            //{
                using (TsmsDBContext context = new TsmsDBContext())
                {
                    Exam exam = context.Exams.SingleOrDefault(p => p.Id == id);
                    if (CurrentUser.level == "admin")
                    {

                        Answer ans = context.Answers.SingleOrDefault(p => p.ExamId == id && p.StudentId == CurrentUser.UserId);
                        if (ans == null)
                        {
                            ViewBag.ExamId = id;

                            ViewBag.Total = exam.TotalQues;
                            var src = DateTime.Now;
                            string hour = src.Hour.ToString();
                            string min = src.Minute.ToString();
                            string end = exam.FinishTime;
                            char[] d = { ' ', ':' };
                            string[] words = end.Split(d, System.StringSplitOptions.RemoveEmptyEntries);
                            string endHour = words[0];
                            string endMin = words[1];
                            ViewBag.duration = ((Convert.ToInt32(endHour) * 60 + Convert.ToInt32(endMin)) - (Convert.ToInt32(hour) * 60 + Convert.ToInt32(min))) * 60 * 1000;
                            ViewBag.CountDown = exam.Date + " " + end + ":00";
                            List<Question> questions = context.Questions.Where(p => p.ExamID == id).ToList();
                            return View(questions);
                        }
                        else
                        {
                            List<Answer> answers = context.Answers.Where(p => p.ExamId == id && p.StudentId == Convert.ToString(Session["Userid"])).ToList();
                            foreach (Answer ansob in answers)
                            {
                                context.Answers.Attach(ansob);
                                context.Answers.Remove(ansob);
                                context.SaveChanges();
                            }
                            ViewBag.ExamId = id;

                            ViewBag.Total = exam.TotalQues;
                            var src = DateTime.Now;
                            string hour = src.Hour.ToString();
                            string min = src.Minute.ToString();
                            string end = exam.FinishTime;
                            char[] d = { ' ', ':' };
                            string[] words = end.Split(d, System.StringSplitOptions.RemoveEmptyEntries);
                            string endHour = words[0];
                            string endMin = words[1];
                            ViewBag.duration = ((Convert.ToInt32(endHour) * 60 + Convert.ToInt32(endMin)) - (Convert.ToInt32(hour) * 60 + Convert.ToInt32(min))) * 60 * 1000;
                            ViewBag.CountDown = exam.Date + " " + end + ":00";
                            List<Question> questions = context.Questions.Where(p => p.ExamID == id).ToList();
                            return View(questions);
                        }
                    }
                    else
                    {
                        if (exam.ExamState == "open")
                        {
                            ExamAssign assign = context.ExamAssigns.SingleOrDefault(p => p.StudentID == (string)Session["Userid"] && p.Id == id);
                            if (assign != null)
                            {
                                Answer ans = context.Answers.SingleOrDefault(p => p.ExamId == id && p.StudentId == Convert.ToString(Session["Userid"]));
                                if (ans == null)
                                {
                                    ViewBag.ExamId = id;

                                    ViewBag.Total = exam.TotalQues;
                                    var src = DateTime.Now;
                                    string hour = src.Hour.ToString();
                                    string min = src.Minute.ToString();
                                    string end = exam.FinishTime;
                                    char[] d = { ' ', ':' };
                                    string[] words = end.Split(d, System.StringSplitOptions.RemoveEmptyEntries);
                                    string endHour = words[0];
                                    string endMin = words[1];
                                    ViewBag.duration = ((Convert.ToInt32(endHour) * 60 + Convert.ToInt32(endMin)) - (Convert.ToInt32(hour) * 60 + Convert.ToInt32(min))) * 60 * 1000;
                                    ViewBag.CountDown = exam.Date + " " + end + ":00";
                                    List<Question> questions = context.Questions.Where(p => p.ExamID == id).ToList();
                                    return View(questions);
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
                        else
                        {
                            return RedirectToAction("Index", "Exam");
                        }
                    }
                }
           // }
            //else
            //{
            //    return RedirectToAction("Index", "Exam");
            //}
        }
        [HttpPost]
        public ActionResult ShowQuestion(FormCollection form)
        {
            //if ((string)Session["Userid"] != null && (string)Session["Level"] != null)
            //{
               using (TsmsDBContext context = new TsmsDBContext())
                {

                    int examId = Convert.ToInt32(Request.Form["hidden"]);
                    Exam exam = context.Exams.SingleOrDefault(p => p.Id == examId);
                    for (int i = 1; i <= exam.TotalQues; i++)
                    {

                        Answer ans = new Answer();
                        ans.StudentId = (string)Session["Userid"];
                        ans.StuAns = Convert.ToInt32(Request.Form[i.ToString()]);
                        ans.ExamId = examId;
                        ans.QuestionId = Convert.ToInt32(Request.Form["QuesId" + i.ToString()]);
                        Question ques = context.Questions.SingleOrDefault(p => p.Id == ans.QuestionId);
                        if (ans.StuAns == ques.CorrectAns)
                        {
                            ans.Result = true;
                        }
                        else
                        {
                            ans.Result = false;
                        }

                        context.Answers.Add(ans);
                        context.SaveChanges();
                    }
                    List<Answer> answers = context.Answers.Where(p => p.ExamId == examId && p.StudentId == (string)Session["Userid"]).ToList();
                    float achived = 0;
                    foreach (Answer answer in answers)
                    {
                        if (answer.Result == true)
                        {
                            achived++;
                        }
                    }
                    Exam examOb = context.Exams.SingleOrDefault(p => p.Id == examId);
                    float Marks = achived * examOb.MarkPerQues;
                    ExamAssign assign = context.ExamAssigns.SingleOrDefault(p => p.ExamID == examId && p.StudentID == Convert.ToString(Session["Userid"]));
                    assign.gotMark = Marks.ToString();
                    context.ExamAssigns.Attach(assign);
                    var entry = context.Entry(assign);
                    entry.Property(e => e.gotMark).IsModified = true;
                    context.SaveChanges();
                    float totalmarks = examOb.TotalQues * examOb.MarkPerQues;
                    return RedirectToAction("ShowResult", new { id = examId, total = totalmarks, achieved = Marks });
                }
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Exam");
            //}
        }

        [HttpGet]
        public ActionResult ShowResult(int id, float total, float achieved)
        {
            //if ((string)Session["Userid"] != null && (string)Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {
                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        ViewBag.ExamId = id;
                        ViewBag.TotalMarks = total;
                        ViewBag.achieved = achieved;
                        return View();
                    }
               // }
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



        //Assign Student
        [HttpGet]
        public ActionResult AssaignStudent(int id)
        {
            //if ((string)Session["Userid"] != null && (string)Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {

                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        ViewBag.ExamId = id;
                        List<ExamAssign> assaign = context.ExamAssigns.Where(p => p.ExamID == id).ToList();
                        return View(assaign);
                    }
            //    }
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
        [HttpPost]
        public ActionResult AssaignStudent(FormCollection form)
        {
            //if ((string)Session["Userid"] != null && (string)Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {

                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        List<ExamAssign> all = context.ExamAssigns.Where(p => p.StudentID == Request.Form["stuName"] && p.ExamID == Convert.ToInt32(Request.Form["ExamId"])).ToList();
                        if (all == null)
                        {
                            ViewBag.ExamId = Convert.ToInt32(Request.Form["ExamId"]);
                            ExamAssign assign = new ExamAssign();
                            assign.ExamID = Convert.ToInt32(Request.Form["ExamId"]);
                            assign.StudentID = Request.Form["stuName"];
                            context.ExamAssigns.Add(assign);
                            context.SaveChanges();
                            List<ExamAssign> assaign = context.ExamAssigns.Where(p => p.ExamID == Convert.ToInt32(Request.Form["ExamId"])).ToList();
                            return View(assaign);
                        }
                        else
                        {
                            ViewBag.ExamId = Convert.ToInt32(Request.Form["ExamId"]);
                            List<ExamAssign> assaign = context.ExamAssigns.Where(p => p.ExamID == Convert.ToInt32(Request.Form["ExamId"])).ToList();
                            return View(assaign);
                        }
                    }
            //    }
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
        public ActionResult AssaignCourse(int id)
        {
            //if ((string)Session["Userid"] != null && (string)Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {

                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        ViewBag.ExamId = id;
                        List<ExamAssign> assaign = context.ExamAssigns.Where(p => p.ExamID == id).ToList();
                        return View(assaign);
                    }
            //    }
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
        [HttpPost]
        public ActionResult AssaignCourse(FormCollection form)
        {
            //if ((string)Session["Userid"] != null && (string)Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {

                    using (TsmsDBContext context = new TsmsDBContext())
                    {

                        List<Studentassignbatche> stu = context.Studentassignbatches.Where(p => p.name == Request.Form["crsName"]).ToList();
                        foreach (Studentassignbatche st in stu)
                        {
                            List<ExamAssign> all = context.ExamAssigns.Where(p => p.StudentID == st.UserId && p.ExamID == Convert.ToInt32(Request.Form["ExamId"])).ToList();
                            if (all == null)
                            {
                                ViewBag.ExamId = Convert.ToInt32(Request.Form["ExamId"]);
                                ExamAssign assign = new ExamAssign();
                                assign.ExamID = Convert.ToInt32(Request.Form["ExamId"]);
                                assign.StudentID = st.UserId;
                                context.ExamAssigns.Add(assign);
                                context.SaveChanges();
                            }
                        }
                        ViewBag.ExamId = Convert.ToInt32(Request.Form["ExamId"]);
                        List<ExamAssign> assaign = context.ExamAssigns.Where(p => p.ExamID == Convert.ToInt32(Request.Form["ExamId"])).ToList();
                        return View(assaign);
                    }
            //    }
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
        public ActionResult DeleteAssignStudent(int id)
        {
            //if ((string)Session["Userid"] != null && (string)Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {
                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        ExamAssign assign = context.ExamAssigns.SingleOrDefault(p => p.Id == id);
                        return View(assign);
                    }
            //    }

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
        [HttpPost]
        [ActionName("DeleteAssignStudent")]
        public ActionResult DeleteAssignStudentPost(int id)
        {
            //if ((string)Session["Userid"] != null && (string)Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {
                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        ExamAssign assign = context.ExamAssigns.SingleOrDefault(p => p.Id == id);
                        int examid = assign.ExamID;
                        context.ExamAssigns.Remove(assign);
                        context.SaveChanges();
                        return RedirectToAction("AssaignCourse", new { id = examid });
                    }
            //    }

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
        public ActionResult ExamResult(int id)
        {
            //if ((string)Session["Userid"] != null && (string)Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {
                    using (TsmsDBContext context = new TsmsDBContext())
                    {
                        List<ExamAssign> exam = context.ExamAssigns.Where(EId => EId.ExamID == id).ToList();
                        return View(exam);
                    }
            //    }
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
        public ActionResult StudentResult()
        {
            //if ((string)Session["Userid"] != null && (string)Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {
                    return View();

            //    }
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
        [HttpPost]
        public ActionResult StudentResult(string a)
        {
            //if ((string)Session["Userid"] != null && (string)Session["Level"] != null)
            //{
            //    if ((string)Session["Level"] == "admin")
            //    {
                    if (a != null)
                    {
                        using (TsmsDBContext context = new TsmsDBContext())
                        {
                            ViewBag.Name = context.UserDetails.SingleOrDefault(p => p.UserId == a).fullname;
                            List<ExamAssign> exam = context.ExamAssigns.Where(EId => EId.StudentID == a).ToList();
                            return View(exam);
                        }
                    }
                    else
                    {
                        return View();
                    }
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "Exam");
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Exam");
        //    }
        }
    }
}