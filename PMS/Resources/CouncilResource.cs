using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources
{
    public class CouncilResource
    {
        public int CouncilId { get; set; }
        public string ResultGrade { get; set; }
        public string ResultScore { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<CouncilEnrollmentResource> CouncilEnrollments { get; set; }
        public CouncilResource()
        {
            CouncilEnrollments = new Collection<CouncilEnrollmentResource>();
        }
    }
}
