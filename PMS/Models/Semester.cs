using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Semester
    {
        public int SemesterId { get; set; }
        public string SemesterName { get; set; }
        public DateTime SemesterStart { get; set; }
        public DateTime SemesterEnd { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public Semester()
        {
            Groups = new Collection<Group>();
            Enrollments = new Collection<Enrollment>();
        }
    }
}
