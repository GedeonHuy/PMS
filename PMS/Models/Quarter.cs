using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Quarter
    {
        public int QuarterId { get; set; }
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
