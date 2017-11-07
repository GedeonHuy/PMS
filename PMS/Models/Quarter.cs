using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Quarter
    {
        public int QuarterId { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Enter alphabets only, please !")]
        [MaxLength(6, ErrorMessage = "Maximum length for the name is 7 characters.")]
        public string QuarterName { get; set; }
        public DateTime QuarterStart { get; set; }
        public DateTime QuarterEnd { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public Quarter()
        {
            Groups = new Collection<Group>();
            Enrollments = new Collection<Enrollment>();
        }
    }
}
