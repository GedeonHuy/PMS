using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PMS.Models;

namespace PMS.Data
{
    public class RoleSeed
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleSeed(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();

            if (!_context.Roles.Any())
            {
                if (!_context.Roles.Any(r => r.Name == "Admin"))
                {
                    var roleStudent = new ApplicationRole
                    {
                        Name = "Admin",
                        Description = "Can only create Enrollment."
                    };
                    await _roleManager.CreateAsync(roleStudent);
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
}