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
            Quarter.isDeleted = true;
            //context.Remove(Quarter);
        }

        public async Task<QueryResult<Quarter>> GetQuarters(Query queryObj)
        {
            var result = new QueryResult<Quarter>();

            var query = context.Quarters
                .Where(c => c.isDeleted == false)
                .Include(s => s.Groups)
                .Include(s => s.Enrollments)
                .AsQueryable();

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Quarter, object>>>()
            {
                ["name"] = s => s.QuarterName,
                ["start"] = s => s.QuarterStart,
                ["end"] = s => s.QuarterEnd
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.QuarterId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            result.TotalItems = result.Items.Count();

            return result;
        }

        public async Task<Quarter> GetCurrentQuarter()
        {
            var currentDate = DateTime.Now.Date;
            return await context.Quarters
                .Include(s => s.Groups)
                .Include(s => s.Enrollments)
                .FirstOrDefaultAsync(s => s.QuarterStart < currentDate && s.QuarterEnd > currentDate);
        }

        public void UpdateGroups(Quarter quarter, QuarterResource quarterResource)
        {
            if (quarterResource.Groups != null && quarterResource.Groups.Count >= 0)
            {
                quarter.Groups.Clear();

                //add new groups
                var newGroups = context.Groups.Where(e => quarterResource.Groups.Any(id => id == e.GroupId)).ToList();
                foreach (var a in newGroups)
                {
                    quarter.Groups.Add(a);
                }
            }
        }

        public void UpdateEnrollments(Quarter quarter, QuarterResource quarterResource)
        {
            if (quarterResource.Enrollments != null && quarterResource.Enrollments.Count >= 0)
            {
                quarter.Enrollments.Clear();

                //add new groups
                var newEnrollments = context.Enrollments.Where(e => quarterResource.Enrollments.Any(id => id == e.EnrollmentId)).ToList();
                foreach (var a in newEnrollments)
                {
                    quarter.Enrollments.Add(a);
                }
            }
        }
    }
}
