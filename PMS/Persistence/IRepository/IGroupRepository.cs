using PMS.Models;
using PMS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence
{
    public interface IGroupRepository
    {
        Task<Group> GetGroup(int? id, bool includeRelated = true);
        void AddGroup(Group group);
        void RemoveGroup(Group group);
        Task<QueryResult<Group>> GetGroups(Query filter);
        bool CheckEnrollment(Group group, Enrollment enrollment);
        Task<bool> CheckGroup(GroupResource group);
    }
}
