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

        public void AddStudent(Student student)
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
    }
}
