using PMS.Models;
using PMS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.IRepository
{
    public interface IMajorRepository
    {
        Task<Major> GetMajor(int? id, bool includeRelated = true);
        void AddMajor(Major major);
        void RemoveMajor(Major major);
        Task<QueryResult<Major>> GetMajors(Query queryObj);
        void UpdateLecturers(Major major, MajorResource majorResource);
        void UpdateProjects(Major major, MajorResource majorResource);
        void UpdateGroups(Major major, MajorResource majorResource);
        void UpdateStudents(Major major, MajorResource majorResource);
    }
}
