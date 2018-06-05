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
    public class RecommendationRepository : IRecommendationRepository
    {
        private ApplicationDbContext context;

        public RecommendationRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Recommendation> GetRecommendation(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Recommendations.FindAsync(id);
            }
            return await context.Recommendations
                .Include(s => s.BoardEnrollment)
                .SingleOrDefaultAsync(s => s.RecommendationId == id);
        }

        public void AddRecommendation(Recommendation recommendation)
        {
            context.Recommendations.Add(recommendation);
        }

        public void RemoveRecommendation(Recommendation recommendation)
        {
            recommendation.IsDeleted = true;
            //context.Remove(Quarter);
        }

        public async Task<QueryResult<Recommendation>> GetRecommendations(Query queryObj)
        {
            var result = new QueryResult<Recommendation>();

            var query = context.Recommendations
                .Where(c => c.IsDeleted == false)
                .Include(s => s.BoardEnrollment)
                .AsQueryable();

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Recommendation, object>>>()
            {
                ["description"] = s => s.Description,
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.RecommendationId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }

        public async Task<QueryResult<Recommendation>> GetRecommendationsByBoardId(Query queryObj, int id)
        {
            var result = new QueryResult<Recommendation>();

            var query = context.Recommendations
                        .Include(s => s.BoardEnrollment)
                            .ThenInclude(b => b.Board)
                .Where(c => c.IsDeleted == false && c.BoardEnrollment.Board.BoardId == id)
                .AsQueryable();

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Recommendation, object>>>()
            {
                ["description"] = s => s.Description,
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.RecommendationId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }

        public async Task<QueryResult<Recommendation>> GetRecommendationsByGroupId(Query queryObj, int id)
        {
            var result = new QueryResult<Recommendation>();

            var query = context.Recommendations
                        .Include(s => s.BoardEnrollment)
                            .ThenInclude(be => be.Board)
                                .ThenInclude(b => b.Group)
                .Where(c => c.IsDeleted == false && c.BoardEnrollment.Board.Group.GroupId == id)
                .AsQueryable();

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Recommendation, object>>>()
            {
                ["description"] = s => s.Description,
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.RecommendationId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }
    }
}
