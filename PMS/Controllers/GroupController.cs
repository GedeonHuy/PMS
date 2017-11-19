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
    [Route("/api/groups")]
    public class GroupController : Controller
    {
        private IMapper mapper;
        private IGroupRepository groupRepository;
        private ILecturerRepository lecturerRepository;
        private IProjectRepository projectRepository;
        private IMajorRepository majorRepository;
        private IQuarterRepository quarterRepository;
        private IEnrollmentRepository enrollmentRepository;
        private IUnitOfWork unitOfWork;

        public GroupController(IMapper mapper, IUnitOfWork unitOfWork,
            IGroupRepository groupRepository, ILecturerRepository lecturerRepository,
            IProjectRepository projectRepository, IMajorRepository majorRepository,
            IQuarterRepository quarterRepository, IEnrollmentRepository enrollmentRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.groupRepository = groupRepository;
            this.lecturerRepository = lecturerRepository;
            this.projectRepository = projectRepository;
            this.majorRepository = majorRepository;
            this.quarterRepository = quarterRepository;
            this.enrollmentRepository = enrollmentRepository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateGroup([FromBody]GroupResource groupResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //case: lecturer, project and major is not the same
            // var checkGroup = await groupRepository.CheckGroup(groupResource);
            // if (!checkGroup)
            // {
            //     ModelState.AddModelError("Error", "Stop hacking. Please set the Lecturer, project and group in the same Major.");
            //     return BadRequest(ModelState);
            // }

            var group = mapper.Map<GroupResource, Group>(groupResource);

            group.Lecturer = await lecturerRepository.GetLecturer(groupResource.LecturerId);

            if (groupResource.ProjectId == null)
            {
                var otherProject = mapper.Map<ProjectResource, Project>(groupResource.OtherProject);
                group.Project = otherProject;
            }
            else
            {
                group.Project = await projectRepository.GetProject(groupResource.ProjectId);
            }

            var major = await majorRepository.GetMajor(groupResource.MajorId);
            group.Major = major;

            var quarter = await quarterRepository.GetQuarter(groupResource.QuarterId);
            group.Quarter = quarter;

            groupRepository.AddGroup(group);
            await unitOfWork.Complete();

            group = await groupRepository.GetGroup(group.GroupId);
            group.Enrollments.Clear();

            foreach (var enrollmentResource in groupResource.Enrollments)
            {
                var enrollment = await enrollmentRepository.GetEnrollment(enrollmentResource.EnrollmentId);
                //case: enrollment's type and project's type is different and the student has been already in group

                if (group != null && group.Project.Type != enrollment.Type)
                {
                    ModelState.AddModelError("Error", "Enrollment's type and Project Type of Group are not the same.");
                    groupRepository.RemoveGroup(group);
                    await unitOfWork.Complete();
                    return BadRequest(ModelState);
                }
                else if (group != null && !groupRepository.CheckEnrollment(group, enrollment))
                {
                    ModelState.AddModelError("Warning", "This group already has this student.");
                    groupRepository.RemoveGroup(group);
                    await unitOfWork.Complete();
                    return BadRequest(ModelState);
                }
                else
                {
                    enrollment.Group = group;
                    group.Enrollments.Add(enrollment);
                    await unitOfWork.Complete();
                }

            }

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
            group.Lecturer = await lecturerRepository.GetLecturer(groupResource.LecturerId.Value);
            if (groupResource.ProjectId == null)
            {
                var otherProject = mapper.Map<ProjectResource, Project>(groupResource.OtherProject);
                group.Project = otherProject;
            }
            else
            {
                group.Project = await projectRepository.GetProject(groupResource.ProjectId.Value);
            }

            var major = await majorRepository.GetMajor(groupResource.MajorId);
            group.Major = major;

            var quarter = await quarterRepository.GetQuarter(groupResource.QuarterId);
            group.Quarter = quarter;
            await unitOfWork.Complete();

            group.Enrollments.Clear();

            foreach (var enrollmentResource in groupResource.Enrollments)
            {
                var enrollment = await enrollmentRepository.GetEnrollment(enrollmentResource.EnrollmentId);
                //case: enrollment's type and project's type is different and the student has been already in group

                if (group != null && group.Project.Type != enrollment.Type)
                {
                    ModelState.AddModelError("Error", "Enrollment's type and Project Type of Group are not the same.");
                    return BadRequest(ModelState);
                }
                else if (group != null && !groupRepository.CheckEnrollment(group, enrollment))
                {
                    ModelState.AddModelError("Warning", "This group already has this student.");
                    return BadRequest(ModelState);
                }
                else
                {
                    enrollment.Group = group;
                    group.Enrollments.Add(enrollment);
                    await unitOfWork.Complete();
                }

            }

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
        public async Task<QueryResultResource<GroupResource>> GetGroups(QueryResource queryResource)
        {
            var query = mapper.Map<QueryResource, Query>(queryResource);

            var queryResult = await groupRepository.GetGroups(query);
            return mapper.Map<QueryResult<Group>, QueryResultResource<GroupResource>>(queryResult);
        }
    }
}
