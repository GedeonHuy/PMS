using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class CouncilEnrollment
    {
        public int CouncilEnrollmentId { get; set; }
        public bool IsDeleted { get; set; }
        public Lecturer Lecturer { get; set; }
        public Council Council { get; set; }
        public double? Percentage { get; set; }
        public bool isMark { get; set; }
        public CouncilRole CouncilRole { get; set; }
    }
}
