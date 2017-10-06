using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence
{
    public class StudentRepository : IStudentRepository
    {
        private ApplicationDbContext context;

        public StudentRepository(ApplicationDbContext context)
        {
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
                .SingleOrDefaultAsync(s => s.StudentId == id);
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
