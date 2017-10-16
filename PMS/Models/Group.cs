using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public Project Project { get; set; }
        public Lecturer Lecturer { get; set; }
        public Council Council { get; set; }
        public bool isDeleted { get; set; }
        public bool isConfirm { get; set; }
        public ICollection<UploadedFile> UploadedFiles { get; set; }
        public Group()
        {
            Enrollments = new Collection<Enrollment>();
            UploadedFiles = new Collection<UploadedFile>();
        }

    }
}
