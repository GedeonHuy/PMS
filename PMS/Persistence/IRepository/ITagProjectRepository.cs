using PMS.Models;
using PMS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence
{
    public interface ITagProjectRepository
    {
        Task<TagProject> GetTagProject(int? id, bool includeRelated = true);
        void AddTagProject(TagProject tagProject);
        void RemoveTagProject(TagProject tagProject);
        Task<IEnumerable<TagProject>> GetTagProjects();
    }
}