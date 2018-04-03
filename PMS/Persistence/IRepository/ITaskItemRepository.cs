using PMS.Models;
using PMS.Models.TaskingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.IRepository
{
    public interface ITaskItemRepository
    {
        Task<TaskItem> GetTaskItem(int? id, bool includeRelated = true);
        void AddTaskItem(TaskItem taskItem);
        void RemoveTaskItem(TaskItem taskItem);
        Task<IEnumerable<TaskItem>> GetTaskItems();
    }
}