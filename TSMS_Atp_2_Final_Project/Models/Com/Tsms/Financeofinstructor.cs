using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TSMS_Atp_2_Final_Project.Models.Com.Tsms
{
    public class Financeofinstructor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        /// <summary>
        /// /////////
        /// </summary>
        [ForeignKey("Instructor"), Column(Order = 0)]
        [Required(ErrorMessage = "Instructor Is Required!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid id!")]
        [Display(Name = "Instructor Id")]
        public int InstructorId { get; set; }
        /// <summary>
        /// ///////
        /// </summary>
        [ForeignKey("Instructor"), Column(Order = 1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Faculty Name Is Required!")]
        [MaxLength(100, ErrorMessage = "The Max Length Of Faculty Name is 100 Character!")]
        [Display(Name = "Instructor Name")]
        public string instructorName { get; set; }
        /// <summary>
        /// ////////////
        /// </summary>
        [Required(ErrorMessage = "Monthly Payment Is Required!")]
        [DataType(DataType.Currency, ErrorMessage = "Invalid Payment!")]
        [Display(Name = "Monthly Payment")]
        public int monthlyPayment { get; set; }
        /// <summary>
        /// /////////
        /// </summary>
        [Required(ErrorMessage = "Net Total Is Required!")]
        [DataType(DataType.Currency,ErrorMessage = "Invalid Net Total!")]
        [Display(Name = "Net Total")]
        public int netTotal { get; set; }
        /// <summary>
        /// ///////////
        /// </summary>
        [Required(ErrorMessage = "Total Paid Is required!")]
        [DataType(DataType.Currency,ErrorMessage = "Invalid Total Paid!")]
        [Display(Name = "Total Paid")]
        public int totalPaid { get; set; }
        /// <summary>
        /// //////
        /// </summary>
        [DataType(DataType.Currency, ErrorMessage = "Invalid Due Amount!")]
        [Display(Name = "Due Amount")]
        public int due { get; set; }
        /// <summary>
        /// /////////
        /// </summary>
        [DataType(DataType.Date, ErrorMessage = "Invalid Date!")]
        public DateTime lastTrunsaction { get; set; }

        ///relationship with anothere table

        public Instructor Instructor { get; set; }
    }
}