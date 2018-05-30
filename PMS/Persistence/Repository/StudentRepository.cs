using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq.Expressions;
using PMS.Extensions;
using PMS.Resources;

namespace PMS.Persistence
{
    public class StudentRepository : IStudentRepository
    {
        private ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public StudentRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.context = context;
        }
        public async Task<Student> GetStudent(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Students.FindAsync(id);
            }
            return await context.Students
                .Include(s => s.Enrollments)
                .Include(p => p.Major)
                .SingleOrDefaultAsync(s => s.Id == id);
        }


        public async Task<Student> GetStudentByEmail(string email)
        {
            return await context.Students
                .Include(s => s.Enrollments)
                .Include(p => p.Major)
                .SingleOrDefaultAsync(s => s.Email == email);
        }


        public async Task<Student> GetStudentByStudentCode(string studentCode)
        {
            return await context.Students
                .Include(s => s.Enrollments)
                .Include(p => p.Major)
                .SingleOrDefaultAsync(s => s.StudentCode == studentCode);
        }

        public void AddStudent(Student student)
        {
            context.Students.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            student.IsDeleted = true;
            //context.Remove(student);
        }

        public async Task<QueryResult<Student>> GetStudents(Query queryObj)
        {
            var result = new QueryResult<Student>();

            var query = context.Students
                .Where(c => c.IsDeleted == false)
                .Include(p => p.Major)
                .Include(s => s.Enrollments)
                .AsQueryable();

            //filter
            if (queryObj.MajorId.HasValue)
            {
                query = query.Where(q => q.Major.MajorId == queryObj.MajorId.Value);
            }

            if (queryObj.Year != null)
            {
                query = query.Where(q => q.Year.Equals(queryObj.Year));
            }


            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Student, object>>>()
            {
                ["name"] = s => s.Name,
                ["code"] = s => s.StudentCode,
                ["year"] = s => s.Year
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.Id);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            result.TotalItems = await query.CountAsync();

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

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            result.TotalItems = await query.CountAsync();

            return result;
        }

        public async Task<QueryResult<Group>> GetGroups(Query queryObj, string email)
        {
            var result = new QueryResult<Group>();
            var student = await context.Students.FirstOrDefaultAsync(s => s.Email == email);
            var query = context.Groups
                .Where(c => c.isDeleted == false)
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

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            result.TotalItems = await query.CountAsync();

            return result;
        }

        public bool CheckStudentEnrollments(Student student, string Type)
        {
            foreach (var enrollment in student.Enrollments)
            {
                if (enrollment.Type.Equals(Type))
                {
                    return false;
                }
            }
            return true;
        }

        private bool RoleExists(string roleName)
        {
            return context.ApplicationRole.Any(r => r.Name == roleName);
        }

        private bool StudentIdExists(string studentCode)
        {
            return context.Students.Any(r => r.StudentCode == studentCode);
        }

        private bool StudentExists(string email)
        {
            return context.Students.Any(e => e.Email == email);
        }

        public void UpdateEnrollments(Student student, StudentResource studentResource)
        {
            if (studentResource.Enrollments != null && studentResource.Enrollments.Count >= 0)
            {
                //remove old enrollments
                student.Enrollments.Clear();

                //add new enrollments
                var newEnrollments = context.Enrollments.Where(e => studentResource.Enrollments.Any(id => id == e.EnrollmentId)).ToList();
                foreach (var e in newEnrollments)
                {
                    student.Enrollments.Add(e);
                    e.Student = student;
                }
            }
        }
    }
}
