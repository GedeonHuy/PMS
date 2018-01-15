using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources
{
    public class ApplicationUserResource
    {
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string Major { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public ICollection<AnnouncementUserResource> AnnouncementUsers { get; set; }
        public ApplicationUserResource()
        {
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
            AnnouncementUsers = new Collection<AnnouncementUserResource>();
            IsDeleted = false;
        }
    }
}
