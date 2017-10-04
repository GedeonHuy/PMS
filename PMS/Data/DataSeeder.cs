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

        private readonly RoleManager<IdentityRole> _roleManager;
        public DataSeeder(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
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

            if (!_context.Roles.Any(r => r.Name == "Student"))
            {
                var roleStudent = new ApplicationRole
                {
                    Name = "Student",
                    Description = "Can only create Enrollment."
                };
                await _roleManager.CreateAsync(roleStudent);
            }

            if (!_context.Roles.Any(r => r.Name == "Lecturer"))
            {
                var roleLecturer = new ApplicationRole
                {
                    Name = "Lecturer",
                    Description = "Can create Project and upload files."
                };
                await _roleManager.CreateAsync(roleLecturer);
            }
        }
    }
}