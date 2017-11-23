using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Council
    {
        public int CouncilId { get; set; }
        public string ResultGrade { get; set; }
        public string ResultScore { get; set; }
        public bool IsDeleted { get; set; }
        public Group Group { get; set; }
        public ICollection<CouncilEnrollment> CouncilEnrollments { get; set; }
        public bool isAllScored { get; set; }
        public Council()
        {
            CouncilEnrollments = new Collection<CouncilEnrollment>();
            isAllScored = false;
        }
    }

}
