using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TSMS_Atp_2_Final_Project.Models.Com.Tsms
{
    public class Answer
    {
       [Key]
       public int Id { set; get; }
       public string StudentId { set; get; }
       public int QuestionId { set; get; }
       public int StuAns { set; get; }
       public int ExamId { set; get; }
       public Boolean Result { set; get; }
    }
}