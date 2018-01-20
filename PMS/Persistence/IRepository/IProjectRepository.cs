using PMS.Models;
using PMS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence
{
    public interface IProjectRepository
    {
        Task<Project> GetProject(int? id, bool includeRelated = true);
        void AddProject(Project project);
        void RemoveProject(Project project);
        Task<QueryResult<Project>> GetProjects(Query filter);
        void UpdateGroups(Project project, ProjectResource projectResource);
    }
}
