using PMS.Models;
using PMS.Resources.SubResources;
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
        public int? GroupId { get; set; }
        public LecturerInformationResource LecturerInformations { get; set; }
        public ICollection<CouncilEnrollment> CouncilEnrollments { get; set; }
        public CouncilResource()
        {
            CouncilEnrollments = new Collection<CouncilEnrollment>();
        }
    }
}
