using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PMS.Persistence;
using AutoMapper;
using PMS.Resources;
using PMS.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Controllers
{
    [Route("/api/projects")]
    public class ProjectController : Controller
    {
        private IMapper mapper;
        private IProjectRepository repository;
        private IUnitOfWork unitOfWork;

        public ProjectController(IMapper mapper, IUnitOfWork unitOfWork, IProjectRepository repository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateProject([FromBody]ProjectResource projectResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = mapper.Map<ProjectResource, Project>(projectResource);

            repository.AddProject(project);
            await unitOfWork.Complete();

            project = await repository.GetProject(project.ProjectId);

            var result = mapper.Map<Project, ProjectResource>(project);

            return Ok(result);
        }

        [HttpPut] /*/api/enrollments/id*/
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody]ProjectResource projectResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = await repository.GetProject(id);

            if (project == null)
                return NotFound();

            mapper.Map<ProjectResource, Project>(projectResource, project);
            await unitOfWork.Complete();

            var result = mapper.Map<Project, ProjectResource>(project);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await repository.GetProject(id, includeRelated: false);

            if (project == null)
            {
                return NotFound();
            }

            repository.RemoveProject(project);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getproject/{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await repository.GetProject(id);

            if (project == null)
            {
                return NotFound();
            }

            var projectResource = mapper.Map<Project, ProjectResource>(project);

            return Ok(projectResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await repository.GetProjects();
            return Ok(projects);
        }
    }
}
