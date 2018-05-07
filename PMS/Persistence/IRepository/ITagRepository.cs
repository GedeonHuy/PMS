using PMS.Models;
using PMS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence
{
    public interface ITagRepository
    {
        Task<Tag> GetTag(int? id, bool includeRelated = true);
        void AddTag(Tag tag);
        void RemoveTag(Tag tag);
        Task<QueryResult<Tag>> GetTags(Query filter);
        void UpdateTagProjects(Tag tag, TagResource tagResource);
    }
}
