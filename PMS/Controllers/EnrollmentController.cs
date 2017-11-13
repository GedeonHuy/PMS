using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using PMS.Persistence;
using PMS.Models;
using PMS.Resources;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using PMS.Persistence.IRepository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Controllers
{
    [Route("/api/enrollments")]
    public class EnrollmentController : Controller
    {
        private IMapper mapper;
        private IEnrollmentRepository enrollmentRepository;
        private IStudentRepository studentRepository;
        private IGroupRepository groupRepository;
        private UserManager<ApplicationUser> userManager;
        private IQuarterRepository quarterRepository;
        private ILecturerRepository lecturerRepository;
        private IUnitOfWork unitOfWork;

        public EnrollmentController(IMapper mapper, IUnitOfWork unitOfWork, IEnrollmentRepository enrollmentRepository,
            IStudentRepository studentRepository, IGroupRepository groupRepository,
            UserManager<ApplicationUser> userManager, IQuarterRepository quarterRepository,
            ILecturerRepository lecturerRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.enrollmentRepository = enrollmentRepository;
            this.studentRepository = studentRepository;
            this.groupRepository = groupRepository;
            this.userManager = userManager;
            this.quarterRepository = quarterRepository;
            this.lecturerRepository = lecturerRepository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateEnrollment([FromBody]EnrollmentResource enrollmentResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var enrollment = mapper.Map<EnrollmentResource, Enrollment>(enrollmentResource);
            var student = await studentRepository.GetStudentByEmail(enrollmentResource.StudentEmail);
            enrollment.Student = student;

            var lecturer = await lecturerRepository.GetLecturer(enrollmentResource.LecturerId);
            enrollment.Lecturer = lecturer;

            //case: enrollment's type and project's type is different and the student has been already in group
            var group = await groupRepository.GetGroup(enrollmentResource.GroupId);
            if (group != null && group.Project.Type != enrollmentResource.Type)
            {
                ModelState.AddModelError("Error", "Enrollment's type and Project Type of Group are not the same.");
                return BadRequest(ModelState);
            }
            else if (!groupRepository.CheckEnrollment(group, enrollment))
            {
                ModelState.AddModelError("Warning", "This group already has this student.");
                return BadRequest(ModelState);
            }
            else
            {
                enrollment.Group = group;
            }

            var quarter = await quarterRepository.GetQuarter(enrollmentResource.QuarterId);
            enrollment.Quarter = quarter;

            enrollmentRepository.AddEnrollment(enrollment);
            await unitOfWork.Complete();

            enrollment = await enrollmentRepository.GetEnrollment(enrollment.EnrollmentId);

            var result = mapper.Map<Enrollment, EnrollmentResource>(enrollment);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateEnrollment(int id, [FromBody]EnrollmentResource enrollmentResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var enrollment = await enrollmentRepository.GetEnrollment(id);

            if (enrollment == null)
                return NotFound();

            mapper.Map<EnrollmentResource, Enrollment>(enrollmentResource, enrollment);
            var student = await studentRepository.GetStudentByEmail(enrollmentResource.StudentEmail);
            enrollment.Student = student;

            var group = await groupRepository.GetGroup(enrollmentResource.GroupId);
            enrollment.Group = group;

            var lecturer = await lecturerRepository.GetLecturer(enrollmentResource.LecturerId);
            enrollment.Lecturer = lecturer;

            ////case: enrollment's type and project's type is different
            //if (group.Project.Type != enrollmentResource.Type)
            //{
            //    ModelState.AddModelError("Error", "Enrollment's type and Project Type of Group are not the same.");
            //    return BadRequest(ModelState);
            //}
            //else
            //{
            //    enrollment.Group = await groupRepository.GetGroup(enrollmentResource.GroupId);
            //}

            ////case student registed another group with the same type
            //if (!studentRepository.CheckStudentEnrollments(student, enrollmentResource.Type))
            //{
            //    ModelState.AddModelError("Error", "Student has an enrollment for this type of projectin another group");
            //    return BadRequest(ModelState);
            //}
            //else
            //{
            //    enrollment.Student = student;
            //}

            var quarter = await quarterRepository.GetQuarter(enrollmentResource.QuarterId);
            enrollment.Quarter = quarter;

            await unitOfWork.Complete();

            var result = mapper.Map<Enrollment, EnrollmentResource>(enrollment);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var enrollment = await enrollmentRepository.GetEnrollment(id, includeRelated: false);

            if (enrollment == null)
            {
                return NotFound();
            }

            enrollmentRepository.RemoveEnrollment(enrollment);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getenrollment/{id}")]
        public async Task<IActionResult> GetEnrollment(int id)
        {
            var enrollment = await enrollmentRepository.GetEnrollment(id);

            if (enrollment == null)
            {
                return NotFound();
            }

            var enrollmentResource = mapper.Map<Enrollment, EnrollmentResource>(enrollment);
            return Ok(enrollmentResource);
        }

        [HttpGet]
        [Route("getenrollmentbyemail/{email}")]
        public async Task<IActionResult> GetEnrollmentByEmail(string email)
        {
            var enrollment = await enrollmentRepository.GetEnrollmentByEmail(email);

            if (enrollment == null)
            {
                return NotFound();
            }

            var enrollmentResource = mapper.Map<Enrollment, EnrollmentResource>(enrollment);
            return Ok(enrollmentResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<QueryResultResource<EnrollmentResource>> GetEnrollments(QueryResource queryResource)
        {
            var query = mapper.Map<QueryResource, Query>(queryResource);
            var queryResult = await enrollmentRepository.GetEnrollments(query);

            return mapper.Map<QueryResult<Enrollment>, QueryResultResource<EnrollmentResource>>(queryResult);
        }

        //private async Task<string> getCurrentUserEmailAsync()
        //{
        //    var userID = User.Identity.Name;
        //    var currentUser = await userManager.GetUserAsync(HttpContext.User);
        //    return currentUser.Email;
        //}
    }
}
