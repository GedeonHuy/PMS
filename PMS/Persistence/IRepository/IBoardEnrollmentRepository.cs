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
        Task<QueryResult<BoardEnrollment>> GetBoardEnrollmentsByLecturerEmail(Query queryObj, string email);
        Task<QueryResult<BoardEnrollment>> GetBoardEnrollmentsByGroupId(Query queryObj, int id);
        Task<QueryResult<BoardEnrollment>> GetBoardEnrollmentsByBoardId(Query queryObj, int id);
        void UpdateScore(BoardEnrollment boardEnrollment);
        void UpdateRecommendations(BoardEnrollment boardEnrollment, BoardEnrollmentResource boardEnrollmentResource);
        void UpdateGrades(BoardEnrollment boardEnrollment, BoardEnrollmentResource boardEnrollmentResource);
    }
}
