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
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
        public int? MajorId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public MajorResource Major { get; set; }
        public ICollection<int> CouncilEnrollments { get; set; }
        public ICollection<int> Groups { get; set; }
        public ICollection<int> Projects { get; set; }
        public LecturerResource()
        {
            CouncilEnrollments = new Collection<int>();
            Groups = new Collection<int>();
            Projects = new Collection<int>();
            IsDeleted = false;
        }
    }
}
