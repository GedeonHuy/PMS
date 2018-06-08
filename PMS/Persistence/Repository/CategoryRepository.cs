using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Extensions;
using PMS.Models;
using PMS.Persistence.IRepository;
using PMS.Resources;
using PMS.Resources.SubResources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PMS.Persistence.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddCategory(Category category)
        {
            context.Categories.Add(category);
        }

        public async Task<QueryResult<Category>> GetCategories(Query queryObj)
        {
            var result = new QueryResult<Category>();

            var query = context.Categories
                .Include(p => p.Project)
                .AsQueryable();

            //filter
            if (queryObj.ProjectId.HasValue)
            {
                query = query.Where(q => q.Project.ProjectId == queryObj.ProjectId.Value);
            }

            //search
            if (queryObj.NameSearch != null)
            {
                query = query.Where(q => q.CategoryName.ToLower().NonUnicode().Contains(queryObj.NameSearch.ToLower().NonUnicode()));
            }

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Category, object>>>()
            {
                ["name"] = s => s.CategoryName,
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.CategoryId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }

        public async Task<Category> GetCategory(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Categories.FindAsync(id);
            }
            return await context.Categories
                .Include(p => p.Project)
                .SingleOrDefaultAsync(s => s.CategoryId == id);
        }

        public void RemoveCategory(Category category)
        {
            category.IsDeleted = true;
        }
    }
}