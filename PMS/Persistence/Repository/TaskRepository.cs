using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Models;
using PMS.Models.TaskingModels;
using PMS.Persistence.IRepository;
using PMS.Resources.TaskResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private ApplicationDbContext context;

        public TaskRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Models.TaskingModels.Task> GetTask(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Tasks.FindAsync(id);
            }
            return await context.Tasks
                .Include(g => g.Status)
                .Include(g => g.Group)
                .SingleOrDefaultAsync(g => g.TaskId == id);
        }

        public void AddTask(Models.TaskingModels.Task task)
        {
            context.Tasks.Add(task);
        }

        public void RemoveTask(Models.TaskingModels.Task task)
        {
            task.IsDeleted = true;
            //context.Remove(Task);
        }

        public async Task<IEnumerable<Models.TaskingModels.Task>> GetTasks()
        {
            return await context.Tasks
                    .Include(g => g.Status)
                    .Include(g => g.Group)
                    .Where(c => c.IsDeleted == false)
                    .ToListAsync();
        }

        public void UpdateAttachments(Models.TaskingModels.Task task, TaskResource taskResource)
        {
            if (taskResource.Attachments != null && taskResource.Attachments.Count >= 0)
            {
                //remove old attchments
                task.Attachments.Clear();

                //add new attchments
                var newAttachments = context.UploadedFiles.Where(e => taskResource.Attachments.Any(id => id == e.UploadedFileId)).ToList();
                foreach (var a in newAttachments)
                {
                    task.Attachments.Add(a);
                }
            }
        }

        public void UpdateCheckList(Models.TaskingModels.Task task, TaskResource taskResource)
        {
            if (taskResource.CheckList != null && taskResource.CheckList.Count >= 0)
            {
                //remove old taskitems
                task.CheckList.Clear();

                //add new taskitems
                var newTaskItems = context.TaskItems.Where(e => taskResource.CheckList.Any(id => id == e.TaskItemId)).ToList();
                foreach (var a in newTaskItems)
                {
                    task.CheckList.Add(a);
                }
            }
        }

        public void UpdateComments(Models.TaskingModels.Task task, TaskResource taskResource)
        {
            if (taskResource.Commnets != null && taskResource.Commnets.Count >= 0)
            {
                //remove old comments
                task.Commnets.Clear();

                //add new comments
                var newComments = context.Comments.Where(e => taskResource.Commnets.Any(id => id == e.CommentId)).ToList();
                foreach (var a in newComments)
                {
                    task.Commnets.Add(a);
                }
            }
        }

        public void UpdateActivities(Models.TaskingModels.Task task, TaskResource taskResource)
        {
            if (taskResource.Activities != null && taskResource.Activities.Count >= 0)
            {
                //remove old activities
                task.Activities.Clear();

                //add new activities
                var newActivities = context.Activities.Where(e => taskResource.Activities.Any(id => id == e.ActivityId)).ToList();
                foreach (var a in newActivities)
                {
                    task.Activities.Add(a);
                }
            }
        }

        public void UpdateMembers(Models.TaskingModels.Task task, TaskResource taskResource)
        {
            if (taskResource.Members != null && taskResource.Members.Count >= 0)
            {
                //remove old members
                task.Members.Clear();

                //add new members
                var newMembers = context.Students.Where(e => taskResource.Members.Any(id => id == e.Id)).ToList();
                foreach (var a in newMembers)
                {
                    task.Members.Add(a);
                }
            }
        }
    }
}
