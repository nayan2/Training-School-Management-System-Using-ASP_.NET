using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TSMS_Atp_2_Final_Project.Models.Com.Tsms
{
    public class Instructor
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InstructorId { get; set; }
        /// <summary>
        /// ///////////
        /// </summary>
        [MaxLength(700, ErrorMessage = "The Max Length For Course Name Is 700 Character!")]
        [Display(Name = "Course Name")]
        public string name { get; set; }
        /// <summary>
        /// //////////
        /// </summary>
        [MaxLength(700, ErrorMessage = "The Max Length For Batch Code Is 700 Character!")]
        [Display(Name = "Batch Code")]
        public string batch_code { get; set; }
        /// <summary>
        /// /////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Picture Is Required!")]
        [MaxLength(700, ErrorMessage = "The Pic Path Max Length Is 700 Character!")]
        [Display(Name = "Choose Pic")]
        public string pic_path { get; set; }
        /// <summary>
        /// ////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name Is Required!")]
        [MaxLength(50, ErrorMessage = "The Max Length Of First Name Is 50 Character!")]
        [Display(Name = "First Name")]
        public string first_name { get; set; }
        /// <summary>
        /// ///////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name Is Required!")]
        [MaxLength(50, ErrorMessage = "The Max Length Of Last Name is 50 Cahracter!")]
        [Display(Name = "Last Name")]
        public string last_name { get; set; }
        /// <summary>
        /// ///////////
        /// </summary>
        [Key, Column(Order = 1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Faculty Name Is Required!")]
        [MaxLength(100, ErrorMessage = "The Max Length Of Faculty Name is 200 Character!")]
        [Display(Name = "Faculty Name")]
        public string faculty_name { get; set; }
        /// <summary>
        /// ////////
        /// </summary>
        [Display(Name = "Company Name")]
        public string company_name { get; set; }
        /// <summary>
        /// /////////
        /// </summary>
        [Display(Name = "City")]
        public string city { get; set; }
        /// <summary>
        /// //////////
        /// </summary>
        [Required(ErrorMessage = "Phone Number Is Required!")]
        [DataType(DataType.PhoneNumber,ErrorMessage="Invalid Phone Number!")]
        [Display(Name = "Phone Number")]
        public int phone_number { get; set; }
        /// <summary>
        /// /////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email Is Required!")]
        [EmailAddress(ErrorMessage = "Invalid Email Address!")]
        [Display(Name = "Email Address")]
        public string email { get; set; }
        /// <summary>
        /// ////////
        /// </summary>
        [Required(ErrorMessage = "Zip Code Is Required!")]
        [RegularExpression("^[0-9]{4,5}$", ErrorMessage = "Invalid Zip Code!")]
        [Display(Name = "Zip Code")]
        public int zip_code { get; set; }
        /// <summary>
        /// ///////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nationality Is Required!")]
        [MaxLength(30, ErrorMessage = "The Max Length Of Nationality Is 30 Character!")]
        [Display(Name = "Nationality")]
        public string nationality { get; set; }
        /// <summary>
        /// ////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Gender Is Required!")]
        [RegularExpression("^(?:male|Male|female|Female)$", ErrorMessage = "Invalid Gender!")]
        [Display(Name = "Gender")]
        public string sex { get; set; }
        /// <summary>
        /// //////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Religion Is Required!")]
        [MaxLength(30, ErrorMessage = "The Max Length Of Religion Is 30 Character!")]
        [Display(Name = "Religion")]
        public string religion { get; set; }
        /// <summary>
        /// //////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Blood Group Is Required!")]
        [MaxLength(50, ErrorMessage = "The Max Length Of Blood Group Is 50 character!")]
        [Display(Name = "Blood Group")]
        public string blood_group { get; set; }
        /// <summary>
        /// //////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date Of Birth Is Required!")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date Of Birth!")]
        [Display(Name = "Date Of Birth")]
        public DateTime dob { get; set; }
        /// <summary>
        /// /////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Faculty Activation Date Is Required!")]
        [DataType(DataType.Date ,ErrorMessage="Invalid Date!")]
        [Display(Name = "Faculty Activation date")]
        public DateTime faculty_activation_date { get; set; }
        /// <summary>
        /// //////////
        /// </summary>
        [DataType(DataType.Date, ErrorMessage = "Invalid Date!")]
        [Display(Name = "Faculty Deactivation Date")]
        public Nullable<DateTime> faculty_inactivation_date { get; set; }
        /// <summary>
        /// /////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Faculty Current Status Is Required!")]
        [RegularExpression("^(?:active|Active|inactive|Inactive)$", ErrorMessage = "Invalid Status!")]
        [Display(Name = "Faculty Current Status")]
        public string faculty_active { get; set; }

        ////relationshipwith other tables-----

        [ForeignKey("name")]
        public Course Course { get; set; }
        /// <summary>
        /// ///////
        /// </summary>
        [ForeignKey("batch_code")]
        public Batche Batche { get; set; }
    }
}