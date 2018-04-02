using System;
namespace PMS.Models.TaskingModels
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public ApplicationUser User { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public Task Task { get; set; }
        public Activity()
        {
            IsDeleted = false;
        }
    }
}
