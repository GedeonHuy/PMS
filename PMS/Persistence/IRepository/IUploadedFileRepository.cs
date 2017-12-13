using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.IRepository
{
    public interface IUploadedFileRepository
    {
        Task<UploadedFile> GetUploadedFile(int? id, bool includeRelated = true);
        void AddUploadedFile(UploadedFile uploadedFile);
        void RemoveUploadedFile(UploadedFile uploadedFile);
        Task<IEnumerable<UploadedFile>> GetUploadedFiles();
    }
}
