using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence
{
    public interface ILecturerRepository
    {
        Task<Lecturer> GetLecturer(int? id, bool includeRelated = true);
        void AddLecturer(Lecturer lecturer);
        void RemoveLecturer(Lecturer lecturer);
        Task<QueryResult<Lecturer>> GetLecturers(Query filter);
        Task<IEnumerable<Enrollment>> FinishGroupingAsync(string email, int QuarterId);
        Task<QueryResult<Enrollment>> GetEnrollments(Query queryObj, string email);
        Task<QueryResult<Group>> GetGroups(Query queryObj, string email);
    }
}
