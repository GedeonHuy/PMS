using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Models;
using PMS.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.Repository
{
    public class QuarterRepository : IQuarterRepository
    {
        private ApplicationDbContext context;

        public QuarterRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Quarter> GetQuarter(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Quarters.FindAsync(id);
            }
            return await context.Quarters
                .Include(s => s.Groups)
                .Include(s => s.Enrollments)
                .SingleOrDefaultAsync(s => s.QuarterId == id);
        }

        public void AddQuarter(Quarter Quarter)
        {
            context.Quarters.Add(Quarter);
        }

        public void RemoveQuarter(Quarter Quarter)
        {
            context.Remove(Quarter);
        }

        public async Task<IEnumerable<Quarter>> GetQuarters()
        {
            return await context.Quarters
                .Include(s => s.Groups)
                .Include(s => s.Enrollments)
                    .ToListAsync();
        }
    }
}
