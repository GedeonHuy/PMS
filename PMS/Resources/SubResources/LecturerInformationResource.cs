using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources.SubResources
{
    public class LecturerInformationResource
    {
        public ChairResource President { get; set; }
        public SecretaryResource Secretary { get; set; }
        public ReviewerResource Reviewer { get; set; }
        public SupervisorResource Supervisor { get; set; }

        public LecturerInformationResource()
        {
            President = new ChairResource();
            Secretary = new SecretaryResource();
            Reviewer = new ReviewerResource();
            Supervisor = new SupervisorResource();
        }
    }
}
