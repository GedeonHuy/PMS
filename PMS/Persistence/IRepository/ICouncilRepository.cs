using PMS.Models;
using PMS.Resources;
using PMS.Resources.SubResources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.IRepository
{
    public interface ICouncilRepository
    {
        Task<Council> GetCouncil(int? id, bool includeRelated = true);
        void AddCouncil(Council council);
        void RemoveCouncil(Council council);
        Task<QueryResult<Council>> GetCouncils(Query queryObj);
        Task AddLecturers(Council council, LecturerInformationResource lecturerInformations);
        void RemoveOldLecturer(Council council);
        string CheckLecturerInformations(LecturerInformationResource lecturerInformations);
        double CalculateScore(Council council);
        void CalculateGrade(Council council);
    }
}
