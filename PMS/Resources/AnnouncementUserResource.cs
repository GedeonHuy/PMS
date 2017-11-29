using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources
{
    public class AnnouncementUserResource
    {
        public int AnnouncementUserId { get; set; }
        public bool IsDeleted { get; set; }
        public ApplicationUserResource AppUser { get; set; }
        public int AnnouncementId { get; set; }
        public AnnouncementUserResource Announcement { get; set; }
        public AnnouncementUserResource()
        {
            IsDeleted = false;
        }
    }
}
