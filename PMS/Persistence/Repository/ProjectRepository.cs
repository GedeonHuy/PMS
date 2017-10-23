using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await context.Projects
                .Include(p => p.Groups)
                .Include(p => p.Major)
                .ToListAsync();
        }
    }
}
