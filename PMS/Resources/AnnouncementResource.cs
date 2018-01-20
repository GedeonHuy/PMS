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
        public string UserId { set; get; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<int> AnnouncementUsers { get; set; }
        public AnnouncementResource()
        {
            AnnouncementUsers = new Collection<int>();
            IsDeleted = false;
            CreatedDate = DateTime.Now;
        }
    }
}
