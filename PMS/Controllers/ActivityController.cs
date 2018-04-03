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
    [Route("/api/activities")]
    public class ActivityController : Controller
    {
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        private IActivityRepository repository;

        public ActivityController(IMapper mapper, IUnitOfWork unitOfWork, IActivityRepository repository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateActivity([FromBody]ActivityResource activityResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activity = mapper.Map<ActivityResource, Activity>(activityResource);

            repository.AddActivity(activity);
            await unitOfWork.Complete();

            activity = await repository.GetActivity(activity.ActivityId);

            var result = mapper.Map<Activity, ActivityResource>(activity);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateActivity(int id, [FromBody]ActivityResource activityResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activity = await repository.GetActivity(id);

            if (activity == null)
                return NotFound();

            mapper.Map<ActivityResource, Activity>(activityResource, activity);
            await unitOfWork.Complete();

            var result = mapper.Map<Activity, ActivityResource>(activity);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var activity = await repository.GetActivity(id, includeRelated: false);

            if (activity == null)
            {
                return NotFound();
            }

            repository.RemoveActivity(activity);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getActivity/{id}")]
        public async Task<IActionResult> GetActivity(int id)
        {
            var activity = await repository.GetActivity(id);

            if (activity == null)
            {
                return NotFound();
            }

            var activityResource = mapper.Map<Activity, ActivityResource>(activity);

            return Ok(activityResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetActivityes()
        {
            var activityes = await repository.GetActivities();
            var activityResource = mapper.Map<IEnumerable<Activity>, IEnumerable<ActivityResource>>(activityes);
            return Ok(activityResource);
        }
    }
}