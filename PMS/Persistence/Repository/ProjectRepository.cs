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
    public class ProjectRepository : IProjectRepository
    {
        private ApplicationDbContext context;

        public ProjectRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Project> GetProject(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Projects.FindAsync(id);
            }
            return await context.Projects
                .Include(p => p.Groups)
                .Include(p => p.Major)
                .SingleOrDefaultAsync(s => s.ProjectId == id);
        }

        public void AddProject(Project project)
        {
            context.Projects.Add(project);
        }

        public void RemoveProject(Project project)
        {
            context.Remove(project);
        }

        public async Task<IEnumerable<Project>> GetProjects(Query queryObj)
        {
            var query = context.Projects
                .Include(p => p.Groups)
                .Include(p => p.Major)
                .AsQueryable();

            //filter
            if (queryObj.Type != null)
            {
                query = query.Where(q => q.Type == queryObj.Type);
            }

            if (queryObj.LecturerId.HasValue)
            {
                query = query.Where(q => q.Lecturer.LecturerId == queryObj.LecturerId.Value);
            }

            if (queryObj.MajorId.HasValue)
            {
                query = query.Where(q => q.Major.MajorId == queryObj.MajorId.Value);
            }

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Project, object>>>()
            {
                ["title"] = s => s.Title,
                ["type"] = s => s.Type,
                ["code"] = s => s.ProjectCode,
                ["description"] = s => s.Description,
                ["major"] = s => s.Major.MajorName,
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.ProjectId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            return await query.ToListAsync();
        }
    }
}
