using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using PMS.Persistence.IRepository;
using PMS.Persistence;
using PMS.Resources;
using PMS.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Controllers
{
    [Route("/api/tags")]
    public class TagController : Controller
    {
        private IMapper mapper;
        private ITagRepository repository;
        private IUnitOfWork unitOfWork;

        public TagController(IMapper mapper, IUnitOfWork unitOfWork, ITagRepository repository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateTag([FromBody]TagResource tagResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tag = mapper.Map<TagResource, Tag>(tagResource);

            repository.AddTag(tag);

            repository.UpdateTagProjects(tag, tagResource);

            await unitOfWork.Complete();

            tag = await repository.GetTag(tag.TagId);

            var result = mapper.Map<Tag, TagResource>(tag);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateTag(int id, [FromBody]TagResource tagResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tag = await repository.GetTag(id);

            if (tag == null)
                return NotFound();

            mapper.Map<TagResource, Tag>(tagResource, tag);

            repository.UpdateTagProjects(tag, tagResource);

            await unitOfWork.Complete();

            var result = mapper.Map<Tag, TagResource>(tag);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var tag = await repository.GetTag(id, includeRelated: false);

            if (tag == null)
            {
                return NotFound();
            }

            repository.RemoveTag(tag);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getTag/{id}")]
        public async Task<IActionResult> GetTag(int id)
        {
            var tag = await repository.GetTag(id);

            if (tag == null)
            {
                return NotFound();
            }

            var tagResource = mapper.Map<Tag, TagResource>(tag);

            return Ok(tagResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetTags()
        {
            var tags = await repository.GetTags();
            var tagResource = mapper.Map<IEnumerable<Tag>, IEnumerable<TagResource>>(tags);
            return Ok(tagResource);
        }
    }
}
