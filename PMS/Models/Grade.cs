using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public string GradeType { get; set; }
        public string IsDeleted { get; set; }
        public int EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; }
    }
}
