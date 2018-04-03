using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PMS.Resources.TaskResources
{
    public class StatusResource
    {
        public int StatusId { get; set; }
        public string Title { get; set; }
        public ICollection<int> Tasks { get; set; }
        public bool IsDeleted { get; set; }
        public StatusResource()
        {
            Tasks = new Collection<int>();
            IsDeleted = false;
        }
    }
}