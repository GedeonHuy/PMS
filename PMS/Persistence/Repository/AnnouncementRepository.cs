using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Extensions;
using PMS.Models;
using PMS.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<QueryResult<Announcement>> GetAnnouncements(Query queryObj)
        {
            var result = new QueryResult<Announcement>();
            var query = context.Announcement
                    .Include(g => g.AnnouncementUsers)
                    .AsQueryable();

            //filter
            if (queryObj.CreatedDate != null)
            {
                query = query.Where(q => q.CreatedDate == DateTime.Parse(queryObj.CreatedDate));
            }

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Announcement, object>>>()
            {
                ["title"] = s => s.Title,
                ["date"] = s => s.CreatedDate
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.AnnouncementId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            return await context.ApplicationUser.ToListAsync();
        }
    }
}
