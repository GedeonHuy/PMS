using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources
{
    public class MajorResource
    {
        public int MajorId { get; set; }
        public string MajorName { get; set; }
        public string MajorCode { get; set; }
        public ICollection<LecturerResource> Lecturers { get; set; }
        public ICollection<ProjectResource> Projects { get; set; }
        public ICollection<GroupResource> Groups { get; set; }
        public ICollection<StudentResource> Students { get; set; }
        public MajorResource()
        {
            Lecturers = new Collection<LecturerResource>();
            Projects = new Collection<ProjectResource>();
            Groups = new Collection<GroupResource>();
            Students = new Collection<StudentResource>();
        }
    }
}
