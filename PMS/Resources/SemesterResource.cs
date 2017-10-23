using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources
{
    public class SemesterResource
    {
        public int SemesterId { get; set; }
        public string SemesterName { get; set; }
        public DateTime SemesterStart { get; set; }
        public DateTime SemesterEnd { get; set; }
        public ICollection<GroupResource> Groups { get; set; }
        public ICollection<EnrollmentResource> Enrollments { get; set; }
        public SemesterResource()
        {
            Groups = new Collection<GroupResource>();
            Enrollments = new Collection<EnrollmentResource>();
        }
    }
}
