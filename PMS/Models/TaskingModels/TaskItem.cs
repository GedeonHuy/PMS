using System;

namespace PMS.Models.TaskingModels
{
    public class TaskItem
    {
        public int TaskItemId { get; set; }
        public String Name { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDone { get; set; }
        public Task Task { get; set; }
        public TaskItem()
        {
            IsDeleted = false;
            IsDone = false;
        }
    }
}