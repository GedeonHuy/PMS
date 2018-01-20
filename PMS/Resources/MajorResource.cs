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
        public bool isDeleted { get; set; }
        public ICollection<int> Lecturers { get; set; }
        public ICollection<int> Projects { get; set; }
        public ICollection<int> Groups { get; set; }
        public ICollection<int> Students { get; set; }
        public MajorResource()
        {
            Lecturers = new Collection<int>();
            Projects = new Collection<int>();
            Groups = new Collection<int>();
            Students = new Collection<int>();
            isDeleted = false;
        }
    }
}
