using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources
{
    public class AnnouncementResource
    {
        public int AnnouncementId { get; set; }
        public string Title { set; get; }
        public string Content { set; get; }
        public DateTime CreatedDate { get; set; }

        public string UserId { set; get; }
        public ICollection<AnnouncementUserResource> AnnouncementUsers { get; set; }
        public bool IsDeleted { get; set; }
        public AnnouncementResource()
        {
            AnnouncementUsers = new Collection<AnnouncementUserResource>();
            IsDeleted = true;
            CreatedDate = DateTime.Now;
        }
    }
}
