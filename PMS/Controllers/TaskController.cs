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
    [Route("/api/tasks")]
    public class TaskController : Controller
    {
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        private ITaskRepository repository;

        public TaskController(IMapper mapper, IUnitOfWork unitOfWork, ITaskRepository repository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateTask([FromBody]TaskResource taskResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = mapper.Map<TaskResource, Models.TaskingModels.Task>(taskResource);

            repository.AddTask(task);
            await unitOfWork.Complete();

            task = await repository.GetTask(task.TaskId);

            var result = mapper.Map<Models.TaskingModels.Task, TaskResource>(task);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody]TaskResource taskResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = await repository.GetTask(id);

            if (task == null)
                return NotFound();

            mapper.Map<TaskResource, Models.TaskingModels.Task>(taskResource, task);
            await unitOfWork.Complete();

            var result = mapper.Map<Models.TaskingModels.Task, TaskResource>(task);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await repository.GetTask(id, includeRelated: false);

            if (task == null)
            {
                return NotFound();
            }

            repository.RemoveTask(task);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getTask/{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await repository.GetTask(id);

            if (task == null)
            {
                return NotFound();
            }

            var taskResource = mapper.Map<Models.TaskingModels.Task, TaskResource>(task);

            return Ok(taskResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetTaskes()
        {
            var taskes = await repository.GetTasks();
            var taskResource = mapper.Map<IEnumerable<Models.TaskingModels.Task>, IEnumerable<TaskResource>>(taskes);
            return Ok(taskResource);
        }
    }
}