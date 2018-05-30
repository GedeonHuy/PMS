using System.Collections.Generic;
using System.Threading.Tasks;
using PMS.Models;

namespace PMS.Persistence.IRepository
{
    public interface IRoleRepository
    {
        Task<QueryResult<ApplicationRole>> GetRoles(Query queryObj);
        void AddRole(ApplicationRole appRole);
        void RemoveRole(ApplicationRole appRole);
        Task<ApplicationRole> GetRole(string id);

    }
}