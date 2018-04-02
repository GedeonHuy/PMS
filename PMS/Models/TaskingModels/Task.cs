using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models.TaskingModels
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public ICollection<UploadedFile> Attachments { get; set; }
        public ICollection<TaskItem> CheckList { get; set; }
        public ICollection<Comment> Commnets { get; set; }
        public ICollection<Activity> Activities { get; set; }
        public ICollection<Student> Members { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsDeleted { get; set; }

        public Task()
        {
            IsDeleted = false;
            CheckList = new Collection<TaskItem>();
            Commnets = new Collection<Comment>();
            Members = new Collection<Student>();
        }
    }
}
