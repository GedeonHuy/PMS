using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PMS.Models.TaskingModels
{
    public class Status
    {
        public int StatusId { get; set; }
        public string Title { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public bool IsDeleted { get; set; }
        public Status()
        {
            Tasks = new Collection<Task>();
            IsDeleted = false;
        }
    }
}