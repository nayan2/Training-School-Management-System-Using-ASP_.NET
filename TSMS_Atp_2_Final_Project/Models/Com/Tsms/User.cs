using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TSMS_Atp_2_Final_Project.Models.Com.Tsms
{
    public class User
    {
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Id Is Required!")]
        [MaxLength(12, ErrorMessage = "You Have Exceed The Max length Of User ID which is [12] character!")]
        [RegularExpression("[1-3]{2}-[0-9]{5}-[123]{1}|[1-3]{2}-[0-9]{7}-[123]{1}", ErrorMessage = "Invalid Id,It should [xx]-[xxxxx]-[x] or [xx]-[xxxxxxx]-[x]!")]
        [Display(Name = "User ID")]
        public string UserId { get; set; }
        /// <summary>
        /// ////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Is Required!")]
        [MaxLength(20, ErrorMessage = "Password Max Length Is 20 Character!")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
        /// <summary>
        /// ////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Level Is Required!")]
        [MaxLength(100, ErrorMessage = "The Max Length For User Level Is 100 Character!")]
        [RegularExpression("^(?:admin|Admin|student|Student)$" , ErrorMessage="Invalid User Level!")]
        [Display(Name = "User Level")]
        public string level { get; set; }

        //relationship with other table----

        public List<UserDetail> UserDetail { get; set; }
    }
}