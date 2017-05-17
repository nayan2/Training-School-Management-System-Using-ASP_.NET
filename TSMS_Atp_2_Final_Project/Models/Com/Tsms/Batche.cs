using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace TSMS_Atp_2_Final_Project.Models.Com.Tsms
{
    public class Batche
    {
        [MaxLength(700, ErrorMessage = "The Max Length For Course Name Is 700 Character!")]
        [Required(ErrorMessage = "Course Name Is Required!")]
        [Display(Name = "Course Name")]
        public string name { get; set; }
        /// <summary>
        /// //////////
        /// </summary>
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Batch Code Is Required!")]
        [MaxLength(700, ErrorMessage = "The Max Length For Batch Code Is 700 Character!")]
        [Display(Name = "Batch Code")]
        public string batch_code { get; set; }
        /// <summary>
        /// ////////
        /// </summary>
        [Required(ErrorMessage = "Batch Starting Date Is Required!")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date!")]
        [Display(Name = "Batch Stating Date")]
        public DateTime batch_starting_date { get; set; }
        /// <summary>
        /// //////////
        /// </summary>
        [Required(ErrorMessage = "Admission Last Date Is Required!")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date!")]
        [Display(Name = "Admission Last Date")]
        public DateTime admission_last_date { get; set; }
        /// <summary>
        /// /////////
        /// </summary>
        [Required(ErrorMessage = "Room Number Is Required!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid Room Number!")]
        [Display(Name = "Room Number")]
        public int room_number { get; set; }
        /// <summary>
        /// ////////
        /// </summary>
        [Required(ErrorMessage = "Faculty Name Is Required!")]
        [DataType(DataType.Text, ErrorMessage = "Invalid Faculty Name!")]
        [Display(Name = "Faculty Name")]
        [MaxLength(500, ErrorMessage = "The Max Length For Faculty Name Is 500 Character!")]
        public string faculty_name { get; set; }
        /// <summary>
        /// //////////
        /// </summary>
        [Required(ErrorMessage = "BAtch Amount is Required!")]
        [DataType(DataType.Currency, ErrorMessage = "Invalid Ammount!")]
        [Display(Name = "Batch Amount")]
        public int amount { get; set; }
        /// <summary>
        /// /////////
        /// </summary>
        [Required(ErrorMessage = "Batch Details is Required!")]
        [MaxLength(1000, ErrorMessage = "The Max Length For Details Is 1000 character!")]
        [Display(Name = "Batch Details")]
        public string details { get; set; }
        /// <summary>
        /// /////////
        /// </summary>
        [Required(ErrorMessage = "Batch Routine is Required!")]
        [MaxLength(700, ErrorMessage = "The Max Length For Routine Is 700 character!")]
        [RegularExpression(@"((?:Saturday|Sunday|Monday|Tuesday|Wednesday|Thursday|Friday)-[0-9]{2}:[0-9]{2}[\s](?:AM|PM)[\s](?:to)[\s][0-9]{2}:[0-9]{2}[\s](?:AM|PM)[\s](?:and)[\s](?:Saturday|Sunday|Monday|Tuesday|Wednesday|Thursday|Friday)-[0-9]{2}:[0-9]{2}[\s](?:AM|PM)[\s](?:to)[\s][0-9]{2}:[0-9]{2}[\s](?:AM|PM))|((?:Saturday|Sunday|Monday|Tuesday|Wednesday|Thursday|Friday)-[0-9]{2}:[0-9]{2}[\s](?:AM|PM)[\s](?:to)[\s][0-9]{2}:[0-9]{2}[\s](?:AM|PM))", ErrorMessage = "Invalid Routine Pattern! It Should Be Something Like [AnyDay-xx:xx AM|PM to xx:xx PM|AM and AnyDay-xx:xx PM|AM to xx:xx PM|AM] or [AnyDay-xx:xx AM|PM to xx:xx PM|AM]")]
        [Display(Name = "Batch Routine")]
        public string routine { get; set; }

        //relationship with other table-----------
        [ForeignKey("name")]
        public Course Course { get; set; }
        public List<Instructor> Instructors { get; set; }
    }
}