using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "There must be Student's Code !")]
        public string StudentCode { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Enter alphabets only, please !")]
        [MaxLength(40, ErrorMessage = "Maximum length for the name is 40 characters.")]
        public string Name { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "Maximum length for the address is 255 characters.")]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("(\\+84|0)\\d{9,10}", ErrorMessage = "Please, enter a phone number.")]
        public string PhoneNumber { get; set; }
        public Major Major { get; set; }
        public string Year { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public Student()
        {
            Enrollments = new Collection<Enrollment>();
        }

    }
}
