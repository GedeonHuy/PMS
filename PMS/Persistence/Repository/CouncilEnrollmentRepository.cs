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
    public class CouncilEnrollmentRepository : ICouncilEnrollmentRepository
    {
        private ApplicationDbContext context;

        public CouncilEnrollmentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<CouncilEnrollment> GetCouncilEnrollment(int id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.CouncilEnrollments.FindAsync(id);
            }
            return await context.CouncilEnrollments
                .Include(c => c.Lecturer)
                .Include(c => c.Council)
                .SingleOrDefaultAsync(s => s.CouncilEnrollmentId == id);
        }

        public void AddCouncilEnrollment(CouncilEnrollment CouncilEnrollment)
        {
            context.CouncilEnrollments.Add(CouncilEnrollment);
        }

        public void RemoveCouncilEnrollment(CouncilEnrollment CouncilEnrollment)
        {
            context.Remove(CouncilEnrollment);
        }

        public async Task<IEnumerable<CouncilEnrollment>> GetCouncilEnrollments()
        {
            return await context.CouncilEnrollments
                .ToListAsync();
        }
    }
}
