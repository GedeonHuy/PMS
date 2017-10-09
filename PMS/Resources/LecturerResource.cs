using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources
{
    public class LecturerResource
    {
        public int LecturerId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<CouncilEnrollmentResource> CouncilEnrollments { get; set; }
        public ICollection<GroupResource> Groups { get; set; }
        public LecturerResource()
        {
            CouncilEnrollments = new Collection<CouncilEnrollmentResource>();
            Groups = new Collection<GroupResource>();
        }
    }
}
