using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TSMS_Atp_2_Final_Project.Models.Com.Tsms
{
    public class Course
    {
        [MaxLength(700, ErrorMessage = "The Max Length For Heading Is 700 Character!")]
        [Required(ErrorMessage="Vendor Name Is Required!")]
        [Display(Name = "Vendor Heading")]
        public string vendor_heading { get; set; }
        /// <summary>
        /// //////////
        /// </summary>
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Course Name Is Required!")]
        [MaxLength(700, ErrorMessage = "The Max Length For Course Name Is 700 Character!")]
        [Display(Name = "Course Name")]
        public string name { get; set; }
        /// <summary>
        /// //////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Course Code Is Required!")]
        [MaxLength(200, ErrorMessage = "The Max Length For Course Code Is 200 Character!")]
        [Display(Name = "Course Code")]
        public string code { get; set; }
        /// <summary>
        /// //////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Picture Is Required!")]
        [MaxLength(1000, ErrorMessage = "The Max Length For Pic Path Is 1000 Character!")]
        [Display(Name = "Pic")]
        public string pic_path { get; set; }
        /// <summary>
        /// /////////
        /// </summary>
        [Required(ErrorMessage = "Adding date Is Required!")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date!")]
        [Display(Name = "Adding Date")]
        public DateTime adding_date { get; set; }
        /// <summary>
        /// /////////
        /// </summary>
        [MaxLength(5000, ErrorMessage = "The Max Length For Course Details Is 5000 Character!")]
        [Display(Name = "Course Details")]
        public string details { get; set; }

        //relationship With  other tables-------
        [ForeignKey("vendor_heading")]
        public Vendor Vendor { get; set; }
        public List<Batche> Batches { get; set; }
    }
}