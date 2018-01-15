using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;

namespace PMS.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool IsDeleted { get; set; }
        public string Avatar { get; set; }
        public string Major { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public ICollection<AnnouncementUser> AnnouncementUsers { get; set; }
        public ApplicationUser()
        {
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
            IsDeleted = false;
            AnnouncementUsers = new Collection<AnnouncementUser>();
        }
    }
}
