using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Models.TaskingModels;
using PMS.Persistence.IRepository;

namespace PMS.Persistence.Repository
{
    public class StatusRepository : IStatusRepository
    {
        private ApplicationDbContext context;

        public StatusRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddStatus(Status status)
        {
            context.Statuses.Add(status);
        }

        public async Task<Status> GetStatus(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Statuses.FindAsync(id);
            }
            return await context.Statuses
                    .SingleOrDefaultAsync(g => g.StatusId == id);
        }

        public async Task<IEnumerable<Status>> GetStatuses()
        {
            return await context.Statuses
                    .Where(s => s.IsDeleted == false)
                    .ToListAsync();
        }

        public void RemoveStatus(Status status)
        {
            status.IsDeleted = true;
        }
    }
}