using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Models;
using PMS.Persistence.IRepository;

namespace PMS.Persistence.Repository
{
    public class RoleRepository : IRoleRepository
    {

        private readonly ApplicationDbContext context;

        public RoleRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<ApplicationRole>> GetRoles()
        {
            return await context.ApplicationRole.OrderBy(o => o.Name).ToListAsync();
        }

        public void AddRole(ApplicationRole role)
        {
            context.ApplicationRole.Add(role);
        }

        public async Task<ApplicationRole> GetRole(string id)
        {
            return await context.ApplicationRole.FindAsync(id);
        }
        public void RemoveRole(ApplicationRole role)
        {
            context.Remove(role);
        }
    }
}