using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Announcement
    {
        public int AnnouncementId { get; set; }
        [StringLength(250)]
        [Required]
        public string Title { set; get; }
        public string Content { set; get; }
        public bool IsDeleted { get; set; }
        [StringLength(128)]
        public string UserId { set; get; }
        public DateTime CreatedDate { get; set; }
        public ICollection<AnnouncementUser> AnnouncementUsers { get; set; }
        public Announcement()
        {
            AnnouncementUsers = new Collection<AnnouncementUser>();
            IsDeleted = false;
            CreatedDate = DateTime.Now;
        }
    }
}
