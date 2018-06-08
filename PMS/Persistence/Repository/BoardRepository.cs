using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Extensions;
using PMS.Models;
using PMS.Persistence.IRepository;
using PMS.Resources;
using PMS.Resources.SubResources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PMS.Persistence.Repository
{
    public class BoardRepository : IBoardRepository
    {
        private ApplicationDbContext context;

        public BoardRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Board> GetBoard(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Boards.FindAsync(id);
            }
            return await context.Boards
                .Include(c => c.BoardEnrollments)
                    .ThenInclude(l => l.Lecturer)
                 .Include(c => c.BoardEnrollments)
                    .ThenInclude(l => l.BoardRole)
                .Include(c => c.Group)
                    .ThenInclude(p => p.Project)
                .Include(c => c.Group)
                    .ThenInclude(p => p.Enrollments)
                        .ThenInclude(e => e.Student)
                .Include(c => c.Group)
                    .ThenInclude(p => p.Lecturer)
                .SingleOrDefaultAsync(s => s.BoardId == id);
        }

        public void AddBoard(Board board)
        {
            context.Boards.Add(board);
        }

        public void RemoveBoard(Board board)
        {
            board.IsDeleted = true;
            //context.Remove(Board);
        }

        public async Task<QueryResult<Board>> GetBoards(Query queryObj)
        {
            var result = new QueryResult<Board>();

            var query = context.Boards
                         .Where(c => c.IsDeleted == false)
                .Include(c => c.BoardEnrollments)
                    .ThenInclude(l => l.Lecturer)
                 .Include(c => c.BoardEnrollments)
                    .ThenInclude(l => l.BoardRole)
                .Include(c => c.Group)
                    .ThenInclude(p => p.Project)
                .Include(c => c.Group)
                    .ThenInclude(p => p.Enrollments)
                        .ThenInclude(e => e.Student)
                .Include(c => c.Group)
                    .ThenInclude(p => p.Lecturer)
                          .AsQueryable();

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Board, object>>>()
            {
                ["grade"] = s => s.ResultGrade,
                ["code"] = s => s.ResultScore,
                ["group"] = s => s.Group.GroupName
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.BoardId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;

        }

        public async Task AddLecturers(Board board, LecturerInformationResource lecturerInformations)
        {
            var presidentBoardEnrollment = new BoardEnrollment
            {
                Board = board,
                IsDeleted = false,
                isMarked = false,
                Percentage = lecturerInformations.Chair.ScorePercent,
                BoardRole = await context.BoardRoles.FirstOrDefaultAsync(c => c.BoardRoleName == "Chair"),
                Lecturer = await context.Lecturers.FindAsync(lecturerInformations.Chair.LecturerId)

            };

            var secretaryBoardEnrollment = new BoardEnrollment
            {
                Board = board,
                IsDeleted = false,
                isMarked = false,
                Percentage = lecturerInformations.Secretary.ScorePercent,
                BoardRole = await context.BoardRoles.FirstOrDefaultAsync(c => c.BoardRoleName == "Secretary"),
                Lecturer = await context.Lecturers.FindAsync(lecturerInformations.Secretary.LecturerId)
            };

            var reviewerBoardEnrollment = new BoardEnrollment
            {
                Board = board,
                IsDeleted = false,
                isMarked = false,
                Percentage = lecturerInformations.Reviewer.ScorePercent,
                BoardRole = await context.BoardRoles.FirstOrDefaultAsync(c => c.BoardRoleName == "Reviewer"),
                Lecturer = await context.Lecturers.FindAsync(lecturerInformations.Reviewer.LecturerId)
            };

            var supervisorBoardEnrollment = new BoardEnrollment
            {
                Board = board,
                IsDeleted = false,
                isMarked = false,
                Percentage = lecturerInformations.Supervisor.ScorePercent,
                BoardRole = await context.BoardRoles.FirstOrDefaultAsync(c => c.BoardRoleName == "Supervisor"),
                Lecturer = await context.Lecturers.FindAsync(lecturerInformations.Supervisor.LecturerId)
            };

            context.BoardEnrollments.Add(presidentBoardEnrollment);
            context.BoardEnrollments.Add(secretaryBoardEnrollment);
            context.BoardEnrollments.Add(reviewerBoardEnrollment);
            context.BoardEnrollments.Add(supervisorBoardEnrollment);
        }

        public void RemoveOldLecturer(Board board)
        {
            var boardEnrollments = board.BoardEnrollments.ToList();
            foreach (var BoardEnrollment in boardEnrollments)
            {
                context.Remove(BoardEnrollment);
            }
        }

        public string CheckLecturerInformations(LecturerInformationResource lecturerInformations)
        {
            var chair = lecturerInformations.Chair;
            if (chair.ScorePercent == null)
            {
                return "nullScorePercent";
            }

            var secretary = lecturerInformations.Secretary;
            if (secretary.ScorePercent == null)
            {
                return "nullScorePercent";
            }

            var reviewer = lecturerInformations.Reviewer;
            if (reviewer.ScorePercent == null)
            {
                return "nullScorePercent";
            }

            var supervisor = lecturerInformations.Supervisor;
            if (supervisor.ScorePercent == null)
            {
                return "nullScorePercent";
            }

            if (chair.ScorePercent + secretary.ScorePercent + reviewer.ScorePercent + supervisor.ScorePercent != 100)
            {
                return "sumScorePercentIsNot100";
            }

            return "correct";
        }

        public double CalculateScore(Board board)
        {
            double score = 0;
            foreach (var boardenrollment in board.BoardEnrollments)
            {
                score += boardenrollment.Score.Value * (boardenrollment.Percentage.Value / 100);
            }
            return score;
        }
        public void CalculateGrade(Board board)
        {
            var score = Double.Parse(board.ResultScore);
            if (score >= 90 && score <= 100)
            {
                //well done
                board.ResultGrade = "A";
            }
            else if (score >= 85)
            {
                //good
                board.ResultGrade = "A-";
            }
            else if (score >= 80)
            {
                //unlucky
                board.ResultGrade = "B+";
            }
            else if (score >= 75)
            {
                //keep fighting
                board.ResultGrade = "B";
            }
            else if (score >= 70)
            {
                //care
                board.ResultGrade = "B-";
            }
            else if (score >= 65)
            {
                //need more try
                board.ResultGrade = "C+";
            }
            else if (score >= 60)
            {
                //noob
                board.ResultGrade = "C";
            }
            else if (score >= 55)
            {
                //chicken
                board.ResultGrade = "C-";
            }
            else if (score >= 53)
            {
                //quit
                board.ResultGrade = "D+";
            }
            else if (score >= 52)
            {
                //no word
                board.ResultGrade = "D";
            }
            else if (score >= 50)
            {
                // lucky
                board.ResultGrade = "D-";
            }
            else
            {
                //poor you bro
                board.ResultGrade = "F";
            }
        }

        public async Task UpdateBoardEnrollments(Board board, BoardResource BoardResource)
        {
            if (BoardResource.BoardEnrollments != null && BoardResource.BoardEnrollments.Count >= 0)
            {
                // //remove old BoardEnrollments
                // board.BoardEnrollments.Clear();

                // //add new enrollments
                // var newBoardEnrollments = context.BoardEnrollments.Where(e => BoardResource.BoardEnrollments.Any(id => id == e.BoardEnrollmentId)).ToList();
                // foreach (var a in newBoardEnrollments)
                // {
                //     board.BoardEnrollments.Add(a);
                // }
                var updatedBoardEnrollments = board.BoardEnrollments.ToList();
                foreach (var boardEnrollment in updatedBoardEnrollments)
                {
                    if (boardEnrollment.BoardRole.BoardRoleName == "Chair")
                    {
                        if (boardEnrollment.Lecturer.LecturerId == BoardResource.LecturerInformations.Chair.LecturerId)
                        {
                            boardEnrollment.Percentage = BoardResource.LecturerInformations.Chair.ScorePercent;
                        }
                        else
                        {
                            boardEnrollment.IsDeleted = true;
                            board.BoardEnrollments.Remove(boardEnrollment);
                            var chairBoardEnrollment = new BoardEnrollment
                            {
                                Board = board,
                                IsDeleted = false,
                                isMarked = false,
                                Percentage = BoardResource.LecturerInformations.Chair.ScorePercent,
                                BoardRole = await context.BoardRoles.FirstOrDefaultAsync(c => c.BoardRoleName == "Chair"),
                                Lecturer = await context.Lecturers.FindAsync(BoardResource.LecturerInformations.Chair.LecturerId)

                            };
                            context.BoardEnrollments.Add(chairBoardEnrollment);
                        }
                    }

                    else if (boardEnrollment.BoardRole.BoardRoleName == "Secretary")
                    {
                        if (boardEnrollment.Lecturer.LecturerId == BoardResource.LecturerInformations.Secretary.LecturerId)
                        {
                            boardEnrollment.Percentage = BoardResource.LecturerInformations.Secretary.ScorePercent;
                        }
                        else
                        {
                            boardEnrollment.IsDeleted = true;
                            board.BoardEnrollments.Remove(boardEnrollment);
                            var secretaryBoardEnrollment = new BoardEnrollment
                            {
                                Board = board,
                                IsDeleted = false,
                                isMarked = false,
                                Percentage = BoardResource.LecturerInformations.Secretary.ScorePercent,
                                BoardRole = await context.BoardRoles.FirstOrDefaultAsync(c => c.BoardRoleName == "Secretary"),
                                Lecturer = await context.Lecturers.FindAsync(BoardResource.LecturerInformations.Secretary.LecturerId)

                            };
                            context.BoardEnrollments.Add(secretaryBoardEnrollment);
                        }
                    }

                    else if (boardEnrollment.BoardRole.BoardRoleName == "Supervisor")
                    {
                        if (boardEnrollment.Lecturer.LecturerId == BoardResource.LecturerInformations.Supervisor.LecturerId)
                        {
                            boardEnrollment.Percentage = BoardResource.LecturerInformations.Supervisor.ScorePercent;
                        }
                        else
                        {
                            boardEnrollment.IsDeleted = true;
                            board.BoardEnrollments.Remove(boardEnrollment);
                            var supervisorBoardEnrollment = new BoardEnrollment
                            {
                                Board = board,
                                IsDeleted = false,
                                isMarked = false,
                                Percentage = BoardResource.LecturerInformations.Supervisor.ScorePercent,
                                BoardRole = await context.BoardRoles.FirstOrDefaultAsync(c => c.BoardRoleName == "Supervisor"),
                                Lecturer = await context.Lecturers.FindAsync(BoardResource.LecturerInformations.Supervisor.LecturerId)

                            };
                            context.BoardEnrollments.Add(supervisorBoardEnrollment);
                        }
                    }

                    else if (boardEnrollment.BoardRole.BoardRoleName == "Reviewer")
                    {
                        if (boardEnrollment.Lecturer.LecturerId == BoardResource.LecturerInformations.Reviewer.LecturerId)
                        {
                            boardEnrollment.Percentage = BoardResource.LecturerInformations.Reviewer.ScorePercent;
                        }
                        else
                        {
                            boardEnrollment.IsDeleted = true;
                            board.BoardEnrollments.Remove(boardEnrollment);
                            var reviewerBoardEnrollment = new BoardEnrollment
                            {
                                Board = board,
                                IsDeleted = false,
                                isMarked = false,
                                Percentage = BoardResource.LecturerInformations.Reviewer.ScorePercent,
                                BoardRole = await context.BoardRoles.FirstOrDefaultAsync(c => c.BoardRoleName == "Reviewer"),
                                Lecturer = await context.Lecturers.FindAsync(BoardResource.LecturerInformations.Reviewer.LecturerId)

                            };
                            context.BoardEnrollments.Add(reviewerBoardEnrollment);
                        }
                    }
                }
            }
        }


        public async Task UpdateOrder(Board board, BoardResource boardResource)
        {
            var listBoardInDate = await context.Boards.Where(b => b.DateTime.Date == boardResource.DateTime.Date
                && b.DateTime.Month == boardResource.DateTime.Month
                && b.DateTime.Year == boardResource.DateTime.Year).ToListAsync();

            board.Order = listBoardInDate.Count + 1;
        }
    }
}
