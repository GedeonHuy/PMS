using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

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
        public async Task<Student> GetStudent(int id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Students.FindAsync(id);
            }
            return await context.Students
                .Include(s => s.Enrollments)
                .SingleOrDefaultAsync(s => s.Id == id);
        }

<<<<<<< HEAD
        public void AddStudent(Student student)
=======
        public async Task<Student> GetStudentByEmail(string email)
        {
            return await context.Students
                .Include(s => s.Enrollments)
                .SingleOrDefaultAsync(s => s.Email == email);
        }

        public async Task AddStudentAsync(Student student)
>>>>>>> 09a40a69d75b1bc3a95e6a379380fc0ef7e7bd0f
        {
            context.Students.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            context.Remove(student);
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await context.Students
                .Include(s => s.Enrollments)
                .ToListAsync();
        }
<<<<<<< HEAD
=======
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
>>>>>>> 09a40a69d75b1bc3a95e6a379380fc0ef7e7bd0f
    }
}
