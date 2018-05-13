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
    public class GroupRepository : IGroupRepository
    {
        private ApplicationDbContext context;

        public GroupRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Group> GetGroup(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Groups.FindAsync(id);
            }
            return await context.Groups
                .Include(p => p.Project)
                .Include(p => p.Enrollments)
                    .ThenInclude(e => e.Student)
                .Include(p => p.UploadedFiles)
                .Include(p => p.Lecturer)
                .Include(p => p.Major)
                .Include(p => p.Quarter)
                .Include(p => p.Board)
                    .ThenInclude(be => be.BoardEnrollments)
                .SingleOrDefaultAsync(s => s.GroupId == id);
        }

        public void AddGroup(Group group)
        {
            context.Groups.Add(group);
        }

        public void RemoveGroup(Group group)
        {
            group.isDeleted = true;
            //context.Update(group);
        }

        public async Task<QueryResult<Group>> GetGroups(Query queryObj)
        {
            var result = new QueryResult<Group>();

            var query = context.Groups.Where(g => g.isDeleted == false)
                .Include(p => p.Project)
                .Include(p => p.Enrollments)
                    .ThenInclude(e => e.Student)
                .Include(p => p.UploadedFiles)
                .Include(p => p.Lecturer)
                .Include(p => p.Major)
                .Include(p => p.Quarter)
                .Include(p => p.Board)
                    .ThenInclude(be => be.BoardEnrollments)
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

            if (queryObj.Email != null)
            {
                query = query.Where(q => q.Lecturer.Email == queryObj.Email || q.Lecturer == null);
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

        public bool CheckEnrollment(Group group, Enrollment enrollment)
        {
            if (group.Enrollments.Any(e => e.Student.Id == enrollment.Student.Id))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> CheckGroup(GroupResource group)
        {
            var lecturer = await context.Lecturers
                .Include(l => l.Major)
                .FirstOrDefaultAsync(l => l.LecturerId == group.LecturerId);
            var project = await context.Projects
                .Include(p => p.Major)
                .FirstOrDefaultAsync(p => p.ProjectId == group.ProjectId);
            var major = await context.Majors
                .FirstOrDefaultAsync(m => m.MajorId == group.MajorId);
            if ((major.MajorId != lecturer.Major.MajorId) || (major.MajorId != project.Major.MajorId)
                || (lecturer.Major.MajorId != project.Major.MajorId))
            {
                return false;
            }
            return true;
        }

        public void UpdateEnrollments(Group group, GroupResource groupResource)
        {
            if (groupResource.Enrollments != null && groupResource.Enrollments.Count >= 0)
            {
                //remove old Enrollments
                var oldEnrollments = group.Enrollments
                                    .Where(e => !groupResource.StudentEmails.Any(email => email == e.Student.Email)).ToList();
                foreach (var enrollment in oldEnrollments)
                {
                    enrollment.IsDeleted = true;
                    enrollment.Group = null;
                }
                //group.Enrollments.Clear();

                //add new enrollments

                //var newEnrollments = context.Enrollments.Where(e => groupResource.Enrollments.Any(id => id == e.EnrollmentId)).ToList();
                //foreach (var a in newEnrollments)
                //{
                //    group.Enrollments.Add(a);
                //}
                var newStudentEmails = groupResource.StudentEmails.Where(s => !group.Enrollments.Any(e => e.Student.Email == s)).ToList();
                foreach (var Email in newStudentEmails)
                {
                    var enrollment = new Enrollment
                    {
                        StartDate = group.Quarter.QuarterStart,
                        EndDate = group.Quarter.QuarterEnd,
                        isConfirm = "Accepted",
                        IsDeleted = false,
                        Lecturer = group.Lecturer,
                        Quarter = group.Quarter,
                        Student = context.Students.FirstOrDefault(s => s.Email == Email),
                        Type = group.Project.Type,
                        Grade = null,
                    };
                    group.Enrollments.Add(enrollment);
                }
            }
        }

        public void UpdateUploadedFiles(Group group, GroupResource groupResource)
        {
            if (groupResource.UploadedFiles != null && groupResource.UploadedFiles.Count >= 0)
            {
                //remove old boardEnrollments
                group.UploadedFiles.Clear();

                //add new enrollments
                var newUploadFiles = context.UploadedFiles.Where(e => groupResource.UploadedFiles.Any(id => id == e.UploadedFileId)).ToList();
                foreach (var a in newUploadFiles)
                {
                    group.UploadedFiles.Add(a);
                }
            }
        }
    }
}
