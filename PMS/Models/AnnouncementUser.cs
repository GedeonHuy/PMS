using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class AnnouncementUser
    {
        public int AnnouncementUserId { get; set; }
        public bool IsDeleted { get; set; }
        public ApplicationUser AppUser { get; set; }
        public Announcement Announcement { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public AnnouncementUser()
        {
            IsDeleted = false;
        }
    }
}
