using PMS.Models;
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
        Task<IEnumerable<Council>> GetCouncils();
        Task AddLecturers(Council council, ICollection<LecturerInformationResource> lecturerInformations);
    }
}
