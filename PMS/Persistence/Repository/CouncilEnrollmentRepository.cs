using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Extensions;
using PMS.Models;
using PMS.Persistence.IRepository;
using PMS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<CouncilEnrollment> GetCouncilEnrollment(int? id, bool includeRelated = true)
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

        public async Task<QueryResult<CouncilEnrollment>> GetCouncilEnrollments(Query queryObj)
        {
            var result = new QueryResult<CouncilEnrollment>();
            var query = context.CouncilEnrollments
                                .Include(c => c.Lecturer)
                .Include(c => c.Council)
                .AsQueryable();

            //filter
            if (queryObj.LecturerId.HasValue)
            {
                query = query.Where(q => q.Lecturer.LecturerId == queryObj.LecturerId.Value);
            }

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<CouncilEnrollment, object>>>()
            {
                ["lecturer"] = s => s.Lecturer.Name,
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.CouncilEnrollmentId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;

        }

        public async Task<CouncilEnrollment> GetCouncilEnrollmentByLecturerEmail(string email, CouncilResource councilResource)
        {
            var councilEnrollment = await context.CouncilEnrollments
                                    .Include(c => c.Lecturer)
                                    .Include(c => c.Council)
                                    .SingleOrDefaultAsync(c => c.Council.CouncilId == councilResource.CouncilId
                                    && c.Lecturer.Email == email);

            return councilEnrollment;
        }

        public async Task<IEnumerable<CouncilEnrollment>> GetCouncilEnrollmentsByCouncilId(int id)
        {
            return await context.CouncilEnrollments
                                .Include(c => c.Council)
                                .Include(c => c.Lecturer)
                                .Include(c => c.CouncilRole)
                                .Where(c => c.Council.CouncilId == id)
                                .ToListAsync();
        }

        public async Task<IEnumerable<CouncilEnrollment>> GetCouncilEnrollmentsByLecturerEmail(string email)
        {   
            return await context.CouncilEnrollments
                                .Include(c => c.Lecturer)
                                .Include(c => c.Council)
                                .Include(c => c.CouncilRole)
                                .Where(c => c.Lecturer.Email == (email + ""))
                                .ToListAsync();
        }
    }
}
