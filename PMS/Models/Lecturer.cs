using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Lecturer
    {
        public int LecturerId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<CouncilEnrollment> CouncilEnrollments { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<Project> projectRepository { get; set; }
        public Lecturer()
        {
            CouncilEnrollments = new Collection<CouncilEnrollment>();
            Groups = new Collection<Group>();
            projectRepository = new Collection<Project>();
        }
    }
}
