using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence
{
    public class LecturerRepository : ILecturerRepository
    {
        private ApplicationDbContext context;

        public LecturerRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Lecturer> GetLecturer(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Lecturers.FindAsync(id);
            }
            return await context.Lecturers
                .Include(l => l.Groups)
                .Include(l => l.CouncilEnrollments)
                .Include(p => p.Major)
                .SingleOrDefaultAsync(s => s.LecturerId == id);
        }

        public void AddLecturer(Lecturer lecturer)
        {
            context.Lecturers.Add(lecturer);
        }

        public void RemoveLecturer(Lecturer lecturer)
        {
            context.Remove(lecturer);
        }

        public async Task<IEnumerable<Lecturer>> GetLecturers(Query filter)
        {
            var query = context.Lecturers
                .Include(l => l.Groups)
                .Include(l => l.CouncilEnrollments)
                .Include(p => p.Major)
                .AsQueryable();

            if (filter.MajorId.HasValue)
            {
                query = query.Where(q => q.Major.MajorId == filter.MajorId.Value);
            }

            return await query.ToListAsync();
        }
    }
}
