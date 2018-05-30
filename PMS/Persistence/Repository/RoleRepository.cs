using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Extensions;
using PMS.Models;
using PMS.Persistence.IRepository;

namespace PMS.Persistence.Repository
{
    public class RoleRepository : IRoleRepository
    {

        private readonly ApplicationDbContext context;

        public RoleRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<QueryResult<ApplicationRole>> GetRoles(Query queryObj)
        {
            var result = new QueryResult<ApplicationRole>();

            var query = context.ApplicationRole
                        .AsQueryable();

            //filter
            if (queryObj.Name != null)
            {
                query = query.Where(q => q.Name.Equals(queryObj.Name));
            }

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<ApplicationRole, object>>>()
            {
                ["name"] = s => s.Name,
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.Id);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            result.TotalItems = await query.CountAsync();

            return result;
        }

        public void AddRole(ApplicationRole role)
        {
            context.ApplicationRole.Add(role);
        }

        public async Task<ApplicationRole> GetRole(string id)
        {
            return await context.ApplicationRole.FindAsync(id);
        }
        public void RemoveRole(ApplicationRole role)
        {
            context.Remove(role);
        }
    }
}