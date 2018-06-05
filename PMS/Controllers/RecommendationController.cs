using System;
using PMS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using PMS.Persistence.IRepository;
using PMS.Persistence;
using PMS.Resources;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Controllers
{
    [Route("/api/recommendations")]
    public class RecommendationController : Controller
    {
        private IMapper mapper;
        private IRecommendationRepository recommendationRepository;
        private IBoardEnrollmentRepository boardEnrollmentRepository;
        private IUnitOfWork unitOfWork;

        public RecommendationController(IMapper mapper, IUnitOfWork unitOfWork,
         IRecommendationRepository recommendationRepository, IBoardEnrollmentRepository boardEnrollmentRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.recommendationRepository = recommendationRepository;
            this.boardEnrollmentRepository = boardEnrollmentRepository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateRecommendation([FromBody]RecommendationResource recommendationResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recommendation = mapper.Map<RecommendationResource, Recommendation>(recommendationResource);

            recommendationRepository.AddRecommendation(recommendation);

            recommendation.BoardEnrollment = await boardEnrollmentRepository.GetBoardEnrollment(recommendationResource.BoardEnrollmentId);
            await unitOfWork.Complete();

            recommendation = await recommendationRepository.GetRecommendation(recommendation.RecommendationId);

            var result = mapper.Map<Recommendation, RecommendationResource>(recommendation);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateRecommendation(int id, [FromBody]RecommendationResource recommendationResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recommendation = await recommendationRepository.GetRecommendation(id);

            if (recommendation == null)
                return NotFound();

            mapper.Map<RecommendationResource, Recommendation>(recommendationResource, recommendation);

            recommendation.BoardEnrollment = await boardEnrollmentRepository.GetBoardEnrollment(recommendationResource.BoardEnrollmentId);
            await unitOfWork.Complete();

            var result = mapper.Map<Recommendation, RecommendationResource>(recommendation);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteRecommendation(int id)
        {
            var recommendation = await recommendationRepository.GetRecommendation(id, includeRelated: false);

            if (recommendation == null)
            {
                return NotFound();
            }

            recommendationRepository.RemoveRecommendation(recommendation);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getrecommendation/{id}")]
        public async Task<IActionResult> GetRecommendation(int id)
        {
            var recommendation = await recommendationRepository.GetRecommendation(id);

            if (recommendation == null)
            {
                return NotFound();
            }

            var recommendationResource = mapper.Map<Recommendation, RecommendationResource>(recommendation);

            return Ok(recommendationResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<QueryResultResource<RecommendationResource>> GetRecommendations(QueryResource queryResource)
        {
            var query = mapper.Map<QueryResource, Query>(queryResource);

            var queryResult = await recommendationRepository.GetRecommendations(query);

            return mapper.Map<QueryResult<Recommendation>, QueryResultResource<RecommendationResource>>(queryResult);
        }

        [HttpGet]
        [Route("getrecommendationsbyboardid/{id}")]
        public async Task<QueryResultResource<RecommendationResource>> GetRecommendationsByBoardId(QueryResource queryResource, int id)
        {
            var query = mapper.Map<QueryResource, Query>(queryResource);

            var queryResult = await recommendationRepository.GetRecommendationsByBoardId(query, id);

            return mapper.Map<QueryResult<Recommendation>, QueryResultResource<RecommendationResource>>(queryResult);
        }

        [HttpGet]
        [Route("getrecommendationsbygroupid/{id}")]
        public async Task<QueryResultResource<RecommendationResource>> GetRecommendationsByGroupId(QueryResource queryResource, int id)
        {
            var query = mapper.Map<QueryResource, Query>(queryResource);

            var queryResult = await recommendationRepository.GetRecommendationsByGroupId(query, id);

            return mapper.Map<QueryResult<Recommendation>, QueryResultResource<RecommendationResource>>(queryResult);
        }
    }
}
