using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TSMS_Atp_2_Final_Project.Models.Com.Tsms
{
    public class Financeofstudent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        /// <summary>
        /// /////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Id Is Required!")]
        [MaxLength(12, ErrorMessage = "The Max length Of User ID is 12 character!")]
        [RegularExpression("[1-3]{2}-[0-9]{5}-[123]{1}|[1-3]{2}-[0-9]{7}-[123]{1}", ErrorMessage = "Invalid Id,It should [xx]-[xxxxx]-[x] or [xx]-[xxxxxxx]-[x]!")]
        [Display(Name = "User ID")]
        public string UserId { get; set; }
        /// <summary>
        /// ////////
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Batch Code Is Required!")]
        [MaxLength(700, ErrorMessage = "The Max Length For Batch Code Is 700 Character!")]
        [Display(Name = "Batch Code")]
        public string batch_code { get; set; }
        /// <summary>
        /// ////////
        /// </summary>
        [Required(ErrorMessage = "Debit Is Required!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid Number!")]
        [Display(Name = "Debit Amount")]
        public int debit { get; set; }
        /// <summary>
        /// ////////
        /// </summary>
        [Required(ErrorMessage = "Credit Is Required!")]
        [RegularExpression("[0-9]*", ErrorMessage = "Invalid Number!")]
        [Display(Name = "Credit Amount")]
        public int credit { get; set; }
        /// <summary>
        /// /////////
        /// </summary>
        [Required(ErrorMessage = "Balance Is Required!")]
        [RegularExpression("[0-9]*", ErrorMessage = "Invalid Number!")]
        [Display(Name = "Current Balance")]
        public int balance { get; set; }
        /// <summary>
        /// //////////
        /// </summary>
        [DataType(DataType.Date, ErrorMessage = "Invalid Date!")]
        [Display(Name = "Last Trunsaction Date")]
        public DateTime lastTrunsaction { get; set; }

        ///relationship with anothere table

        [ForeignKey("UserId")]
        public User User { get; set; }
        /// <summary>
        /// ///////
        /// </summary>
        [ForeignKey("batch_code")]
        public Batche Batche { get; set; }
    }
}