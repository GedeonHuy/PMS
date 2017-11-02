using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Extensions;
using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<QueryResult<Lecturer>> GetLecturers(Query queryObj)
        {
            var result = new QueryResult<Lecturer>();

            var query = context.Lecturers
                .Include(l => l.Groups)
                .Include(l => l.CouncilEnrollments)
                .Include(p => p.Major)
                .AsQueryable();

            //filter
            if (queryObj.MajorId.HasValue)
            {
                query = query.Where(q => q.Major.MajorId == queryObj.MajorId.Value);
            }

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Lecturer, object>>>()
            {
                ["name"] = s => s.Name,
                ["major"] = s => s.Major.MajorName,
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.LecturerId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }
    }
}
