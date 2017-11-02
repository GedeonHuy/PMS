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

        public async Task AddLecturers(Council council, ICollection<LecturerInformationResource> lecturerInformations)
        {
            foreach (var lecturerInformation in lecturerInformations)
            {
                var lecturer = await context.Lecturers.FirstOrDefaultAsync(l => l.LecturerId == lecturerInformation.LecturerId);
                if (lecturerInformation.ScorePercent == null)
                {
                    council.CouncilEnrollments.Add(new CouncilEnrollment
                    {
                        Council = council,
                        Lecturer = lecturer,
                        Percentage = (100.0 / lecturerInformations.Count),
                        IsDeleted = false
                    });
                }
                else
                {
                    council.CouncilEnrollments.Add(new CouncilEnrollment
                    {
                        Council = council,
                        Lecturer = lecturer,
                        Percentage = lecturerInformation.ScorePercent,
                        IsDeleted = false
                    });
                }

            }
        }
    }
}
