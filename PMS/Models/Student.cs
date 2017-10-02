using System;
using System.ComponentModel.DataAnnotations;

namespace PMS.Models
{
    public class Student
    {
         public int Id { get; set; }

        [Required]
        [Display(Name = "Student ID")]
        [MinLength(8, ErrorMessage = "Please enter the valid Student ID")]
        public string StudentId { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please choose your Gender !")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please choose your Major !")]
        public string Major { get; set; }

        public string Address { get; set; }

        public bool IsDeleted { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\+?\d{1,3}?[- .]?\(?(?:\d{2,3})\)?[- .]?\d\d\d[- .]?\d\d\d\d$", ErrorMessage = "Entered phone format is not valid.")]
        public string PhoneNumber { get; set; }
    }
}