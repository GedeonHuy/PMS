using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources
{
    public class GradeResource
    {
        public int GradeId { get; set; }
        public string GradeType { get; set; }
        public bool IsDeleted { get; set; }
        public EnrollmentResource Enrollment { get; set; }
        public GradeResource()
        {
            IsDeleted = false;
        }
    }
}
