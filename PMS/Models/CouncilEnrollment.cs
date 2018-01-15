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
        public double? Percentage { get; set; }
        public double? Score { get; set; }
        public bool isMarked { get; set; }
        public Lecturer Lecturer { get; set; }
        public Council Council { get; set; }
        public CouncilRole CouncilRole { get; set; }
        public CouncilEnrollment()
        {
            IsDeleted = false;
            isMarked = false;
        }
    }
}
