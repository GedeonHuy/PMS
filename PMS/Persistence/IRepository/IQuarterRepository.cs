using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.IRepository
{
    public interface IQuarterRepository
    {
        Task<Quarter> GetQuarter(int? id, bool includeRelated = true);
        void AddQuarter(Quarter semester);
        void RemoveQuarter(Quarter semester);
        Task<QueryResult<Quarter>> GetQuarters(Query queryObj);
    }
}
