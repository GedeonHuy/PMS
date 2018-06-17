using PMS.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMS.Models;
using PMS.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using PMS.Extensions;

namespace PMS.Persistence.Repository
{
    public class CategoryProjectRepository : ICategoryProjectRepository
    {
        private ApplicationDbContext context;

        public CategoryProjectRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void AddCategoryProject(CategoryProject categoryProject)
        {
            context.CategoryProjects.Add(categoryProject);
        }

        public async Task<CategoryProject> GetCategoryProject(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.CategoryProjects.FindAsync(id);
            }
            return await context.CategoryProjects
                .Include(g => g.Project)
                .Include(g => g.Category)
                .SingleOrDefaultAsync(g => g.CategoryProjectId == id);
        }
        public async Task<QueryResult<CategoryProject>> GetCategoryProjects(Query queryObj)
        {
            var result = new QueryResult<CategoryProject>();
            var query = context.CategoryProjects
                                .Where(c => c.IsDeleted == false)
                                .Include(c => c.Project)
                                .Include(c => c.Category)
                                .AsQueryable();

            //filter


            //sort
            var columnsMap = new Dictionary<string, Expression<Func<CategoryProject, object>>>()
            {

            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.CategoryProjectId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;

        }

        public void RemoveCategoryProject(CategoryProject categoryProjects)
        {
            categoryProjects.IsDeleted = true;
            //context.Remove(AnnouncementUser);
        }
    }
}
