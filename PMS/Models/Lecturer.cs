using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Lecturer
    {
        public int LecturerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "Maximum length for the address is 255 characters.")]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public Major Major { get; set; }
        public ICollection<CouncilEnrollment> CouncilEnrollments { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<Project> Projects { get; set; }
        public Lecturer()
        {
            CouncilEnrollments = new Collection<CouncilEnrollment>();
            Groups = new Collection<Group>();
            Projects = new Collection<Project>();
            IsDeleted = false;
        }
    }
}
