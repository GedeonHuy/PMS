using System;
using System.Collections.Generic;
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
        public string Desciption { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<GroupResource> Group { get; set; }
    }
}
