using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Extensions;
using PMS.Models;
using PMS.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                    .ThenInclude(tp => tp.Board)
                .Include(p => p.TagProjects)
                    .ThenInclude(tp => tp.Tag)
                .Include(p => p.Major)
                .Include(p => p.Lecturer)
                .Include(p => p.CategoryProjects)
                    .ThenInclude(cp => cp.Category)
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
                    .ThenInclude(tp => tp.Board)
                .Include(p => p.TagProjects)
                    .ThenInclude(tp => tp.Tag)
                .Include(p => p.Major)
                .Include(p => p.Lecturer)
                .Include(p => p.CategoryProjects)
                    .ThenInclude(cp => cp.Category)
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

            if (queryObj.ResultScore != null)
            {
                query = query.Where(q => q.Groups.Any(qg => Convert.ToDouble(qg.Board.ResultScore) >= Convert.ToDouble(queryObj.ResultScore)));
            }

            //search
            if (queryObj.ProjectCodeSearch != null)
            {
                query = query.Where(q => q.ProjectCode.ToLower().NonUnicode().Contains(queryObj.ProjectCodeSearch.ToLower().NonUnicode()));
            }

            if (queryObj.TitleSearch != null)
            {
                query = query.Where(q => q.Title.ToLower().NonUnicode().Contains(queryObj.TitleSearch.ToLower().NonUnicode()));
            }

            if (queryObj.DescriptionSearch != null)
            {
                query = query.Where(q => q.Description.ToLower().NonUnicode().Contains(queryObj.DescriptionSearch.ToLower().NonUnicode()));
            }

            if (queryObj.IsNotAssigned == true)
            {
                query = query.Where(q => q.Groups == null || q.Groups.Count == 0);
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

        public async Task UpdateCategories(Project project, Dictionary<string, double> categories)
        {
            if (categories != null && categories.Count >= 0)
            {
                //remove old categories
                var oldCategoryProjects = project.CategoryProjects.Where(p => !categories.Any(id => id.Key == p.Category.CategoryName)).ToList();
                foreach (CategoryProject categoryProject in oldCategoryProjects)
                {
                    categoryProject.IsDeleted = true;
                    project.CategoryProjects.Remove(categoryProject);
                }
                //project.TagProjects.Clear();

                //add new tagprojects
                var newCategories = categories.Where(t => !project.CategoryProjects.Any(id => id.Category.CategoryName == t.Key));
                foreach (var categoryInformation in newCategories)
                {
                    var categoryProject = await context.CategoryProjects.FirstOrDefaultAsync(c => c.Category.CategoryName == categoryInformation.Key);
                    if (categoryProject == null)
                    {
                        var category = new Category
                        {
                            CategoryName = categoryInformation.Key,
                            Confidence = categoryInformation.Value,
                            IsDeleted = false
                        };

                        project.CategoryProjects.Add(new CategoryProject
                        {
                            Category = category,
                            IsDeleted = false,
                            Project = project
                        });
                    }
                    else
                    {
                        project.CategoryProjects.Add(categoryProject);
                    }
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
                .Include(p => p.CategoryProjects)
                    .ThenInclude(cp => cp.Category)
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

            if (queryObj.IsNotAssigned == true)
            {
                query = query.Where(q => q.Groups == null || q.Groups.Count == 0);
            }

            //search
            if (queryObj.ProjectCodeSearch != null)
            {
                query = query.Where(q => q.ProjectCode.ToLower().NonUnicode().Contains(queryObj.ProjectCodeSearch.ToLower().NonUnicode()));
            }

            if (queryObj.TitleSearch != null)
            {
                query = query.Where(q => q.Title.ToLower().NonUnicode().Contains(queryObj.TitleSearch.ToLower().NonUnicode()));
            }

            if (queryObj.DescriptionSearch != null)
            {
                query = query.Where(q => q.Description.ToLower().NonUnicode().Contains(queryObj.DescriptionSearch.ToLower().NonUnicode()));
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

        public async Task<QueryResult<Project>> GetSimilarProjects(Query queryObj, ProjectResource projectResource)
        {
            var result = new QueryResult<Project>();

            var searchtermTitle = projectResource.Title.NonUnicode();
            var searchtermDescription = projectResource.Description.NonUnicode();

            var query = context.Projects
                .Where(c => c.IsDeleted == false
                 && (searchtermTitle.CalculateSimilarity(c.Title.NonUnicode()) > 0.3
                  || searchtermTitle.CalculateSimilarity(c.Description.NonUnicode()) > 0.3)
                  || searchtermDescription.CalculateSimilarity(c.Title.NonUnicode()) > 0.3
                  || searchtermDescription.CalculateSimilarity(c.Description.NonUnicode()) > 0.3)
                .Include(p => p.Groups)
                    .ThenInclude(tp => tp.Board)
                .Include(p => p.TagProjects)
                    .ThenInclude(tp => tp.Tag)
                .Include(p => p.Major)
                .Include(p => p.Lecturer)
                .Include(p => p.CategoryProjects)
                    .ThenInclude(cp => cp.Category)
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

            if (queryObj.IsNotAssigned == true)
            {
                query = query.Where(q => q.Groups == null || q.Groups.Count == 0);
            }

            //search
            if (queryObj.ProjectCodeSearch != null)
            {
                query = query.Where(q => q.ProjectCode.ToLower().NonUnicode().Contains(queryObj.ProjectCodeSearch.ToLower().NonUnicode()));
            }

            if (queryObj.TitleSearch != null)
            {
                query = query.Where(q => q.Title.ToLower().NonUnicode().Contains(queryObj.TitleSearch.ToLower().NonUnicode()));
            }

            if (queryObj.DescriptionSearch != null)
            {
                query = query.Where(q => q.Description.ToLower().NonUnicode().Contains(queryObj.DescriptionSearch.ToLower().NonUnicode()));
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
            // var projects = await context.Projects
            //     .Where(c => c.IsDeleted == false)
            //     .Include(p => p.Groups)
            //     .Include(p => p.TagProjects)
            //         .ThenInclude(tp => tp.Tag)
            //     .Include(p => p.Major)
            //     .Include(p => p.Lecturer)
            //     .ToListAsync();
            // foreach (var project in projects)
            // {
            //     var a = searchterm.CalculateSimilarity(project.Title.NonUnicode());
            //     var b = searchterm.CalculateSimilarity(project.Description.NonUnicode());
            //     if (searchterm.CalculateSimilarity(project.Title.NonUnicode()) > 0.6
            //        || searchterm.CalculateSimilarity(project.Description.NonUnicode()) > 0.6)
            //     {
            //         similarProject.Add(project);
            //     }
            // }
            // return similarProject;
        }
    }
}
