using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources
{
    public class GroupResource
    {
        public int GroupId { get; set; }
        [Required]
        [MaxLength(30)]
        public string GroupName { get; set; }
        public string LinkGitHub { get; set; }
        public bool isDeleted { get; set; }
        public string ResultGrade { get; set; }
        public string ResultScore { get; set; }
        public string isConfirm { get; set; }
        public int? ProjectId { get; set; }
        public int? LecturerId { get; set; }
        public int? MajorId { get; set; }
        public int? QuarterId { get; set; }
        /// <other project selection>
        public ProjectResource OtherProject { get; set; }
        public ICollection<EnrollmentResource> Enrollments { get; set; }
        public ProjectResource Project { get; set; }
        public LecturerResource Lecturer { get; set; }
        public MajorResource Major { get; set; }
        public QuarterResource Quarter { get; set; }
        public CouncilResource Council { get; set; }
        public ICollection<UploadedFileResource> UploadedFiles { get; set; }
        public GroupResource()
        {
            Enrollments = new Collection<EnrollmentResource>();
            UploadedFiles = new Collection<UploadedFileResource>();
            isConfirm = "Pending";
            isDeleted = false;
        }
    }
}
