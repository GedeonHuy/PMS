using System.Collections.Generic;
using System.Threading.Tasks;
using PMS.Models;

namespace PMS.Persistence.IRepository
{
    public interface IRoleRepository
    {
        Task<IEnumerable<ApplicationRole>> GetRoles();
        void AddRole(ApplicationRole appRole);
        void RemoveRole(ApplicationRole appRole);
        Task<ApplicationRole> GetRole(string id);

    }
}