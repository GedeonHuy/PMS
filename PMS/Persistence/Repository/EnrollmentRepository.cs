using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private ApplicationDbContext context;

        public EnrollmentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Enrollment> GetEnrollment(int id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Enrollments.FindAsync(id);
            }
            return await context.Enrollments
                .Include(e => e.Student)
                .SingleOrDefaultAsync(s => s.EnrollmentId == id);
        }

        public void AddEnrollment(Enrollment enrollemnt)
        {
            context.Enrollments.Add(enrollemnt);
        }

        public void RemoveEnrollment(Enrollment enrollment)
        {
            context.Remove(enrollment);
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollments()
        {
            return await context.Enrollments
                .ToListAsync();
        }
    }
}
