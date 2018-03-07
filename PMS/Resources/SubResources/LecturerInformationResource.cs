using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources.SubResources
{
    public class LecturerInformationResource
    {
        public ChairResource Chair { get; set; }
        public SecretaryResource Secretary { get; set; }
        public ReviewerResource Reviewer { get; set; }
        public SupervisorResource Supervisor { get; set; }

        public LecturerInformationResource()
        {
            Chair = new ChairResource();
            Secretary = new SecretaryResource();
            Reviewer = new ReviewerResource();
            Supervisor = new SupervisorResource();
        }
    }
}
