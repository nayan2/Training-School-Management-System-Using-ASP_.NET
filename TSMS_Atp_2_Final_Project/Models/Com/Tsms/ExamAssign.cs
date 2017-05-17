using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TSMS_Atp_2_Final_Project.Models.Com.Tsms
{
    public class ExamAssign
    {
        [Key]
        public int Id { set; get; }
        public string StudentID { set; get; }
        public int ExamID { set; get; }
        public string gotMark { set; get; }
       // public Exam exam { set; get; }
    }
}