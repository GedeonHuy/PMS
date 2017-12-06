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
    [Route("/api/quarters")]
    public class QuarterController : Controller
    {
        private IMapper mapper;
        private IQuarterRepository repository;
        private IUnitOfWork unitOfWork;

        public QuarterController(IMapper mapper, IUnitOfWork unitOfWork, IQuarterRepository repository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateQuarter([FromBody]QuarterResource QuarterResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Quarter = mapper.Map<QuarterResource, Quarter>(QuarterResource);

            repository.AddQuarter(Quarter);
            await unitOfWork.Complete();

            Quarter = await repository.GetQuarter(Quarter.QuarterId);

            var result = mapper.Map<Quarter, QuarterResource>(Quarter);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateQuarter(int id, [FromBody]QuarterResource QuarterResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Quarter = await repository.GetQuarter(id);

            if (Quarter == null)
                return NotFound();

            mapper.Map<QuarterResource, Quarter>(QuarterResource, Quarter);
            await unitOfWork.Complete();

            var result = mapper.Map<Quarter, QuarterResource>(Quarter);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteQuarter(int id)
        {
            var Quarter = await repository.GetQuarter(id, includeRelated: false);

            if (Quarter == null)
            {
                return NotFound();
            }

            repository.RemoveQuarter(Quarter);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getquarter/{id}")]
        public async Task<IActionResult> GetQuarter(int id)
        {
            var Quarter = await repository.GetQuarter(id);

            if (Quarter == null)
            {
                return NotFound();
            }

            var QuarterResource = mapper.Map<Quarter, QuarterResource>(Quarter);

            return Ok(QuarterResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<QueryResultResource<QuarterResource>> GetQuarters(QueryResource queryResource)
        {
            var query = mapper.Map<QueryResource, Query>(queryResource);

            var queryResult = await repository.GetQuarters(query);

            return mapper.Map<QueryResult<Quarter>, QueryResultResource<QuarterResource>>(queryResult);
        }

        [HttpGet]
        [Route("getcurrentquarter")]
        public async Task<IActionResult> GetCurrentQuarter()
        {
            var Quarter = await repository.GetCurrentQuarter();

            if (Quarter == null)
            {
                return NotFound();
            }

            var QuarterResource = mapper.Map<Quarter, QuarterResource>(Quarter);

            return Ok(QuarterResource);
        }
    }
}
