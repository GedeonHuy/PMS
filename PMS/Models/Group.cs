using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace PMS.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string LinkGitHub { get; set; }
        public bool isDeleted { get; set; }
        public string isConfirm { get; set; }
        public string ResultGrade { get; set; }
        public string ResultScore { get; set; }
        public Project Project { get; set; }
        public Major Major { get; set; }
        public Lecturer Lecturer { get; set; }
        public Board Board { get; set; }
        public Quarter Quarter { get; set; }
        public ICollection<UploadedFile> UploadedFiles { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<TaskingModels.Task> Tasks { get; set; }
        public Group()
        {
            Enrollments = new Collection<Enrollment>();
            UploadedFiles = new Collection<UploadedFile>();
            Tasks = new Collection<TaskingModels.Task>();
            isConfirm = "Pending";
            isDeleted = false;
        }

    }
}
