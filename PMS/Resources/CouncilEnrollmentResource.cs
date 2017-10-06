using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources
{
    public class CouncilEnrollmentResource
    {
        public int CouncilEnrollmentId { get; set; }
        public bool IsDeleted { get; set; }
        public int LecturerID { get; set; }
        public LecturerResource Lecturer { get; set; }
        public CouncilResource Council { get; set; }
    }
}
