using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Models;
using PMS.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.Repository
{
    public class TagProjectRepository : ITagProjectRepository
    {
        private ApplicationDbContext context;

        public TagProjectRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<TagProject> GetTagProject(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.TagProjects.FindAsync(id);
            }
            return await context.TagProjects
                .Include(t => t.Tag)
                .Include(t => t.Project)
                .SingleOrDefaultAsync(g => g.TagProjectId == id);
        }

        public void AddTagProject(TagProject tagProject)
        {
            context.TagProjects.Add(tagProject);
        }

        public void RemoveTagProject(TagProject tagProject)
        {
            tagProject.IsDeleted = true;
            //context.Remove(Grade);
        }

        public async Task<IEnumerable<TagProject>> GetTagProjects()
        {
            return await context.TagProjects
                .Include(t => t.Tag)
                .Include(t => t.Project)
                .ToListAsync();
        }
    }
}
