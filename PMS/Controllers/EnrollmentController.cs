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
        private IUnitOfWork unitOfWork;

        public EnrollmentController(IMapper mapper, IUnitOfWork unitOfWork, IEnrollmentRepository enrollmentRepository,
            IStudentRepository studentRepository, IGroupRepository groupRepository, UserManager<ApplicationUser> userManager)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.enrollmentRepository = enrollmentRepository;
            this.studentRepository = studentRepository;
            this.groupRepository = groupRepository;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnrollment([FromBody]EnrollmentResource enrollmentResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var enrollment = mapper.Map<EnrollmentResource, Enrollment>(enrollmentResource);
            var student = await studentRepository.GetStudentByEmail(enrollmentResource.StudentEmail);

            var group = await groupRepository.GetGroup(enrollmentResource.GroupId);

            //case: enrollment's type and project's type is different
            if (group.Project.Type != enrollmentResource.Type)
            {
                ModelState.AddModelError("Error", "Enrollment's type and Project Type of Group are not the same.");
                return BadRequest(ModelState);
            }
            else
            {
                enrollment.Group = await groupRepository.GetGroup(enrollmentResource.GroupId);
            }

            //case: student registed another group with the same type
            if (!studentRepository.CheckStudentEnrollments(student, enrollmentResource.Type))
            {
                ModelState.AddModelError("Error", "Student has an enrollment for this type of projectin another group");
                return BadRequest(ModelState);
            }
            else
            {
                enrollment.Student = student;
            }

            enrollmentRepository.AddEnrollment(enrollment);
            await unitOfWork.Complete();

            enrollment = await enrollmentRepository.GetEnrollment(enrollment.EnrollmentId);

            var result = mapper.Map<Enrollment, EnrollmentResource>(enrollment);

            return Ok(result);
        }

        [HttpPut("{id}")] /*/api/enrollments/id*/
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

            var group = await groupRepository.GetGroup(enrollmentResource.GroupId);

            //case: enrollment's type and project's type is different
            if (group.Project.Type != enrollmentResource.Type)
            {
                ModelState.AddModelError("Error", "Enrollment's type and Project Type of Group are not the same.");
                return BadRequest(ModelState);
            }
            else
            {
                enrollment.Group = await groupRepository.GetGroup(enrollmentResource.GroupId);
            }

            //case student registed another group with the same type
            if (!studentRepository.CheckStudentEnrollments(student, enrollmentResource.Type))
            {
                ModelState.AddModelError("Error", "Student has an enrollment for this type of projectin another group");
                return BadRequest(ModelState);
            }
            else
            {
                enrollment.Student = student;
            }
            await unitOfWork.Complete();

            var result = mapper.Map<Enrollment, EnrollmentResource>(enrollment);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
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
        public async Task<IActionResult> GetEnrollments()
        {
            var enrollments = await enrollmentRepository.GetEnrollments();
            return Ok(enrollments);
        }

        //private async Task<string> getCurrentUserEmailAsync()
        //{
        //    var userID = User.Identity.Name;
        //    var currentUser = await userManager.GetUserAsync(HttpContext.User);
        //    return currentUser.Email;
        //}
    }
}
