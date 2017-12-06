using PMS.Models;
using PMS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.IRepository
{
    public interface ICouncilEnrollmentRepository
    {
        Task<CouncilEnrollment> GetCouncilEnrollment(int? id, bool includeRelated = true);
        void AddCouncilEnrollment(CouncilEnrollment CouncilEnrollment);
        void RemoveCouncilEnrollment(CouncilEnrollment CouncilEnrollment);
        Task<IEnumerable<CouncilEnrollment>> GetCouncilEnrollments();
        Task<CouncilEnrollment> GetCouncilEnrollmentByLecturerEmail(string email, CouncilResource councilResource);
        Task<IEnumerable<CouncilEnrollment>> GetCouncilEnrollmentsByCouncilId(int id);
    }
}
