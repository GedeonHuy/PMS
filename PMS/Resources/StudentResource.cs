using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources
{
    public class StudentResource
    {
        public int Id { get; set; }
        public string StudentCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? MajorId { get; set; }
        public string Year { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateOfBirth { get; set; }
        public MajorResource Major { get; set; }
        public ICollection<EnrollmentResource> Enrollments { get; set; }
        public StudentResource()
        {
            Enrollments = new Collection<EnrollmentResource>();
            IsDeleted = false;
        }
    }
}
