using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TSMS_Atp_2_Final_Project.Models.Com.Tsms
{
    public class Studentassignbatche
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        /// <summary>
        /// ////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Id Is Required!")]
        [MaxLength(12, ErrorMessage = "The Max length Of User ID is 12 Cahracter!")]
        [RegularExpression("[1-3]{2}-[0-9]{5}-[123]{1}|[1-3]{2}-[0-9]{7}-[123]{1}", ErrorMessage = "Invalid Id,It should [xx]-[xxxxx]-[x] or [xx]-[xxxxxxx]-[x]!")]
        [Display(Name = "User Id")]
        public string UserId { get; set; }
        /// <summary>
        /// /////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Course Name Is Required!")]
        [MaxLength(700, ErrorMessage = "The Max Length For Course Name Is 700 Character!")]
        [Display(Name = "Course Name")]
        public string name { get; set; }
        /// <summary>
        /// /////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Batch Code Is Required!")]
        [MaxLength(700, ErrorMessage = "The Max Length For Batch Code Is 700 Character!")]
        [Display(Name = "Batch Code")]
        public string batch_code { get; set; }
        /// <summary>
        /// ///////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Status Is Required!")]
        [RegularExpression("^(?:complete|Complete|in progress|In progress)$", ErrorMessage = "Invalid Status!")]
        [Display(Name = "Current Status")]
        public string status { get; set; }
        /// <summary>
        /// ////////
        /// </summary>
        [Display(Name = "Course Completion Date")]
        [DataType(DataType.DateTime,ErrorMessage="Invalid Date!")]
        public Nullable<DateTime> completion_date { get; set; }

        //relationship with anothere  table------

        [ForeignKey("UserId")]
        public User User { get; set; }
        /// <summary>
        /// //////
        /// </summary>
        [ForeignKey("name")]
        public Course Course { get; set; }
        /// <summary>
        /// ///////
        /// </summary>
        [ForeignKey("batch_code")]
        public Batche Batche { get; set; }
    }
}