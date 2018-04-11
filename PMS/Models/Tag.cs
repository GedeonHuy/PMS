using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace PMS.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public string TagInfo { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<TagProject> TagProjects { get; set; }
        public Tag()
        {
            IsDeleted = false;
            TagProjects = new Collection<TagProject>();
        }
    }
}