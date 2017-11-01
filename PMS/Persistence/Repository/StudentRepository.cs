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

        public void AddStudent(Student student)
        {
            context.Students.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            context.Remove(student);
        }

        public async Task<IEnumerable<Student>> GetStudents(Query queryObj)
        {
            var query = context.Students
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
                query = query.Where(q => q.Year == queryObj.Year);
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
            return await query.ToListAsync();
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
    }
}
