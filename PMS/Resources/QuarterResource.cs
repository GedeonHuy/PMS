using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources
{
    public class QuarterResource
    {
        public int QuarterId { get; set; }
        public string QuarterName { get; set; }
        public DateTime QuarterStart { get; set; }
        public DateTime QuarterEnd { get; set; }
        public ICollection<GroupResource> Groups { get; set; }
        public ICollection<EnrollmentResource> Enrollments { get; set; }
        public QuarterResource()
        {
            Groups = new Collection<GroupResource>();
            Enrollments = new Collection<EnrollmentResource>();
        }
    }
}
