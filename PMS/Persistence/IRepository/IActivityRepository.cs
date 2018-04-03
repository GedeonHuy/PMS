using System.Collections.Generic;
using System.Threading.Tasks;
using PMS.Models.TaskingModels;

namespace PMS.Persistence.IRepository
{
    public interface IActivityRepository
    {
        Task<Activity> GetActivity(int? id, bool includeRelated = true);
        void AddActivity(Activity activity);
        void RemoveActivity(Activity activity);
        Task<IEnumerable<Activity>> GetActivities();
    }
}