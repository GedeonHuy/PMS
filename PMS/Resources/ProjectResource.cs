using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources
{
    public class ProjectResource
    {
        public int ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsCompleted { get; set; }
        public int? MajorId { get; set; }
        public int? LecturerId { get; set; }
        public ICollection<int> Groups { get; set; }
        public ICollection<int> TagProjects { get; set; }
        public ICollection<int> Tags { get; set; }
        public MajorResource Major { get; set; }
        public LecturerResource Lecturer { get; set; }

        public ProjectResource()
        {
            Groups = new Collection<int>();
            TagProjects = new Collection<int>();
            Tags = new Collection<int>();
            IsCompleted = false;
            IsDeleted = false;
        }
    }
}
