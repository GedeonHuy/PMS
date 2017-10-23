using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Models;
using PMS.Resources;
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

        public async Task<Group> GetGroup(int? id, bool includeRelated = true)
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
                .Include(p => p.Major)
                .Include(p => p.Quarter)
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
                .Include(p => p.Major)
                .Include(p => p.Quarter)
                .ToListAsync();
        }

        public async Task<bool> CheckGroup(GroupResource group)
        {
            var lecturer = await context.Lecturers
                .Include(l => l.Major)
                .FirstOrDefaultAsync(l => l.LecturerId == group.LecturerId);
            var project = await context.Projects
                .Include(p => p.Major)
                .FirstOrDefaultAsync(p => p.ProjectId == group.ProjectId);
            var major = await context.Majors
                .FirstOrDefaultAsync(m => m.MajorId == group.MajorId);
            if ((major.MajorId != lecturer.Major.MajorId) || (major.MajorId != project.Major.MajorId)
                || (lecturer.Major.MajorId != project.Major.MajorId))
            {
                return false;
            }
            return true;
        }
    }
}
