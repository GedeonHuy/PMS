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
    public class CouncilRepository : ICouncilRepository
    {
        private ApplicationDbContext context;

        public CouncilRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Council> GetCouncil(int id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Councils.FindAsync(id);
            }
            return await context.Councils
                .Include(c => c.CouncilEnrollments)
                .SingleOrDefaultAsync(s => s.CouncilId == id);
        }

        public void AddCouncil(Council council)
        {
            context.Councils.Add(council);
        }

        public void RemoveCouncil(Council council)
        {
            context.Remove(council);
        }

        public async Task<IEnumerable<Council>> GetCouncils()
        {
            return await context.Councils
                         .Include(c => c.CouncilEnrollments)
                         .ToListAsync();
        }
    }
}
