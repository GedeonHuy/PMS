using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.IRepository
{
    public interface IExcelRepository
    {
        Task<Excel> GetExcel(int? id, bool includeRelated = true);
        void AddExcel(Excel excel);
        void RemoveExcel(Excel excel);
        Task<IEnumerable<Excel>> GetExcels();
    }
}
