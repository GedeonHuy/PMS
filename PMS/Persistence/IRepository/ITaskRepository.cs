using PMS.Models;
using PMS.Models.TaskingModels;
using PMS.Resources.TaskResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.IRepository
{
    public interface ITaskRepository
    {
        Task<Models.TaskingModels.Task> GetTask(int? id, bool includeRelated = true);
        void AddTask(Models.TaskingModels.Task task);
        void RemoveTask(Models.TaskingModels.Task task);
        Task<IEnumerable<Models.TaskingModels.Task>> GetTasks();
        void UpdateAttachments(Models.TaskingModels.Task task, TaskResource taskResource);
        void UpdateCheckList(Models.TaskingModels.Task task, TaskResource taskResource);
        void UpdateComments(Models.TaskingModels.Task task, TaskResource taskResource);
        void UpdateActivities(Models.TaskingModels.Task task, TaskResource taskResource);
        void UpdateMembers(Models.TaskingModels.Task task, TaskResource taskResource);
    }
}