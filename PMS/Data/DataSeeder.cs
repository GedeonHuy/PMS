using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PMS.Models;

namespace PMS.Data
{
    public class DataSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public DataSeeder(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();

            if (!_context.Users.Any())
            {

                var user = new ApplicationUser()
                {
                    FullName = "Admin",
                    Email = "a@a.com",
                    UserName = "a@a.com"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result.Succeeded)
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                }
            }
        }
    }
}