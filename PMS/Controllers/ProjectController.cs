using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PMS.Persistence;
using AutoMapper;
using PMS.Resources;
using PMS.Models;
using PMS.Persistence.IRepository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Controllers
{
    [Route("/api/projects")]
    public class ProjectController : Controller
    {
        private IMapper mapper;
        private IProjectRepository projectRepository;
        private IMajorRepository majorRepository;
        private IUnitOfWork unitOfWork;

        public ProjectController(IMapper mapper, IUnitOfWork unitOfWork,
            IProjectRepository projectRepository, IMajorRepository majorRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.projectRepository = projectRepository;
            this.majorRepository = majorRepository;
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

            var major = await majorRepository.GetMajor(projectResource.MajorId);
            project.Major = major;

            projectRepository.AddProject(project);
            await unitOfWork.Complete();

            project = await projectRepository.GetProject(project.ProjectId);

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

            var project = await projectRepository.GetProject(id);

            if (project == null)
                return NotFound();

            mapper.Map<ProjectResource, Project>(projectResource, project);

            var major = await majorRepository.GetMajor(projectResource.MajorId);
            project.Major = major;

            await unitOfWork.Complete();

            var result = mapper.Map<Project, ProjectResource>(project);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await projectRepository.GetProject(id, includeRelated: false);

            if (project == null)
            {
                return NotFound();
            }

            projectRepository.RemoveProject(project);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getproject/{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await projectRepository.GetProject(id);

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
            var projects = await projectRepository.GetProjects();
            var projectResource = mapper.Map<IEnumerable<Project>, IEnumerable<ProjectResource>>(projects);
            return Ok(projectResource);
        }
    }
}
