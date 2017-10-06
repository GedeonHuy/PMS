using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources
{
    public class GroupResource
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public ICollection<EnrollmentResource> Enrollments { get; set; }
        public int ProjectId { get; set; }
        public ProjectResource Project { get; set; }
        public int LecturerId { get; set; }
        public LecturerResource Lecturere { get; set; }
        public int CouncilId { get; set; }
        public CouncilResource Councile { get; set; }
        public ICollection<UploadedFileResource> UploadedFiles { get; set; }
    }
}
