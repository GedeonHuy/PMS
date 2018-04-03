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
        private ICommentRepository repository;

        public CommentController(IMapper mapper, IUnitOfWork unitOfWork, ICommentRepository repository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
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

            repository.AddComment(comment);
            await unitOfWork.Complete();

            comment = await repository.GetComment(comment.CommentId);

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

            var comment = await repository.GetComment(id);

            if (comment == null)
                return NotFound();

            mapper.Map<CommentResource, Comment>(commentResource, comment);
            await unitOfWork.Complete();

            var result = mapper.Map<Comment, CommentResource>(comment);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await repository.GetComment(id, includeRelated: false);

            if (comment == null)
            {
                return NotFound();
            }

            repository.RemoveComment(comment);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getComment/{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await repository.GetComment(id);

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
            var commentes = await repository.GetComments();
            var commentResource = mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(commentes);
            return Ok(commentResource);
        }
    }
}