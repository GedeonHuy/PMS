using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence
{
    public interface IGroupRepository
    {
        Task<Group> GetGroup(int id, bool includeRelated = true);
        void AddGroup(Group group);
        void RemoveGroup(Group group);
        Task<IEnumerable<Group>> GetGroups();
    }
}
