using PMS.Models;
using PMS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.IRepository
{
    public interface IRecommendationRepository
    {
        Task<Recommendation> GetRecommendation(int? id, bool includeRelated = true);
        void AddRecommendation(Recommendation recommendation);
        void RemoveRecommendation(Recommendation recommendation);
        Task<QueryResult<Recommendation>> GetRecommendations(Query queryObj);
        Task<QueryResult<Recommendation>> GetRecommendationsByBoardId(Query queryObj, int id);
        Task<QueryResult<Recommendation>> GetRecommendationsByGroupId(Query queryObj, int id);
    }
}
