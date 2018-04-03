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
    }
}
