using PMS.Models;
using PMS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategory(int? id, bool includeRelated = true);
        void AddCategory(Category category);
        void RemoveCategory(Category category);
        Task<QueryResult<Category>> GetCategories(Query queryObj);
    }
}
