using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TSMS_Atp_2_Final_Project.Models.Com.Tsms
{
    public class UserDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        /// <summary>
        /// /////////////////////
        /// </summary>
        [MaxLength(12, ErrorMessage = "You Have Exceed The Max length Of User ID which is 12 Cahracter!")]
        [RegularExpression("[1-3]{2}-[0-9]{5}-[123]{1}|[1-3]{2}-[0-9]{7}-[123]{1}", ErrorMessage = "Invalid Id,It should [xx]-[xxxxx]-[x] or [xx]-[xxxxxxx]-[x]")]
        [Display(Name = "User ID")]
        public string UserId { get; set; }
        /// <summary>
        /// ///////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Full Name Is Required!")]
        [MaxLength(100, ErrorMessage = "The Max Length Of Full Name Is 100 Character!")]
        [DataType(DataType.Text, ErrorMessage = "Invalid Full Name!")]
        [Display(Name = "Full Name")]
        public string fullname { get; set; }
        /// <summary>
        /// ////////////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name IS Required!")]
        [MaxLength(50, ErrorMessage = "The Max Length Of First Name Is 50 Character!")]
        [DataType(DataType.Text, ErrorMessage = "Invalid First Name!")]
        [Display(Name = "First Name")]
        public string first_name { get; set; }
        /// <summary>
        /// /////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name Is Required!")]
        [MaxLength(50, ErrorMessage = "The Max Length Of Last Name is 50 Character!")]
        [DataType(DataType.Text, ErrorMessage = "Invalid Last Name!")]
        [Display(Name = "Last Name")]
        public string last_name { get; set; }
        /// <summary>
        /// ////////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pic Is Required!")]
        [MaxLength(700, ErrorMessage = "Your Destined Pic Path Is Too Much Long.The Max Length Of Picture Path Is 700 Character!")]
        [Display(Name = "Profile Pic")]
        public string pic_path { get; set; }
        /// <summary>
        /// ///////
        /// </summary>
        [Display(Name = "Company Name")]
        public string company_name { get; set; }
        /// <summary>
        /// //////////
        /// </summary>
        [Display(Name = "City")]
        public string city { get; set; }
        /// <summary>
        /// ////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone Number Is Required!")]
        [RegularExpression("[0-9]{7,11}", ErrorMessage = "Invalid Phone Number!")]
        [Display(Name = "Phone Number")]
        public int phone_number { get; set; }
        /// <summary>
        /// /////////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email Is Required!")]
        [EmailAddress(ErrorMessage = "Invalid Email Address!")]
        [Display(Name = "Email Address")]
        public string email { get; set; }
        /// <summary>
        /// //////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Zip Code Is Required!")]
        [RegularExpression("[0-9]{4}", ErrorMessage = "Invalid Zip Code!")]
        [Display(Name = "Zip Code")]
        public int zip_code { get; set; }
        /// <summary>
        /// /////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nationality Is Required!")]
        [MaxLength(30, ErrorMessage = "The Max Length Of Nationality Is 30 Character!")]
        [DataType(DataType.Text, ErrorMessage = "Invalid Nationality!")]
        [Display(Name = "User Nationality")]
        public string nationality { get; set; }
        /// <summary>
        /// ////////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Gender Is Required!")]
        [RegularExpression("^(?:m|M|male|Male|f|F|female|Female)$", ErrorMessage = "Invalid Gender!")]
        [Display(Name = "Gender")]
        public string sex { get; set; }
        /// <summary>
        /// ///////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Religion Is Required!")]
        [MaxLength(20, ErrorMessage = "The Max Length Of Religion is 20 Character!")]
        [DataType(DataType.Text, ErrorMessage = "Invalid Religion!")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Invalid Religion!")]
        [Display(Name = "Religion")]
        public string religion { get; set; }
        /// <summary>
        /// ////////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Blood Group Is Required!")]
        [MaxLength(50, ErrorMessage = "The Max Length Of Blood Group Is 50 character!")]
        [Display(Name = "Blood Group")]
        public string blood_group { get; set; }
        /// <summary>
        /// ///////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date Of Birth Is Required!")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date Of Birth")]
        [Display(Name = "Date Of Birth")]
        public DateTime dob { get; set; }
        /// <summary>
        /// ///////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Activation Is Required!")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date!")]
        [Display(Name = "User Activation Date")]
        public DateTime user_activation_date { get; set; }
        /// <summary>
        /// //////////
        /// </summary>
        [DataType(DataType.Date, ErrorMessage = "Invalid Date!")]
        [Display(Name = "User Deactivation Date")]
        public Nullable<DateTime> user_deactivation_date { get; set; }
        /// <summary>
        /// ///////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Current Status Is Required!")]
        [RegularExpression("^(?:active|Active|inactive|Inactive)$", ErrorMessage = "Invalid Status!")]
        [Display(Name = "User Current Status")]
        public string user_active { get; set; }

        //relationshipwith other tables-----

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}