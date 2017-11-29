using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.IRepository
{
    public interface IAnnouncementUserRepository
    {
        Task<AnnouncementUser> GetAnnouncementUser(int? id, bool includeRelated = true);
        void AddAnnouncementUser(AnnouncementUser AnnouncementUser);
        void RemoveAnnouncementUser(AnnouncementUser AnnouncementUser);
        Task<IEnumerable<AnnouncementUser>> GetAnnouncementUsers();
    }
}
