using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSMS_Atp_2_Final_Project.Models.Com.Tsms
{
    public class Vendor
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pic Is Required!")]
        [MaxLength(1000, ErrorMessage = "The Max Length For Pic Path Is 1000!")]
        [Display(Name = "Pic")]
        public string pic_path { get; set; }
        /// <summary>
        /// ////////
        /// </summary>
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vendor Heading Is Required!")]
        [MaxLength(700, ErrorMessage = "The Max Length For Heading Is 700 Character!")]
        [Display(Name = "Vendor Name")]
        public string heading { get; set; }
        /// <summary>
        /// ///////////
        /// </summary>
        [Required(ErrorMessage = "Adding date Is Required!")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date!")]
        [Display(Name = "Vendor Adding Date")]
        public DateTime adding_date { get; set; }
        /// <summary>
        /// ///////////
        /// </summary>
        [MaxLength(5000, ErrorMessage = "The Max Length For Vendor Body/Details Is 5000 Character!")]
        [Display(Name = "Vendor Details")]
        public string body { get; set; }

        //relationship with anothere tables------

        public List<Course> Courses { get; set; }
    }
}