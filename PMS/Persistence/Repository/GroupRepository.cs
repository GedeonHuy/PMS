using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence
{
    public class GroupRepository : IGroupRepository
    {
        private ApplicationDbContext context;

        public GroupRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Group> GetGroup(int id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Groups.FindAsync(id);
            }
            return await context.Groups
                .Include(p => p.Project)
                .Include(p => p.Enrollments)
                .Include(p => p.UploadedFiles)
                .Include(p => p.Lecturer)
                .SingleOrDefaultAsync(s => s.GroupId == id);
        }

        public void AddGroup(Group group)
        {
            context.Groups.Add(group);
        }

        public void RemoveGroup(Group group)
        {
            context.Remove(group);
        }

        public async Task<IEnumerable<Group>> GetGroups()
        {
            return await context.Groups
                .Include(p => p.Lecturer)
                .Include(p => p.Project)
                .Include(p => p.Enrollments)
                .Include(p => p.UploadedFiles)
                .ToListAsync();
        }
    }
}
