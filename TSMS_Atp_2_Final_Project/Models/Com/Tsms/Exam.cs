using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TSMS_Atp_2_Final_Project.Models.Com.Tsms
{
    public class Exam
    {
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
        public string Course { set; get; }
        public string Type { set; get; }
        public string Date { set; get; }
        public string StartTime { set; get; }
        public string FinishTime { set; get; }
        public string QuestionMaker { set; get; }
        public int TotalQues { set; get; }
        public float MarkPerQues { set; get; }
        public string ExamState { set; get; }
        //public List<ExamAssign> assaignedStus {set;get;}
        //public List<Question> questions { set; get; }
        //public List<Answer> answers { set; get; }
        //public List<User> users { set; get; }
    }
}