using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string StudentCode { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Major Major { get; set; }
        public string Year { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public Student()
        {
            Enrollments = new Collection<Enrollment>();
        }

    }
}
