using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }
        public int CouncilId { get; set; }
        public Council Council { get; set; }
        public ICollection<UploadedFile> UploadedFiles { get; set; }

    }
}
