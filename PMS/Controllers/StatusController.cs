using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PMS.Models.TaskingModels;
using PMS.Persistence;
using PMS.Persistence.IRepository;
using PMS.Resources.TaskResources;

namespace PMS.Controllers
{
    [Route("/api/statuses")]
    public class StatusController : Controller
    {
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        private IStatusRepository repository;

        public StatusController(IMapper mapper, IUnitOfWork unitOfWork, IStatusRepository repository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateStatus([FromBody]StatusResource statusResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var status = mapper.Map<StatusResource, Status>(statusResource);

            repository.AddStatus(status);
            await unitOfWork.Complete();

            status = await repository.GetStatus(status.StatusId);

            var result = mapper.Map<Status, StatusResource>(status);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody]StatusResource statusResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var status = await repository.GetStatus(id);

            if (status == null)
                return NotFound();

            mapper.Map<StatusResource, Status>(statusResource, status);
            await unitOfWork.Complete();

            var result = mapper.Map<Status, StatusResource>(status);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var status = await repository.GetStatus(id, includeRelated: false);

            if (status == null)
            {
                return NotFound();
            }

            repository.RemoveStatus(status);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getstatus/{id}")]
        public async Task<IActionResult> GetStatus(int id)
        {
            var status = await repository.GetStatus(id);

            if (status == null)
            {
                return NotFound();
            }

            var statusResource = mapper.Map<Status, StatusResource>(status);

            return Ok(statusResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetStatuses()
        {
            var statuses = await repository.GetStatuses();
            var statusResource = mapper.Map<IEnumerable<Status>, IEnumerable<StatusResource>>(statuses);
            return Ok(statusResource);
        }
    }
}