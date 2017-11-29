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
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private ApplicationDbContext context;

        public AnnouncementRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Announcement> GetAnnouncement(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Announcement.FindAsync(id);
            }
            return await context.Announcement
                .Include(g => g.AnnouncementUsers)
                .SingleOrDefaultAsync(g => g.AnnouncementId == id);
        }

        public void AddAnnouncement(Announcement Annoucement)
        {
            context.Announcement.Add(Annoucement);
        }

        public void RemoveAnnouncement(Announcement Announcement)
        {
            context.Remove(Announcement);
        }

        public async Task<IEnumerable<Announcement>> GetAnnouncements()
        {
            return await context.Announcement
                    .Include(g => g.AnnouncementUsers)
                    .ToListAsync();
        }


    }
}
