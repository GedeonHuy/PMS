using System.Collections.Generic;
using System.Threading.Tasks;
using PMS.Models.TaskingModels;

namespace PMS.Persistence.IRepository
{
    public interface IStatusRepository
    {
        Task<Status> GetStatus(int? id, bool includeRelated = true);
        void AddStatus(Status status);
        void RemoveStatus(Status status);
        Task<IEnumerable<Status>> GetStatuses();
    }
}