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
                .Include(c => c.Recommendations)
                .Include(c => c.Grades)
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
                                .Include(c => c.Recommendations)
                                .Include(c => c.Grades)
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
                                .Include(c => c.Recommendations)
                                .Include(c => c.Grades)
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
                                .Include(c => c.Recommendations)
                                .Include(c => c.Grades)
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

            result.TotalItems = await query.CountAsync();

            //paging

            result.Items = await query.ToListAsync();

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
                                    .Include(c => c.Recommendations)
                                    .Include(c => c.Grades)
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

            result.TotalItems = await query.CountAsync();

            //paging

            result.Items = await query.ToListAsync();

            return result;
        }

        public void UpdateRecommendations(BoardEnrollment boardEnrollment, BoardEnrollmentResource boardEnrollmentResource)
        {
            if (boardEnrollmentResource.Recommendations != null && boardEnrollmentResource.Recommendations.Count >= 0)
            {
                //remove old tagprojects
                var oldRecommendations = boardEnrollment.Recommendations.Where(p => !boardEnrollmentResource.Recommendations.Any(id => id == p.Description)).ToList();
                foreach (Recommendation recommendation in oldRecommendations)
                {
                    recommendation.IsDeleted = true;
                    boardEnrollment.Recommendations.Remove(recommendation);
                }
                //project.TagProjects.Clear();

                //add new tagprojects
                var newRecommendations = boardEnrollmentResource.Recommendations.Where(t => !boardEnrollment.Recommendations.Any(id => id.Description == t));
                foreach (var recommendation in newRecommendations)
                {
                    boardEnrollment.Recommendations.Add(new Recommendation { IsDeleted = false, IsDone = false, Description = recommendation });
                    //project.TagProjects.Add(a);
                }
            }
        }

        public void UpdateGrades(BoardEnrollment boardEnrollment, BoardEnrollmentResource boardEnrollmentResource)
        {
            var totalScore =0.0;
            foreach(var gradeInformation in boardEnrollmentResource.GradeInformation){
                var grade = context.Grades
                            .Include(g => g.BoardEnrollment)
                            .FirstOrDefault(g => g.BoardEnrollment.BoardEnrollmentId ==boardEnrollment.BoardEnrollmentId && 
                            g.GradeDescription.Equals(gradeInformation.GradeDescription));
                if(grade != null){
                    grade.Score = gradeInformation.Score;
                    grade.Comment = gradeInformation.Comment;
                    grade.BoardEnrollment = boardEnrollment;
                }
                totalScore += gradeInformation.Score.Value;
            }
            boardEnrollment.Score =totalScore;
        }

    }
}
