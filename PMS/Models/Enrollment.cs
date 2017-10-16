using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
        public bool IsDeleted { get; set; }
        //public int? GradeId { get; set; }
        public Grade Grade { get; set; }
        //public int? StudentId { get; set; }
        public Student Student { get; set; }
        //public int? GroupId { get; set; }
        public Group Group { get; set; }
    }

}
