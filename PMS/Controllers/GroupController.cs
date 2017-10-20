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
    [Route("/api/groups")]
    public class GroupController : Controller
    {
        private IMapper mapper;
        private IGroupRepository groupRepository;
        private ILecturerRepository lecturerRepository;
        private IProjectRepository projectRepository;
        private IUnitOfWork unitOfWork;

        public GroupController(IMapper mapper, IUnitOfWork unitOfWork, IGroupRepository groupRepository, ILecturerRepository lecturerRepository, IProjectRepository projectRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.groupRepository = groupRepository;
            this.lecturerRepository = lecturerRepository;
            this.projectRepository = projectRepository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateGroup([FromBody]GroupResource groupResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var group = mapper.Map<GroupResource, Group>(groupResource);
            group.Lecturer = await lecturerRepository.GetLecturer(groupResource.LecturerId);
            if (groupResource.ProjectId == null)
            {
                var otherProject = mapper.Map<ProjectResource, Project>(groupResource.OtherProject);
                group.Project = otherProject;
            }
            else
            {
                group.Project = await projectRepository.GetProject(groupResource.ProjectId.Value);
            }

            groupRepository.AddGroup(group);
            await unitOfWork.Complete();

            group = await groupRepository.GetGroup(group.GroupId);

            var result = mapper.Map<Group, GroupResource>(group);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateGroup(int id, [FromBody]GroupResource groupResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var group = await groupRepository.GetGroup(id);

            if (group == null)
                return NotFound();

            mapper.Map<GroupResource, Group>(groupResource, group);
            group.Lecturer = await lecturerRepository.GetLecturer(groupResource.LecturerId);
            if (groupResource.ProjectId == null)
            {
                var otherProject = mapper.Map<ProjectResource, Project>(groupResource.OtherProject);
                group.Project = otherProject;
            }
            else
            {
                group.Project = await projectRepository.GetProject(groupResource.ProjectId.Value);
            }
            await unitOfWork.Complete();

            var result = mapper.Map<Group, GroupResource>(group);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await groupRepository.GetGroup(id, includeRelated: false);

            if (group == null)
            {
                return NotFound();
            }

            groupRepository.RemoveGroup(group);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getgroup/{id}")]
        public async Task<IActionResult> GetGroup(int id)
        {
            var group = await groupRepository.GetGroup(id);

            if (group == null)
            {
                return NotFound();
            }

            var groupResource = mapper.Map<Group, GroupResource>(group);

            return Ok(groupResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetGroups()
        {
            var groups = await groupRepository.GetGroups();
            var groupResource = mapper.Map<IEnumerable<Group>, IEnumerable<GroupResource>>(groups);
            return Ok(groupResource);
        }
    }
}
