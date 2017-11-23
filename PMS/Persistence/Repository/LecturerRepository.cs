using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Extensions;
using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PMS.Persistence
{
    public class LecturerRepository : ILecturerRepository
    {
        private ApplicationDbContext context;

        public LecturerRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Lecturer> GetLecturer(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Lecturers.FindAsync(id);
            }
            return await context.Lecturers
                .Include(l => l.Groups)
                .Include(l => l.CouncilEnrollments)
                .Include(p => p.Major)
                .SingleOrDefaultAsync(s => s.LecturerId == id);
        }

        public void AddLecturer(Lecturer lecturer)
        {
            context.Lecturers.Add(lecturer);
        }

        public void RemoveLecturer(Lecturer lecturer)
        {
            context.Remove(lecturer);
        }

        public async Task<IEnumerable<Enrollment>> FinishGroupingAsync(string email, int QuarterId)
        {
            var enrollments = await context.Enrollments
                                .Include(e => e.Grade)
                                .Include(e => e.Group)
                                .Include(e => e.Lecturer)
                                .Include(e => e.Student)
                                .Include(e => e.Quarter)
                                .Where(e => e.Lecturer.Email == email && e.Quarter.QuarterId == QuarterId && e.Group == null)
                                .ToListAsync();
            foreach (var enrollment in enrollments)
            {
                enrollment.Lecturer = null;
            }
            return enrollments;
        }

        public async Task<QueryResult<Lecturer>> GetLecturers(Query queryObj)
        {
            var result = new QueryResult<Lecturer>();

            var query = context.Lecturers
                .Include(l => l.Groups)
                .Include(l => l.CouncilEnrollments)
                .Include(p => p.Major)
                .AsQueryable();

            //filter
            if (queryObj.MajorId.HasValue)
            {
                query = query.Where(q => q.Major.MajorId == queryObj.MajorId.Value);
            }

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Lecturer, object>>>()
            {
                ["name"] = s => s.Name,
                ["major"] = s => s.Major.MajorName,
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.LecturerId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }

        public async Task<QueryResult<Enrollment>> GetEnrollments(Query queryObj, string email)
        {
            var result = new QueryResult<Enrollment>();

            var query = context.Enrollments
                .Include(p => p.Quarter)
                .Include(p => p.Group)
                .Include(p => p.Grade)
                .Include(p => p.Student)
                .Include(p => p.Lecturer)
                .Where(e => e.Student.Email == email)
                .AsQueryable();

            //filter
            if (queryObj.isConfirm != null)
            {
                query = query.Where(q => q.isConfirm == queryObj.isConfirm);
            }

            if (queryObj.QuarterId.HasValue)
            {
                query = query.Where(q => q.Quarter.QuarterId == queryObj.QuarterId.Value);
            }

            if (queryObj.Type != null)
            {
                query = query.Where(q => q.Type == queryObj.Type);
            }

            if (queryObj.isDeniedByLecturer == true)
            {
                query = query.Where(q => q.Lecturer == null);
            }

            if (queryObj.LecturerId.HasValue)
            {
                query = query.Where(q => q.Lecturer.LecturerId == queryObj.LecturerId.Value);
            }

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Enrollment, object>>>()
            {
                ["type"] = s => s.Type,
                ["grade"] = s => s.Grade,
                ["quarter"] = s => s.Quarter.QuarterId
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.EnrollmentId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }

        public async Task<QueryResult<Group>> GetGroups(Query queryObj, string email)
        {
            var result = new QueryResult<Group>();
            var student = await context.Students.FirstOrDefaultAsync(s => s.Email == email);
            var query = context.Groups
                .Include(p => p.Lecturer)
                .Include(p => p.Project)
                .Include(p => p.Enrollments)
                    .ThenInclude(e => e.Student)
                .Include(p => p.UploadedFiles)
                .Include(p => p.Major)
                .Include(p => p.Quarter)
                .Where(g => g.Enrollments.Any(e => e.Student == student))
                .AsQueryable();

            //filter
            if (queryObj.LecturerId.HasValue)
            {
                query = query.Where(q => q.Lecturer.LecturerId == queryObj.LecturerId.Value);
            }

            if (queryObj.ProjectId.HasValue)
            {
                query = query.Where(q => q.Project.ProjectId == queryObj.ProjectId.Value);
            }

            if (queryObj.isConfirm != null)
            {
                query = query.Where(q => q.isConfirm == queryObj.isConfirm);
            }

            if (queryObj.QuarterId.HasValue)
            {
                query = query.Where(q => q.Quarter.QuarterId == queryObj.QuarterId.Value);
            }

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Group, object>>>()
            {
                ["name"] = s => s.GroupName,
                ["quarter"] = s => s.Quarter.QuarterName,
                ["lecturer"] = s => s.Lecturer.Name,
                ["project"] = s => s.Project.Title
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.GroupId);
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
