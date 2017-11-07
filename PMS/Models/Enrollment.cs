using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
        public bool IsDeleted { get; set; }
        public string isConfirm { get; set; }
        public Grade Grade { get; set; }
        public Quarter Quarter { get; set; }
        public Student Student { get; set; }
        public Lecturer Lecturer { get; set; }
        public Group Group { get; set; }
        public Enrollment()
        {
            isConfirm = "Pending";
        }
    }

}
