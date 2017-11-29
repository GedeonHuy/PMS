using PMS.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMS.Models;
using PMS.Data;
using Microsoft.EntityFrameworkCore;

namespace PMS.Persistence.Repository
{
    public class AnnouncementUserRepository : IAnnouncementUserRepository
    {
        private ApplicationDbContext context;

        public AnnouncementUserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void AddAnnouncementUser(AnnouncementUser AnnouncementUser)
        {
            context.AnnouncementUser.Add(AnnouncementUser);
        }

        public async Task<AnnouncementUser> GetAnnouncementUser(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.AnnouncementUser.FindAsync(id);
            }
            return await context.AnnouncementUser
                .Include(g => g.Announcement)
                .Include(g => g.AppUser)
                .SingleOrDefaultAsync(g => g.AnnouncementUserId == id);
        }

        public Task<AnnouncementUser> GetAnnouncementUserAsync(int? id, bool includeRelated = true)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AnnouncementUser>> GetAnnouncementUsers()
        {
            return await context.AnnouncementUser
                .Include(g => g.Announcement)
                .Include(g => g.AppUser)
                    .ToListAsync();
        }


        public void RemoveAnnouncementUser(AnnouncementUser AnnouncementUser)
        {
            context.Remove(AnnouncementUser);
        }
    }
}
