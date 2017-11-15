using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Models;
using PMS.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.Repository
{
    public class ExcelRepository : IExcelRepository
    {
        private ApplicationDbContext context;

        public ExcelRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Excel> GetExcel(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Excels.FindAsync(id);
            }
            return await context.Excels
                .SingleOrDefaultAsync(g => g.ExcelId == id);
        }

        public void AddExcel(Excel excel)
        {
            context.Excels.Add(excel);
        }

        public void RemoveExcel(Excel excel)
        {
            context.Excels.Remove(excel);
        }

        public async Task<IEnumerable<Excel>> GetExcels()
        {
            return await context.Excels
                    .ToListAsync();
        }
    }
}
