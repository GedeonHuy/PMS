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
    [Route("/api/tagprojects")]
    public class TagProjectController : Controller
    {
        private IMapper mapper;
        private ITagProjectRepository tagProjectrepository;
        private IProjectRepository projectRepository;
        private ITagRepository tagRepository;
        private IUnitOfWork unitOfWork;

        public TagProjectController(IMapper mapper, IUnitOfWork unitOfWork,
         ITagProjectRepository tagProjectrepository, IProjectRepository projectRepository,
         ITagRepository tagRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.tagProjectrepository = tagProjectrepository;
            this.projectRepository = projectRepository;
            this.tagRepository = tagRepository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateTagProject([FromBody]TagProjectResource tagProjectResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tagProject = mapper.Map<TagProjectResource, TagProject>(tagProjectResource);

            tagProjectrepository.AddTagProject(tagProject);

            tagProject.Tag = await tagRepository.GetTag(tagProjectResource.TagId);
            tagProject.Project = await projectRepository.GetProject(tagProjectResource.ProjectId);

            await unitOfWork.Complete();

            tagProject = await tagProjectrepository.GetTagProject(tagProject.TagProjectId);

            var result = mapper.Map<TagProject, TagProjectResource>(tagProject);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateTagProject(int id, [FromBody]TagProjectResource tagProjectResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tagProject = await tagProjectrepository.GetTagProject(id);

            if (tagProject == null)
                return NotFound();

            mapper.Map<TagProjectResource, TagProject>(tagProjectResource, tagProject);

            tagProject.Tag = await tagRepository.GetTag(tagProjectResource.TagId);
            tagProject.Project = await projectRepository.GetProject(tagProjectResource.ProjectId);

            await unitOfWork.Complete();

            var result = mapper.Map<TagProject, TagProjectResource>(tagProject);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteTagProject(int id)
        {
            var tagProject = await tagProjectrepository.GetTagProject(id, includeRelated: false);

            if (tagProject == null)
            {
                return NotFound();
            }

            tagProjectrepository.RemoveTagProject(tagProject);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getTagProject/{id}")]
        public async Task<IActionResult> GetTagProject(int id)
        {
            var tagProject = await tagProjectrepository.GetTagProject(id);

            if (tagProject == null)
            {
                return NotFound();
            }

            var tagProjectResource = mapper.Map<TagProject, TagProjectResource>(tagProject);

            return Ok(tagProjectResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetTagProjects()
        {
            var tagProjects = await tagProjectrepository.GetTagProjects();
            var tagProjectResource = mapper.Map<IEnumerable<TagProject>, IEnumerable<TagProjectResource>>(tagProjects);
            return Ok(tagProjectResource);
        }
    }
}
