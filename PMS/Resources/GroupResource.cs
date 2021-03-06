﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PMS.Resources.SubResources;

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
        public int? BoardId { get; set; }
        /// <other project selection>
        public ProjectResource OtherProject { get; set; }
        public ProjectResource Project { get; set; }
        public LecturerResource Lecturer { get; set; }
        public MajorResource Major { get; set; }
        public QuarterResource Quarter { get; set; }
        public BoardResource Board { get; set; }
        public ICollection<int> UploadedFiles { get; set; }
        public ICollection<int> Enrollments { get; set; }
        public ICollection<int> Tasks { get; set; }
        public ICollection<string> StudentEmails { get; set; } //Students không tồn tại trong Group.cs
        public string LecturerEmail { get; set; }
        public ICollection<string> Comments { get; set; }
        public ICollection<StudentInformationResource> StudentInformations { get; set; }
        public ICollection<UploadFilesInformationResource> UploadFilesInformation { get; set; }

        public GroupResource()
        {
            StudentEmails = new Collection<string>();
            Enrollments = new Collection<int>();
            UploadedFiles = new Collection<int>();
            Tasks = new Collection<int>();
            Comments = new Collection<string>();
            UploadFilesInformation = new Collection<UploadFilesInformationResource>();
            StudentInformations = new Collection<StudentInformationResource>();
            isConfirm = "Pending";
            isDeleted = false;
        }
    }
}
