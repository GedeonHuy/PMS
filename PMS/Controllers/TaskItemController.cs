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
    [Route("/api/taskitems")]
    public class TaskItemController : Controller
    {
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        private ITaskItemRepository repository;

        public TaskItemController(IMapper mapper, IUnitOfWork unitOfWork, ITaskItemRepository repository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateTaskItem([FromBody]TaskItemResource taskItemResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var taskItem = mapper.Map<TaskItemResource, TaskItem>(taskItemResource);

            repository.AddTaskItem(taskItem);
            await unitOfWork.Complete();

            taskItem = await repository.GetTaskItem(taskItem.TaskItemId);

            var result = mapper.Map<TaskItem, TaskItemResource>(taskItem);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateTaskItem(int id, [FromBody]TaskItemResource taskItemResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var taskItem = await repository.GetTaskItem(id);

            if (taskItem == null)
                return NotFound();

            mapper.Map<TaskItemResource, TaskItem>(taskItemResource, taskItem);
            await unitOfWork.Complete();

            var result = mapper.Map<TaskItem, TaskItemResource>(taskItem);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            var taskItem = await repository.GetTaskItem(id, includeRelated: false);

            if (taskItem == null)
            {
                return NotFound();
            }

            repository.RemoveTaskItem(taskItem);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getTaskItem/{id}")]
        public async Task<IActionResult> GetTaskItem(int id)
        {
            var taskItem = await repository.GetTaskItem(id);

            if (taskItem == null)
            {
                return NotFound();
            }

            var taskItemResource = mapper.Map<TaskItem, TaskItemResource>(taskItem);

            return Ok(taskItemResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetTaskItems()
        {
            var taskItemes = await repository.GetTaskItems();
            var taskItemResource = mapper.Map<IEnumerable<TaskItem>, IEnumerable<TaskItemResource>>(taskItemes);
            return Ok(taskItemResource);
        }
    }
}