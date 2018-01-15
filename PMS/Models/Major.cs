using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Major
    {
        public int MajorId { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Enter alphabets only, please !")]
        [MaxLength(40, ErrorMessage = "Maximum length for the name is 40 characters.")]
        public string MajorName { get; set; }
        public string MajorCode { get; set; }
        public bool isDeleted { get; set; }
        public ICollection<Lecturer> Lecturers { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<Student> Students { get; set; }
        public Major()
        {
            Lecturers = new Collection<Lecturer>();
            Projects = new Collection<Project>();
            Groups = new Collection<Group>();
            Students = new Collection<Student>();
            isDeleted = false;
        }
    }
}
