using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using PMS.Persistence;
using PMS.Resources;
using PMS.Models;
using PMS.Persistence.IRepository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Controllers
{
    [Route("/api/councils")]
    public class CouncilController : Controller
    {
        private IMapper mapper;
        private ICouncilRepository repository;
        private IUnitOfWork unitOfWork;

        public CouncilController(IMapper mapper, IUnitOfWork unitOfWork, ICouncilRepository repository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateCouncil([FromBody]CouncilResource councilResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var council = mapper.Map<CouncilResource, Council>(councilResource);

            repository.AddCouncil(council);
            await unitOfWork.Complete();

            council = await repository.GetCouncil(council.CouncilId);

            var result = mapper.Map<Council, CouncilResource>(council);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateCouncil(int id, [FromBody]CouncilResource councilResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var council = await repository.GetCouncil(id);

            if (council == null)
                return NotFound();

            mapper.Map<CouncilResource, Council>(councilResource, council);
            await unitOfWork.Complete();

            var result = mapper.Map<Council, CouncilResource>(council);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteCouncil(int id)
        {
            var council = await repository.GetCouncil(id, includeRelated: false);

            if (council == null)
            {
                return NotFound();
            }

            repository.RemoveCouncil(council);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getcouncil/{id}")]
        public async Task<IActionResult> GetCouncil(int id)
        {
            var council = await repository.GetCouncil(id);

            if (council == null)
            {
                return NotFound();
            }

            var councilResource = mapper.Map<Council, CouncilResource>(council);

            return Ok(councilResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetCouncils()
        {
            var councils = await repository.GetCouncils();
            return Ok(councils);
        }
    }
}
