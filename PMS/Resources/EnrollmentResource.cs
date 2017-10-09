using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources
{
    public class EnrollmentResource
    {
        public int EnrollmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsDeleted { get; set; }
        public GradeResource Grade { get; set; }
        //public int StudentId { get; set; }
        //public Student Student { get; set; }
        public GroupResource Group { get; set; }
    }
}
