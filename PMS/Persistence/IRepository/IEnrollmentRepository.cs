using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence
{
    public interface IEnrollmentRepository
    {
        Task<Enrollment> GetEnrollment(int? id, bool includeRelated = true);
        void AddEnrollment(Enrollment enrollment);
        void RemoveEnrollment(Enrollment enrollment);
        Task<IEnumerable<Enrollment>> GetEnrollments();
        bool CheckStudent(Student student, Group group);
    }
}
