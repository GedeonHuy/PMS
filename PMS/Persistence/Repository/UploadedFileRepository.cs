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
    public class UploadedFileRepository : IUploadedFileRepository
    {
        private ApplicationDbContext context;

        public UploadedFileRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<UploadedFile> GetUploadedFile(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.UploadedFiles.FindAsync(id);
            }
            return await context.UploadedFiles
                .Include(g => g.Group)
                .SingleOrDefaultAsync(g => g.UploadedFileId == id);
        }

        public void AddUploadedFile(UploadedFile uploadedFile)
        {
            context.UploadedFiles.Add(uploadedFile);
        }

        public void RemoveUploadedFile(UploadedFile uploadedFile)
        {
            context.Remove(uploadedFile);
        }

        public async Task<IEnumerable<UploadedFile>> GetUploadedFiles()
        {
            return await context.UploadedFiles
                    .Include(g => g.Group)
                    .ToListAsync();
        }
    }
}
