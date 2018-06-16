using PMS.Models;
using PMS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.IRepository
{
    public interface ICategoryProjectRepository
    {
        Task<CategoryProject> GetCategoryProject(int? id, bool includeRelated = true);
        void AddCategoryProject(CategoryProject categoryProject);
        void RemoveCategoryProject(CategoryProject categoryProject);
        Task<QueryResult<CategoryProject>> GetCategoryProjects(Query queryObj);
    }
}
