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
        private ITaskItemRepository taskItemRepository;
        private ITaskRepository taskRepository;

        public TaskItemController(IMapper mapper, IUnitOfWork unitOfWork, ITaskItemRepository taskItemRepository,
         ITaskRepository taskRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.taskItemRepository = taskItemRepository;
            this.taskRepository = taskRepository;
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

            taskItemRepository.AddTaskItem(taskItem);

            taskItem.Task = await taskRepository.GetTask(taskItemResource.TaskId);

            await unitOfWork.Complete();

            taskItem = await taskItemRepository.GetTaskItem(taskItem.TaskItemId);

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

            var taskItem = await taskItemRepository.GetTaskItem(id);

            if (taskItem == null)
                return NotFound();

            mapper.Map<TaskItemResource, TaskItem>(taskItemResource, taskItem);

            taskItem.Task = await taskRepository.GetTask(taskItemResource.TaskId);

            await unitOfWork.Complete();

            var result = mapper.Map<TaskItem, TaskItemResource>(taskItem);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            var taskItem = await taskItemRepository.GetTaskItem(id, includeRelated: false);

            if (taskItem == null)
            {
                return NotFound();
            }

            taskItemRepository.RemoveTaskItem(taskItem);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getTaskItem/{id}")]
        public async Task<IActionResult> GetTaskItem(int id)
        {
            var taskItem = await taskItemRepository.GetTaskItem(id);

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
            var taskItemes = await taskItemRepository.GetTaskItems();
            var taskItemResource = mapper.Map<IEnumerable<TaskItem>, IEnumerable<TaskItemResource>>(taskItemes);
            return Ok(taskItemResource);
        }

    }
}