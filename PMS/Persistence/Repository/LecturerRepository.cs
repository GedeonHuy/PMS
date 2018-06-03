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
                .Include(l => l.BoardEnrollments)
                .Include(p => p.Major)
                .Include(l => l.Projects)
                .SingleOrDefaultAsync(s => s.LecturerId == id);
        }

        public async Task<Lecturer> GetLecturerByEmail(string email, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Lecturers.FirstOrDefaultAsync(l => l.Email.Equals(email));
            }
            return await context.Lecturers
                .Include(l => l.Groups)
                .Include(l => l.BoardEnrollments)
                .Include(p => p.Major)
                                .Include(l => l.Projects)
                .SingleOrDefaultAsync(s => s.Email == email);
        }

        public void AddLecturer(Lecturer lecturer)
        {
            context.Lecturers.Add(lecturer);
        }

        public void RemoveLecturer(Lecturer lecturer)
        {
            lecturer.IsDeleted = true;
            //context.Remove(lecturer);
        }

        public async Task<IEnumerable<Enrollment>> FinishGroupingAsync(string email, int QuarterId)
        {
            var enrollments = await context.Enrollments
                                .Where(c => c.IsDeleted == false)
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
                .Where(c => c.IsDeleted == false)
                .Include(l => l.Groups)
                .Include(l => l.BoardEnrollments)
                .Include(p => p.Major)
                .Include(l => l.Projects)
                .AsQueryable();

            //filter
            if (queryObj.MajorId.HasValue)
            {
                query = query.Where(q => q.Major.MajorId == queryObj.MajorId.Value);
            }

            //search
            if (queryObj.NameSearch != null)
            {
                query = query.Where(q => q.Name.ToLower().NonUnicode().Contains(queryObj.NameSearch.ToLower().NonUnicode()));
            }

            if (queryObj.AddressSearch != null)
            {
                query = query.Where(q => q.Address.ToLower().NonUnicode().Contains(queryObj.AddressSearch.ToLower().NonUnicode()));
            }

            if (queryObj.EmailSearch != null)
            {
                query = query.Where(q => q.Email.ToLower().NonUnicode().Contains(queryObj.EmailSearch.ToLower().NonUnicode()));
            }

            if (queryObj.PhoneNumberSearch != null)
            {
                query = query.Where(q => q.PhoneNumber.ToLower().NonUnicode().Contains(queryObj.PhoneNumberSearch.ToLower().NonUnicode()));
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
                .Where(c => c.IsDeleted == false)
                .Include(p => p.Quarter)
                .Include(p => p.Group)
                .Include(p => p.Grade)
                .Include(p => p.Student)
                .Include(p => p.Lecturer)
                .Where(e => e.Lecturer.Email == email)
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
            var lecturer = await context.Lecturers.FirstOrDefaultAsync(s => s.Email == email);
            var query = context.Groups
                .Where(c => c.isDeleted == false)
                .Include(p => p.Lecturer)
                .Include(p => p.Project)
                .Include(p => p.Enrollments)
                    .ThenInclude(e => e.Student)
                .Include(p => p.UploadedFiles)
                .Include(p => p.Major)
                .Include(p => p.Quarter)
                .Where(g => g.Enrollments.Any(e => e.Lecturer == lecturer))
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

            //search
            if (queryObj.GroupNameSearch != null)
            {
                query = query.Where(q => q.GroupName.ToLower().NonUnicode().Contains(queryObj.GroupNameSearch.ToLower().NonUnicode()));
            }

            if (queryObj.LinkGitHubSearch != null)
            {
                query = query.Where(q => q.LinkGitHub.ToLower().NonUnicode().Contains(queryObj.LinkGitHubSearch.ToLower().NonUnicode()));
            }

            if (queryObj.ResultGradeSearch != null)
            {
                query = query.Where(q => q.ResultGrade.ToLower().NonUnicode().Contains(queryObj.ResultGradeSearch.ToLower().NonUnicode()));
            }

            if (queryObj.ResultScoreSearch != null)
            {
                query = query.Where(q => q.ResultScore.ToLower().NonUnicode().Contains(queryObj.ResultScoreSearch.ToLower().NonUnicode()));
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
        public async Task<QueryResult<Lecturer>> GetLecturersByMajor(int? majorId, Query queryObj)
        {
            var result = new QueryResult<Lecturer>();

            var query = context.Lecturers
                .Where(c => c.IsDeleted == false)
                .Include(l => l.Groups)
                .Include(l => l.BoardEnrollments)
                .Include(p => p.Major)
                .Include(l => l.Projects)
                .AsQueryable();

            //filter
            if (queryObj.MajorId.HasValue)
            {
                query = query.Where(q => q.Major.MajorId == queryObj.MajorId.Value)
                            .Where(q => q.Major.MajorId == majorId);
            }

            //search
            if (queryObj.NameSearch != null)
            {
                query = query.Where(q => q.Name.ToLower().NonUnicode().Contains(queryObj.NameSearch.ToLower().NonUnicode()));
            }

            if (queryObj.AddressSearch != null)
            {
                query = query.Where(q => q.Address.ToLower().NonUnicode().Contains(queryObj.AddressSearch.ToLower().NonUnicode()));
            }

            if (queryObj.EmailSearch != null)
            {
                query = query.Where(q => q.Email.ToLower().NonUnicode().Contains(queryObj.EmailSearch.ToLower().NonUnicode()));
            }

            if (queryObj.PhoneNumberSearch != null)
            {
                query = query.Where(q => q.PhoneNumber.ToLower().NonUnicode().Contains(queryObj.PhoneNumberSearch.ToLower().NonUnicode()));
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

        public void UpdateBoardEnrollments(Lecturer lecturer, LecturerResource lecturerResource)
        {
            if (lecturerResource.BoardEnrollments != null && lecturerResource.BoardEnrollments.Count >= 0)
            {
                //remove old boardEnrollments
                lecturer.BoardEnrollments.Clear();

                //add new enrollments
                var newBoardEnrollments = context.BoardEnrollments.Where(e => lecturerResource.BoardEnrollments.Any(id => id == e.BoardEnrollmentId)).ToList();
                foreach (var a in newBoardEnrollments)
                {
                    lecturer.BoardEnrollments.Add(a);
                }
            }
        }



        public void UpdateGroups(Lecturer lecturer, LecturerResource lecturerResource)
        {
            if (lecturerResource.Groups != null && lecturerResource.Groups.Count >= 0)
            {
                //remove old groups
                lecturer.Groups.Clear();

                //add new groups
                var newBoards = context.Groups.Where(e => lecturerResource.Groups.Any(id => id == e.GroupId)).ToList();
                foreach (var a in newBoards)
                {
                    lecturer.Groups.Add(a);
                }
            }
        }

        public void UpdateProjects(Lecturer lecturer, LecturerResource lecturerResource)
        {
            if (lecturerResource.Projects != null && lecturerResource.Projects.Count >= 0)
            {
                //remove old projects
                lecturer.Projects.Clear();

                //add new projects
                var newProjects = context.Projects.Where(e => lecturerResource.Projects.Any(id => id == e.ProjectId)).ToList();
                foreach (var a in newProjects)
                {
                    lecturer.Projects.Add(a);
                }
            }
        }
    }
}
