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
        void UpdateTagProjects(Project project, ProjectResource projectResource);
        Task<QueryResult<Project>> GetProjectsByMajor(int? majorId, Query filter);
        Task<QueryResult<Project>> GetSimilarProjects(Query queryObj, ProjectResource projectResource);
        Task UpdateCategories(Project project, Dictionary<string, double> categories);
    }
}
