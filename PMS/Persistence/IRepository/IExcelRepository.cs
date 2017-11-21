using OfficeOpenXml;
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
        Task AddStudent(Student student, ExcelWorksheet worksheet, int row);
        Task AddLecturer(Lecturer lecturer, ExcelWorksheet worksheet, int row);
        Task AddProject(Project project, ExcelWorksheet worksheet, int row);
    }
}
