using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PMS.Resources
{
    public class TagResource
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public string TagInfo { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<int> TagProjects { get; set; }
        public TagResource()
        {
            IsDeleted = false;
            TagProjects = new Collection<int>();
        }
    }
}