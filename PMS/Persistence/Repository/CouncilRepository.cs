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
                    .ThenInclude(p => p.Project)
                .Include(c => c.Group)
                    .ThenInclude(p => p.Lecturer)    
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
                            .ThenInclude(p => p.Project)
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
                isMarked = false,
                Percentage = lecturerInformations.President.ScorePercent,
                CouncilRole = await context.CouncilRoles.FirstOrDefaultAsync(c => c.CouncilRoleName == "President"),
                Lecturer = await context.Lecturers.FindAsync(lecturerInformations.President.LecturerId)

            };

            var secretaryCouncilEnrollment = new CouncilEnrollment
            {
                Council = council,
                IsDeleted = false,
                isMarked = false,
                Percentage = lecturerInformations.Secretary.ScorePercent,
                CouncilRole = await context.CouncilRoles.FirstOrDefaultAsync(c => c.CouncilRoleName == "Secretary"),
                Lecturer = await context.Lecturers.FindAsync(lecturerInformations.Secretary.LecturerId)
            };

            var reviewerCouncilEnrollment = new CouncilEnrollment
            {
                Council = council,
                IsDeleted = false,
                isMarked = false,
                Percentage = lecturerInformations.Reviewer.ScorePercent,
                CouncilRole = await context.CouncilRoles.FirstOrDefaultAsync(c => c.CouncilRoleName == "Reviewer"),
                Lecturer = await context.Lecturers.FindAsync(lecturerInformations.Reviewer.LecturerId)
            };

            var supervisorCouncilEnrollment = new CouncilEnrollment
            {
                Council = council,
                IsDeleted = false,
                isMarked = false,
                Percentage = lecturerInformations.Supervisor.ScorePercent,
                CouncilRole = await context.CouncilRoles.FirstOrDefaultAsync(c => c.CouncilRoleName == "Supervisor"),
                Lecturer = council.Group.Lecturer
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

        public double CalculateScore(Council council)
        {
            double score = 0;
            foreach (var councilenrollment in council.CouncilEnrollments)
            {
                score += councilenrollment.Score.Value * (councilenrollment.Percentage.Value / 100);
            }
            return score;
        }
        public void CalculateGrade(Council council)
        {
            var score = Double.Parse(council.ResultScore);
            if (score >= 90 && score <= 100)
            {
                //well done
                council.ResultGrade = "A";
            }
            else if (score >= 85)
            {
                //good
                council.ResultGrade = "A-";
            }
            else if (score >= 80)
            {
                //unlucky
                council.ResultGrade = "B+";
            }
            else if (score >= 75)
            {
                //keep fighting
                council.ResultGrade = "B";
            }
            else if (score >= 70)
            {
                //care
                council.ResultGrade = "B-";
            }
            else if (score >= 65)
            {
                //need more try
                council.ResultGrade = "C+";
            }
            else if (score >= 60)
            {
                //noob
                council.ResultGrade = "C";
            }
            else if (score >= 55)
            {
                //chicken
                council.ResultGrade = "C-";
            }
            else if (score >= 53)
            {
                //quit
                council.ResultGrade = "D+";
            }
            else if (score >= 52)
            {
                //no word
                council.ResultGrade = "D";
            }
            else if (score >= 50)
            {
                // lucky
                council.ResultGrade = "D-";
            }
            else
            {
                //poor you bro
                council.ResultGrade = "F";
            }
        }

        public CouncilResource FillLecturersInformation(CouncilResource councilResource)
        {
            throw new NotImplementedException();
        }

        public CouncilResource FillLecturersInformation(CouncilResource councilResource)
        {
            throw new NotImplementedException();
        }
    }
}
