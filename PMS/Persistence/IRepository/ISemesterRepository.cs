using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.IRepository
{
    public interface ISemesterRepository
    {
        Task<Semester> GetSemester(int? id, bool includeRelated = true);
        void AddSemester(Semester semester);
        void RemoveSemester(Semester semester);
        Task<IEnumerable<Semester>> GetSemesters();
    }
}
