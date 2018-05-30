using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Extensions;
using PMS.Models;
using PMS.Persistence.IRepository;
using PMS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PMS.Persistence.Repository
{
    public class MajorRepository : IMajorRepository
    {
        private ApplicationDbContext context;

        public MajorRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Major> GetMajor(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Majors.FindAsync(id);
            }
            return await context.Majors
                .Include(m => m.Groups)
                .Include(m => m.Lecturers)
                .Include(m => m.Students)
                .Include(m => m.Projects)
                .SingleOrDefaultAsync(g => g.MajorId == id);
        }

        public void AddMajor(Major major)
        {
            context.Majors.Add(major);
        }

        public void RemoveMajor(Major major)
        {
            major.isDeleted = true;
            //context.Remove(major);
        }

        public async Task<QueryResult<Major>> GetMajors(Query queryObj)
        {
            var result = new QueryResult<Major>();

            var query = context.Majors
                .Where(c => c.isDeleted == false)
                .Include(m => m.Groups)
                .Include(m => m.Lecturers)
                .Include(m => m.Students)
                .Include(m => m.Projects)
                .AsQueryable();

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Major, object>>>()
            {
                ["name"] = s => s.MajorName,
                ["code"] = s => s.MajorCode
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.MajorId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            result.TotalItems = await query.CountAsync();

            return result;
        }

        public void UpdateLecturers(Major major, MajorResource majorResource)
        {
            if (majorResource.Lecturers != null && majorResource.Lecturers.Count >= 0)
            {
                //remove old lecturers
                major.Lecturers.Clear();

                //add new lecturers
                var newLecturers = context.Lecturers.Where(e => majorResource.Lecturers.Any(id => id == e.LecturerId)).ToList();
                foreach (var a in newLecturers)
                {
                    major.Lecturers.Add(a);
                }
            }
        }

        public void UpdateProjects(Major major, MajorResource majorResource)
        {
            if (majorResource.Projects != null && majorResource.Projects.Count >= 0)
            {
                //remove old projects
                major.Projects.Clear();

                //add new projects
                var newProjects = context.Projects.Where(e => majorResource.Projects.Any(id => id == e.ProjectId)).ToList();
                foreach (var a in newProjects)
                {
                    major.Projects.Add(a);
                }
            }
        }

        public void UpdateGroups(Major major, MajorResource majorResource)
        {
            if (majorResource.Groups != null && majorResource.Groups.Count >= 0)
            {
                //remove old groups
                major.Groups.Clear();

                //add new groups
                var newGroups = context.Groups.Where(e => majorResource.Groups.Any(id => id == e.GroupId)).ToList();
                foreach (var a in newGroups)
                {
                    major.Groups.Add(a);
                }
            }
        }

        public void UpdateStudents(Major major, MajorResource majorResource)
        {
            if (majorResource.Students != null && majorResource.Students.Count >= 0)
            {
                //remove old students
                major.Students.Clear();

                //add new students
                var newStudents = context.Students.Where(e => majorResource.Students.Any(id => id == e.Id)).ToList();
                foreach (var a in newStudents)
                {
                    major.Students.Add(a);
                }
            }
        }
    }
}
