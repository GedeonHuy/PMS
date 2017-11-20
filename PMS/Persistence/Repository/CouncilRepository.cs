using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Extensions;
using PMS.Models;
using PMS.Persistence.IRepository;
using PMS.Resources.SubResources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PMS.Persistence.Repository
{
    public class CouncilRepository : ICouncilRepository
    {
        private ApplicationDbContext context;

        public CouncilRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Council> GetCouncil(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Councils.FindAsync(id);
            }
            return await context.Councils
                .Include(c => c.CouncilEnrollments)
                    .ThenInclude(l => l.Lecturer)
                 .Include(c => c.CouncilEnrollments)
                    .ThenInclude(l => l.CouncilRole)
                .Include(c => c.Group)
                .SingleOrDefaultAsync(s => s.CouncilId == id);
        }

        public void AddCouncil(Council council)
        {
            context.Councils.Add(council);
        }

        public void RemoveCouncil(Council council)
        {
            context.Remove(council);
        }

        public async Task<QueryResult<Council>> GetCouncils(Query queryObj)
        {
            var result = new QueryResult<Council>();

            var query = context.Councils
                         .Include(c => c.CouncilEnrollments)
                            .ThenInclude(l => l.Lecturer)
                         .Include(c => c.CouncilEnrollments)
                            .ThenInclude(l => l.CouncilRole)
                         .Include(c => c.Group)
                          .AsQueryable();

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Council, object>>>()
            {
                ["grade"] = s => s.ResultGrade,
                ["code"] = s => s.ResultScore,
                ["group"] = s => s.Group.GroupName
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.CouncilId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;

        }

        public async Task AddLecturers(Council council, LecturerInformationResource lecturerInformations)
        {
            var presidentCouncilEnrollment = new CouncilEnrollment
            {
                Council = council,
                IsDeleted = false,
                isMark = false,
                Percentage = lecturerInformations.President.ScorePercent,
                CouncilRole = await context.CouncilRoles.FirstOrDefaultAsync(c => c.CouncilRoleName == "President"),
                Lecturer = await context.Lecturers.FindAsync(lecturerInformations.President.LecturerId)

            };

            var secretaryCouncilEnrollment = new CouncilEnrollment
            {
                Council = council,
                IsDeleted = false,
                isMark = false,
                Percentage = lecturerInformations.Secretary.ScorePercent,
                CouncilRole = await context.CouncilRoles.FirstOrDefaultAsync(c => c.CouncilRoleName == "Serectory"),
                Lecturer = await context.Lecturers.FindAsync(lecturerInformations.Secretary.LecturerId)
            };

            var reviewerCouncilEnrollment = new CouncilEnrollment
            {
                Council = council,
                IsDeleted = false,
                isMark = false,
                Percentage = lecturerInformations.Reviewer.ScorePercent,
                CouncilRole = await context.CouncilRoles.FirstOrDefaultAsync(c => c.CouncilRoleName == "Reviewer"),
                Lecturer = await context.Lecturers.FindAsync(lecturerInformations.Reviewer.LecturerId)
            };

            var supervisorCouncilEnrollment = new CouncilEnrollment
            {
                Council = council,
                IsDeleted = false,
                isMark = false,
                Percentage = lecturerInformations.Supervisor.ScorePercent,
                CouncilRole = await context.CouncilRoles.FirstOrDefaultAsync(c => c.CouncilRoleName == "Supervisor"),
                Lecturer = await context.Lecturers.FindAsync(lecturerInformations.Supervisor.LecturerId)
            };

            context.CouncilEnrollments.Add(presidentCouncilEnrollment);
            context.CouncilEnrollments.Add(secretaryCouncilEnrollment);
            context.CouncilEnrollments.Add(reviewerCouncilEnrollment);
            context.CouncilEnrollments.Add(supervisorCouncilEnrollment);
        }

        public void RemoveOldLecturer(Council council)
        {
            var councilEnrollments = council.CouncilEnrollments.ToList();
            foreach (var councilEnrollment in councilEnrollments)
            {
                context.Remove(councilEnrollment);
            }
        }

        public string CheckLecturerInformations(LecturerInformationResource lecturerInformations)
        {
            var president = lecturerInformations.President;
            if (president.ScorePercent == null || president.ScorePercent == 0)
            {
                return "nullOrZeroScorePercent";
            }

            var secretary = lecturerInformations.Secretary;
            if (secretary.ScorePercent == null || secretary.ScorePercent == 0)
            {
                return "nullOrZeroScorePercent";
            }

            var reviewer = lecturerInformations.Reviewer;
            if (reviewer.ScorePercent == null || reviewer.ScorePercent == 0)
            {
                return "nullOrZeroScorePercent";
            }

            var supervisor = lecturerInformations.Supervisor;
            if (supervisor.ScorePercent == null || supervisor.ScorePercent == 0)
            {
                return "nullOrZeroScorePercent";
            }

            if (president.ScorePercent + secretary.ScorePercent + reviewer.ScorePercent + supervisor.ScorePercent != 100)
            {
                return "sumScorePercentIsNot100";
            }

            return "correct";
        }
    }
}
