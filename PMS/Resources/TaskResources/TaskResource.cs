using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PMS.Resources.TaskResources
{
    public class TaskResource
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? GroupId { get; set; }
        public int? StatusId { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsLate { get; set; }
        public GroupResource Group { get; set; }
        public StatusResource Status { get; set; }
        public ICollection<int> Attachments { get; set; }
        public ICollection<int> CheckList { get; set; }
        public ICollection<int> Commnets { get; set; }
        public ICollection<int> Activities { get; set; }
        public ICollection<int> Members { get; set; }
        public TaskResource()
        {
            IsDeleted = false;
            IsLate = false;
            CheckList = new Collection<int>();
            Commnets = new Collection<int>();
            Members = new Collection<int>();
            Activities = new Collection<int>();
            Attachments = new Collection<int>();
        }
    }
}