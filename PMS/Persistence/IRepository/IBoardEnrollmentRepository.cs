using PMS.Models;
using PMS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.IRepository
{
    public interface IBoardEnrollmentRepository
    {
        Task<BoardEnrollment> GetBoardEnrollment(int? id, bool includeRelated = true);
        void AddBoardEnrollment(BoardEnrollment boardEnrollment);
        void RemoveBoardEnrollment(BoardEnrollment boardEnrollment);
        Task<QueryResult<BoardEnrollment>> GetBoardEnrollments(Query queryObj);
        Task<BoardEnrollment> GetBoardEnrollmentByLecturerEmail(string email, BoardResource boardResource);
        Task<QueryResult<BoardEnrollment>> GetBoardEnrollmentsByLecturerEmail(string email);
        Task<IEnumerable<BoardEnrollment>> GetBoardEnrollmentsByBoardId(int id);
        void UpdateScore(BoardEnrollment boardEnrollment);
    }
}
