using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Extensions;
using PMS.Models;
using PMS.Persistence.IRepository;
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
            context.Remove(Quarter);
        }

        public async Task<QueryResult<Quarter>> GetQuarters(Query queryObj)
        {
            var result = new QueryResult<Quarter>();

            var query = context.Quarters
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

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }
    }
}
