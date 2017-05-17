using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TSMS_Atp_2_Final_Project.Models.Com.Tsms
{
    public class Question
    {
        [Key]
        public int Id { set; get; }
        public string QuestionTitle { set; get; }
        public int QuestionNumber { set; get; }
        public string Option1 { set; get; }
        public string Option2 { set; get; }
        public string Option3 { set; get; }
        public string Option4 { set; get; }
        public string Option5 { set; get; }
        public int CorrectAns { set; get; }
        public int ExamID  { set; get; }
        public string mark { set; get; }
       // public Exam exam { set; get; }
    }
}