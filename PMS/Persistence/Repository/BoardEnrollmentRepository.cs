using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Extensions;
using PMS.Models;
using PMS.Persistence.IRepository;
using PMS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PMS.Persistence.Repository
{
    public class BoardEnrollmentRepository : IBoardEnrollmentRepository
    {
        private ApplicationDbContext context;

        public BoardEnrollmentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<BoardEnrollment> GetBoardEnrollment(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.BoardEnrollments.FindAsync(id);
            }
            return await context.BoardEnrollments
                .Include(c => c.Lecturer)
                .Include(c => c.Board)
                .SingleOrDefaultAsync(s => s.BoardEnrollmentId == id);
        }

        public void AddBoardEnrollment(BoardEnrollment boardEnrollment)
        {
            context.BoardEnrollments.Add(boardEnrollment);
        }

        public void RemoveBoardEnrollment(BoardEnrollment boardEnrollment)
        {
            boardEnrollment.IsDeleted = true;
            //context.Remove(boardEnrollment);
        }

        public async Task<QueryResult<BoardEnrollment>> GetBoardEnrollmentsByLecturerEmail(string email)
        {
            var result = new QueryResult<BoardEnrollment>();
            var query = context.BoardEnrollments
                                .Where(c => c.IsDeleted == false)
                                .Include(c => c.Lecturer)
                                .Include(c => c.Board)
                                .AsQueryable();

            //filter

            query = query.Where(q => q.Lecturer.Email.Equals(email));

            //sort

            query = query.OrderByDescending(s => s.BoardEnrollmentId);



            result.TotalItems = await query.CountAsync();

            //paging

            result.Items = await query.ToListAsync();

            return result;

        }

        public async Task<QueryResult<BoardEnrollment>> GetBoardEnrollments(Query queryObj)
        {
            var result = new QueryResult<BoardEnrollment>();
            var query = context.BoardEnrollments
                                .Where(c => c.IsDeleted == false)
                                .Include(c => c.Lecturer)
                                .Include(c => c.Board)
                                .AsQueryable();

            //filter
            if (queryObj.LecturerId.HasValue)
            {
                query = query.Where(q => q.Lecturer.LecturerId == queryObj.LecturerId.Value);
            }
            if (queryObj.Email != null)
            {
                query = query.Where(q => q.Lecturer.Email == queryObj.Email);
            }

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<BoardEnrollment, object>>>()
            {
                ["lecturer"] = s => s.Lecturer.Name,
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.BoardEnrollmentId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;

        }

        public async Task<BoardEnrollment> GetBoardEnrollmentByLecturerEmail(string email, BoardResource boardResource)
        {
            var boardEnrollment = await context.BoardEnrollments
                                    .Where(c => c.IsDeleted == false)
                                    .Include(c => c.Lecturer)
                                    .Include(c => c.Board)
                                    .SingleOrDefaultAsync(c => c.Board.BoardId == boardResource.BoardId
                                    && c.Lecturer.Email == email);

            return boardEnrollment;
        }

        public async Task<IEnumerable<BoardEnrollment>> GetBoardEnrollmentsByBoardId(int id)
        {
            return await context.BoardEnrollments
                                .Where(c => c.IsDeleted == false)
                                .Include(c => c.Board)
                                .Include(c => c.Lecturer)
                                .Include(c => c.BoardRole)
                                .Where(c => c.Board.BoardId == id)
                                .ToListAsync();
        }

        public void UpdateScore(BoardEnrollment boardEnrollment)
        {
            context.Update(boardEnrollment);
        }
    }
}
