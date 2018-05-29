using PMS.Models;
using PMS.Resources;
using PMS.Resources.SubResources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.IRepository
{
    public interface IBoardRepository
    {
        Task<Board> GetBoard(int? id, bool includeRelated = true);
        void AddBoard(Board board);
        void RemoveBoard(Board board);
        Task<QueryResult<Board>> GetBoards(Query queryObj);
        Task AddLecturers(Board board, LecturerInformationResource lecturerInformations);
        void RemoveOldLecturer(Board board);
        string CheckLecturerInformations(LecturerInformationResource lecturerInformations);
        double CalculateScore(Board board);
        void CalculateGrade(Board board);
        void UpdateBoardEnrollments(Board board, BoardResource boardResource);
        Task UpdateOrder(Board board, BoardResource boardResource);
    }
}
