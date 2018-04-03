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
    public class ActivityRepository : IActivityRepository
    {
        private ApplicationDbContext context;

        public ActivityRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Activity> GetActivity(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Activities.FindAsync(id);
            }
            return await context.Activities
                .Include(a => a.Task)
                .Include(a => a.User)
                .SingleOrDefaultAsync(g => g.ActivityId == id);
        }

        public void AddActivity(Activity activity)
        {
            context.Activities.Add(activity);
        }

        public void RemoveActivity(Activity activity)
        {
            activity.IsDeleted = true;
            //context.Remove(Activity);
        }

        public async Task<IEnumerable<Activity>> GetActivities()
        {
            return await context.Activities
                    .Include(a => a.Task)
                    .Include(a => a.User)
                    .Where(c => c.IsDeleted == false)
                    .ToListAsync();
        }
    }
}
