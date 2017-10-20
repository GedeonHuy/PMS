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
        private ICouncilRepository councilRepository;
        private IGroupRepository groupRepository;
        private IUnitOfWork unitOfWork;

        public CouncilController(IMapper mapper, IUnitOfWork unitOfWork, ICouncilRepository councilRepository, IGroupRepository groupRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.councilRepository = councilRepository;
            this.groupRepository = groupRepository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateCouncil([FromBody]CouncilResource councilResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //case: missing set point
            var nullPercentCount = councilResource.LecturerInformations.Count(l => l.ScorePercent == null);
            if (nullPercentCount != councilResource.LecturerInformations.Count)
            {
                ModelState.AddModelError("Error", "If you set percentage of score, u must set for all lecturer");
                return BadRequest(ModelState);
            }

            ////case: one percent of score is equal 0
            var zeroPercentCount = councilResource.LecturerInformations.Count(l => l.ScorePercent == 0);
            if (zeroPercentCount > 0)
            {
                ModelState.AddModelError("Error", "One or more lecturer's percentage of score is 0");
                return BadRequest(ModelState);
            }

            //case: the total sum of score is not 100
            var PercentSum = councilResource.LecturerInformations.Sum(l => l.ScorePercent);
            if (PercentSum != 100.0 && PercentSum != 0)
            {
                ModelState.AddModelError("Error", "If total percentage of score is not equal 100%");
                return BadRequest(ModelState);
            }

            var council = mapper.Map<CouncilResource, Council>(councilResource);
            var group = await groupRepository.GetGroup(councilResource.GroupId);
            council.Group = group;

            councilRepository.AddCouncil(council);
            await unitOfWork.Complete();

            council = await councilRepository.GetCouncil(council.CouncilId);

            await councilRepository.AddLecturers(council, councilResource.LecturerInformations);
            await unitOfWork.Complete();

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

            //case: missing set point
            var nullPercentCount = councilResource.LecturerInformations.Count(l => l.ScorePercent == null);
            if (nullPercentCount != councilResource.LecturerInformations.Count)
            {
                ModelState.AddModelError("Error", "If you set percentage of score, u must set for all lecturer");
                return BadRequest(ModelState);
            }

            //case: one percent of score is equal 0
            var zeroPercentCount = councilResource.LecturerInformations.Count(l => l.ScorePercent == 0);
            if (zeroPercentCount > 0)
            {
                ModelState.AddModelError("Error", "One or more lecturer's percentage of score is 0");
                return BadRequest(ModelState);
            }

            //case: the total sum of score is not 100
            var PercentSum = councilResource.LecturerInformations.Sum(l => l.ScorePercent);
            if (PercentSum != 100.0 && PercentSum != 0)
            {
                ModelState.AddModelError("Error", "If total percentage of score is not equal 100%");
                return BadRequest(ModelState);
            }

            var council = await councilRepository.GetCouncil(id);

            if (council == null)
                return NotFound();

            mapper.Map<CouncilResource, Council>(councilResource, council);
            await unitOfWork.Complete();

            var group = await groupRepository.GetGroup(councilResource.GroupId);
            council.Group = group;
            await unitOfWork.Complete();

            await councilRepository.AddLecturers(council, councilResource.LecturerInformations);
            await unitOfWork.Complete();

            var result = mapper.Map<Council, CouncilResource>(council);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteCouncil(int id)
        {
            var council = await councilRepository.GetCouncil(id, includeRelated: false);

            if (council == null)
            {
                return NotFound();
            }

            councilRepository.RemoveCouncil(council);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getcouncilenrollment/{id}")]
        public async Task<IActionResult> GetCouncil(int id)
        {
            var council = await councilRepository.GetCouncil(id);

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
            var councils = await councilRepository.GetCouncils();
            var councilResource = mapper.Map<IEnumerable<Council>, IEnumerable<CouncilResource>>(councils);
            return Ok(councilResource);
        }
    }
}
