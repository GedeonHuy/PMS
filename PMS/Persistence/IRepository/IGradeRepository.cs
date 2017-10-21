using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.IRepository
{
    public interface IGradeRepository
    {
        Task<Grade> GetGrade(int? id, bool includeRelated = true);
        void AddGrade(Grade Grade);
        void RemoveGrade(Grade Grade);
        Task<IEnumerable<Grade>> GetGrades();
    }
}
