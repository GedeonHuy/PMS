using Microsoft.AspNetCore.Identity;
using PMS.Data;
using PMS.Models;
using PMS.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private UserManager<ApplicationUser> userManager;
        private ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            return context.ApplicationUser.FirstOrDefault(a => a.Email == email);
        }
    }
}
