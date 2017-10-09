using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence
{
    public class UserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        //public Task<ApplicationUser> GetCurrentUserAsync()
        //{
        //    return userManager.GetUserAsync(HttpContext.User);
        //}
    }
}
