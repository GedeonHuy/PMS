using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources.SubResources
{
    public class LecturerInformationResource
    {
        public PresidentResource President { get; set; }
        public SerectoryResource Serectory { get; set; }
        public ReviewerResource Reviewer { get; set; }
        public SupervisorResource Supervisor { get; set; }
    }
}
