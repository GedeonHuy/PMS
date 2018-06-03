using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Extensions;
using PMS.Models;
using PMS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PMS.Persistence
{
    public class ProjectRepository : IProjectRepository
    {
        private ApplicationDbContext context;

        public ProjectRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Project> GetProject(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Projects.FindAsync(id);
            }
            return await context.Projects
                .Include(p => p.Groups)
                .Include(p => p.TagProjects)
                    .ThenInclude(tp => tp.Tag)
                .Include(p => p.Major)
                .Include(p => p.Lecturer)
                .SingleOrDefaultAsync(s => s.ProjectId == id);
        }

        public void AddProject(Project project)
        {
            context.Projects.Add(project);
        }

        public void RemoveProject(Project project)
        {
            project.IsDeleted = true;
            //context.Remove(project);
        }

        public async Task<QueryResult<Project>> GetProjects(Query queryObj)
        {
            var result = new QueryResult<Project>();

            var query = context.Projects
                .Where(c => c.IsDeleted == false)
                .Include(p => p.Groups)
                .Include(p => p.TagProjects)
                    .ThenInclude(tp => tp.Tag)
                .Include(p => p.Major)
                .Include(p => p.Lecturer)
                .AsQueryable();

            //filter
            if (queryObj.Type != null)
            {
                query = query.Where(q => q.Type.Equals(queryObj.Type));
            }

            if (queryObj.LecturerId.HasValue)
            {
                query = query.Where(q => q.Lecturer.LecturerId == queryObj.LecturerId.Value);
            }

            if (queryObj.MajorId.HasValue)
            {
                query = query.Where(q => q.Major.MajorId == queryObj.MajorId.Value);
            }

            if (queryObj.TagName != null)
            {
                query = query.Where(q => q.TagProjects.Any(tp => tp.Tag.TagName.Equals(queryObj.TagName)));
            }

            //search
            if (queryObj.ProjectCodeSearch != null)
            {
                query = query.Where(q => q.ProjectCode.ToLower().Contains(queryObj.ProjectCodeSearch.ToLower()));
            }

            if (queryObj.TitleSearch != null)
            {
                query = query.Where(q => q.Title.ToLower().Contains(queryObj.TitleSearch.ToLower()));
            }

            if (queryObj.DescriptionSearch != null)
            {
                query = query.Where(q => q.Description.ToLower().Contains(queryObj.DescriptionSearch.ToLower()));
            }

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Project, object>>>()
            {
                ["title"] = s => s.Title,
                ["type"] = s => s.Type,
                ["code"] = s => s.ProjectCode,
                ["description"] = s => s.Description,
                ["major"] = s => s.Major.MajorName,
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.ProjectId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }

        public void UpdateGroups(Project project, ProjectResource projectResource)
        {
            if (projectResource.Groups != null && projectResource.Groups.Count >= 0)
            {
                //remove old groups
                project.Groups.Clear();

                //add new groups
                var newGroups = context.Groups.Where(e => projectResource.Groups.Any(id => id == e.GroupId)).ToList();
                foreach (var a in newGroups)
                {
                    project.Groups.Add(a);
                }
            }
        }

        public void UpdateTagProjects(Project project, ProjectResource projectResource)
        {
            if (projectResource.TagProjects != null && projectResource.TagProjects.Count >= 0)
            {
                //remove old tagprojects
                var oldTagProjects = project.TagProjects.Where(p => !projectResource.Tags.Any(id => id == p.Tag.TagName)).ToList();
                foreach (TagProject tagprojects in oldTagProjects)
                {
                    tagprojects.IsDeleted = true;
                    project.TagProjects.Remove(tagprojects);
                }
                //project.TagProjects.Clear();

                //add new tagprojects
                var newTags = projectResource.Tags.Where(t => !project.TagProjects.Any(id => id.Tag.TagName == t));
                foreach (var tagName in newTags)
                {
                    var tag = context.Tags.FirstOrDefault(t => t.TagName == tagName);
                    project.TagProjects.Add(new TagProject { IsDeleted = false, Project = project, Tag = tag });
                    //project.TagProjects.Add(a);
                }
            }
        }

        public async Task<QueryResult<Project>> GetProjectsByMajor(int? majorId, Query queryObj)
        {
            var result = new QueryResult<Project>();

            var query = context.Projects
                .Include(p => p.Groups)
                .Include(p => p.TagProjects)
                    .ThenInclude(tp => tp.Tag)
                .Include(p => p.Major)
                .Include(p => p.Lecturer)
                .Where(c => c.IsDeleted == false && c.Major.MajorId == majorId)
                .AsQueryable();

            //filter
            if (queryObj.Type != null)
            {
                query = query.Where(q => q.Type == queryObj.Type);
            }

            if (queryObj.LecturerId.HasValue)
            {
                query = query.Where(q => q.Lecturer.LecturerId == queryObj.LecturerId.Value);
            }

            if (queryObj.MajorId.HasValue)
            {
                query = query.Where(q => q.Major.MajorId == queryObj.MajorId.Value);
            }

            //search
            if (queryObj.ProjectCodeSearch != null)
            {
                query = query.Where(q => q.ProjectCode.ToLower().Contains(queryObj.ProjectCodeSearch.ToLower()));
            }

            if (queryObj.TitleSearch != null)
            {
                query = query.Where(q => q.Title.ToLower().Contains(queryObj.TitleSearch.ToLower()));
            }

            if (queryObj.DescriptionSearch != null)
            {
                query = query.Where(q => q.Description.ToLower().Contains(queryObj.DescriptionSearch.ToLower()));
            }

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Project, object>>>()
            {
                ["title"] = s => s.Title,
                ["type"] = s => s.Type,
                ["code"] = s => s.ProjectCode,
                ["description"] = s => s.Description,
                ["major"] = s => s.Major.MajorName,
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.ProjectId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }
    }
}
