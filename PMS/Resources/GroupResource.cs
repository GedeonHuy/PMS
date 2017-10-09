using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources
{
    public class GroupResource
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public ICollection<EnrollmentResource> Enrollments { get; set; }
        public ProjectResource Project { get; set; }
        public int LecturerId { get; set; }
        public LecturerResource Lecturer { get; set; }
        public CouncilResource Council { get; set; }
        public ICollection<UploadedFileResource> UploadedFiles { get; set; }
        public GroupResource()
        {
            Enrollments = new Collection<EnrollmentResource>();
            UploadedFiles = new Collection<UploadedFileResource>();
        }
    }
}
