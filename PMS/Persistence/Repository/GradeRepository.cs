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
    public class GradeRepository : IGradeRepository
    {
        private ApplicationDbContext context;

        public GradeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Grade> GetGrade(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Grades.FindAsync(id);
            }
            return await context.Grades
                .Include(g => g.Enrollment)
                .SingleOrDefaultAsync(g => g.GradeId == id);
        }

        public void AddGrade(Grade Grade)
        {
            context.Grades.Add(Grade);
        }

        public void RemoveGrade(Grade Grade)
        {
            Grade.IsDeleted = true;
            //context.Remove(Grade);
        }

        public async Task<IEnumerable<Grade>> GetGrades()
        {
            return await context.Grades
                    .Where(c => c.IsDeleted == false)
                    .Include(g => g.Enrollment)
                    .ToListAsync();
        }
    }
}
