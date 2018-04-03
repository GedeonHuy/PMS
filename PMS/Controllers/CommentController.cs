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
    [Route("/api/comments")]
    public class CommentController : Controller
    {
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        private ICommentRepository commentRepository;
        private ITaskRepository taskRepository;
        private IUserRepository userRepository;

        public CommentController(IMapper mapper, IUnitOfWork unitOfWork, ICommentRepository commentRepository,
         ITaskRepository taskRepository, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.commentRepository = commentRepository;
            this.taskRepository = taskRepository;
            this.userRepository = userRepository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateComment([FromBody]CommentResource commentResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = mapper.Map<CommentResource, Comment>(commentResource);

            comment.Task = await taskRepository.GetTask(commentResource.TaskId);
            comment.User = userRepository.GetUserByEmail(commentResource.Email);

            commentRepository.AddComment(comment);
            await unitOfWork.Complete();

            comment = await commentRepository.GetComment(comment.CommentId);

            var result = mapper.Map<Comment, CommentResource>(comment);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody]CommentResource commentResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await commentRepository.GetComment(id);

            if (comment == null)
                return NotFound();

            mapper.Map<CommentResource, Comment>(commentResource, comment);

            comment.Task = await taskRepository.GetTask(commentResource.TaskId);
            comment.User = userRepository.GetUserByEmail(commentResource.Email);

            await unitOfWork.Complete();

            var result = mapper.Map<Comment, CommentResource>(comment);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await commentRepository.GetComment(id, includeRelated: false);

            if (comment == null)
            {
                return NotFound();
            }

            commentRepository.RemoveComment(comment);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getComment/{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await commentRepository.GetComment(id);

            if (comment == null)
            {
                return NotFound();
            }

            var commentResource = mapper.Map<Comment, CommentResource>(comment);

            return Ok(commentResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetCommentes()
        {
            var commentes = await commentRepository.GetComments();
            var commentResource = mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(commentes);
            return Ok(commentResource);
        }
    }
}