using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Major
    {
        public int MajorId { get; set; }
        public string MajorName { get; set; }
        public string MajorCode { get; set; }
        public ICollection<Lecturer> Lecturers { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<Student> Students { get; set; }
        public Major()
        {
            Lecturers = new Collection<Lecturer>();
            Projects = new Collection<Project>();
            Groups = new Collection<Group>();
            Students = new Collection<Student>();
        }
    }
}
