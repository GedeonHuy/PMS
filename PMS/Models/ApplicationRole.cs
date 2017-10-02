using Microsoft.AspNetCore.Identity;

namespace PMS.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
    }

}