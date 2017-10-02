using PMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources
{
    public class StudentResource
    {
        public int StudentId { get; set; }
        public string StudentCode { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Major { get; set; }
        public string Year { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<int> Enrollments { get; set; }
        public StudentResource()
        {
            Enrollments = new Collection<int>();
        }
    }
}
