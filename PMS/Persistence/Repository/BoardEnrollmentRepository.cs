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

        public async Task<QueryResult<BoardEnrollment>> GetBoardEnrollmentsByLecturerEmail(Query queryObj, string email)
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

            //paging

            result.Items = await query.ToListAsync();

            result.TotalItems = result.Items.Count();

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

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            result.TotalItems = result.Items.Count();

            return result;

        }

        // public async Task<QueryResult<BoardEnrollment>> GetBoardEnrollmentByLecturerEmail(Query queryObj, string email)
        // {
        //     var result = new QueryResult<BoardEnrollment>();
        //     var query = context.BoardEnrollments
        //                             .Where(c => c.IsDeleted == false && c.Lecturer.Email == email)
        //                             .Include(c => c.Lecturer)
        //                             .Include(c => c.Board)
        //                         .AsQueryable();


        //     //filter
        //     if (queryObj.LecturerId.HasValue)
        //     {
        //         query = query.Where(q => q.Lecturer.LecturerId == queryObj.LecturerId.Value);
        //     }
        //     if (queryObj.Email != null)
        //     {
        //         query = query.Where(q => q.Lecturer.Email == queryObj.Email);
        //     }

        //     //sort

        //     query = query.OrderByDescending(s => s.BoardEnrollmentId);

        //     //paging

        //     result.Items = await query.ToListAsync();

        //     result.TotalItems = await query.CountAsync();

        //     return result;
        // }

        public async Task<QueryResult<BoardEnrollment>> GetBoardEnrollmentsByBoardId(Query queryObj, int id)
        {
            var result = new QueryResult<BoardEnrollment>();
            var query = context.BoardEnrollments
                                .Where(c => c.IsDeleted == false)
                                .Include(c => c.Board)
                                .Include(c => c.Lecturer)
                                .Include(c => c.BoardRole)
                                .Where(c => c.Board.BoardId == id)
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

            query = query.OrderByDescending(s => s.BoardEnrollmentId);

            //paging

            result.Items = await query.ToListAsync();

            result.TotalItems = result.Items.Count();

            return result;
        }

        public void UpdateScore(BoardEnrollment boardEnrollment)
        {
            context.Update(boardEnrollment);
        }

        public async Task<QueryResult<BoardEnrollment>> GetBoardEnrollmentsByGroupId(Query queryObj, int id)
        {
            var result = new QueryResult<BoardEnrollment>();
            var query = context.BoardEnrollments
                                    .Include(c => c.Lecturer)
                                    .Include(c => c.Board)
                                        .ThenInclude(b => b.Group)
                                    .Where(c => c.IsDeleted == false && c.Board.Group.GroupId == id)
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

            query = query.OrderByDescending(s => s.BoardEnrollmentId);

            //paging

            result.Items = await query.ToListAsync();

            result.TotalItems = result.Items.Count();

            return result;
        }

    }
}
