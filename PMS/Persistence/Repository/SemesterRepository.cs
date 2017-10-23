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
    public class SemesterRepository : ISemesterRepository
    {
        private ApplicationDbContext context;

        public SemesterRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Semester> GetSemester(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Semesters.FindAsync(id);
            }
            return await context.Semesters
                .Include(s => s.Groups)
                .Include(s => s.Enrollments)
                .SingleOrDefaultAsync(s => s.SemesterId == id);
        }

        public void AddSemester(Semester semester)
        {
            context.Semesters.Add(semester);
        }

        public void RemoveSemester(Semester semester)
        {
            context.Remove(semester);
        }

        public async Task<IEnumerable<Semester>> GetSemesters()
        {
            return await context.Semesters
                .Include(s => s.Groups)
                .Include(s => s.Enrollments)
                    .ToListAsync();
        }
    }
}
