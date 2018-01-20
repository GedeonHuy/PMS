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
        public bool isDeleted { get; set; }
        public DateTime QuarterStart { get; set; }
        public DateTime QuarterEnd { get; set; }
        public ICollection<int> Groups { get; set; }
        public ICollection<int> Enrollments { get; set; }
        public QuarterResource()
        {
            Groups = new Collection<int>();
            Enrollments = new Collection<int>();
            isDeleted = false;
        }
    }
}
