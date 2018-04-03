using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Models;
using PMS.Models.TaskingModels;
using PMS.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.Repository
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private ApplicationDbContext context;

        public TaskItemRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<TaskItem> GetTaskItem(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.TaskItems.FindAsync(id);
            }
            return await context.TaskItems
                .Include(t => t.Task)
                .SingleOrDefaultAsync(g => g.TaskItemId == id);
        }

        public void AddTaskItem(TaskItem taskItem)
        {
            context.TaskItems.Add(taskItem);
        }

        public void RemoveTaskItem(TaskItem taskItem)
        {
            taskItem.IsDeleted = true;
            //context.Remove(TaskItem);
        }

        public async Task<IEnumerable<TaskItem>> GetTaskItems()
        {
            return await context.TaskItems
                    .Include(t => t.Task)
                    .Where(c => c.IsDeleted == false)
                    .ToListAsync();
        }
    }
}
