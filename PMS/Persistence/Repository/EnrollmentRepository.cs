using Microsoft.EntityFrameworkCore;
using PMS.Controllers;
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
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private ApplicationDbContext context;

        public EnrollmentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Enrollment> GetEnrollment(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Enrollments.FindAsync(id);
            }
            return await context.Enrollments
                .Include(p => p.Quarter)
                .Include(p => p.Group)
                .Include(p => p.Grade)
                .Include(p => p.Student)
                .Include(p => p.Lecturer)
                .SingleOrDefaultAsync(s => s.EnrollmentId == id);
        }
        public async Task<Enrollment> GetEnrollmentByEmail(string email)
        {
            return await context.Enrollments
                .Include(p => p.Quarter)
                .Include(p => p.Group)
                .Include(p => p.Grade)
                .Include(p => p.Student)
                .Include(p => p.Lecturer)
                .SingleOrDefaultAsync(s => s.Student.Email == email);
        }

        public void AddEnrollment(Enrollment enrollemnt)
        {
            context.Enrollments.Add(enrollemnt);
        }

        public void RemoveEnrollment(Enrollment enrollment)
        {
            context.Remove(enrollment);
        }

        public async Task<QueryResult<Enrollment>> GetEnrollments(Query queryObj)
        {
            var result = new QueryResult<Enrollment>();

            var query = context.Enrollments
                .Include(p => p.Quarter)
                .Include(p => p.Group)
                .Include(p => p.Grade)
                .Include(p => p.Student)
                .Include(p => p.Lecturer)
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

            if (queryObj.Email != null)
            {
                query = query.Where(q => q.Lecturer.Email == queryObj.Email || q.Lecturer == null);
            }

            if (queryObj.LecturerId.HasValue)
            {
                query = query.Where(q => q.Lecturer.LecturerId == queryObj.LecturerId.Value || q.Lecturer == null);
            }

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Enrollment, object>>>()
            {
                ["type"] = s => s.Type,
                ["grade"] = s => s.Grade,
                ["quarter"] = s => s.Quarter.QuarterId,
                ["email"] = s => s.Student.Email
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

        public bool CheckStudent(Student student, Group group)
        {
            if (student.Major.MajorId != group.Major.MajorId)
            {
                return false;
            }
            return true;
        }

    }
}
