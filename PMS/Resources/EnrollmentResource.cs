using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources
{
    public class EnrollmentResource
    {
        public int EnrollmentId { get; set; }
        public string Type { get; set; }
        public bool IsDeleted { get; set; }
        public string isConfirm { get; set; }
        public int? GradeId { get; set; }
        public string StudentEmail { get; set; }
        public int? GroupId { get; set; }
        public int? QuarterId { get; set; }
        public int? LecturerId { get; set; }
        public GradeResource Grade { get; set; }
        //public int StudentId { get; set; }
        public StudentResource Student { get; set; }
        public GroupResource Group { get; set; }
        public QuarterResource Quarter { get; set; }
        public LecturerResource Lecturer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EnrollmentResource()
        {
            isConfirm = "Pending";
            IsDeleted = false;
        }
    }
}
