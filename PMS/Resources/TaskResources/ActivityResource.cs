using PMS.Models;

namespace PMS.Resources.TaskResources
{
    public class ActivityResource
    {
        public int ActivityResourceId { get; set; }
        public int? TaskId { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public ApplicationUserResource User { get; set; }
        public TaskResource Task { get; set; }
        public ActivityResource()
        {
            IsDeleted = false;
        }
    }
}