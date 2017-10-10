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
        private IEnrollmentRepository enrollmenRrepository;
        private IStudentRepository studentRepository;
        private UserManager<ApplicationUser> userManager;
        private IUnitOfWork unitOfWork;

        public EnrollmentController(IMapper mapper, IUnitOfWork unitOfWork, IEnrollmentRepository enrollmentRepository,
            IStudentRepository studentRepository, UserManager<ApplicationUser> userManager)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.enrollmenRrepository = enrollmentRepository;
            this.studentRepository = studentRepository;
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
            enrollment.Student = await studentRepository.GetStudentByEmail(enrollmentResource.StudentEmail);

            enrollmenRrepository.AddEnrollment(enrollment);
            await unitOfWork.Complete();

            enrollment = await enrollmenRrepository.GetEnrollment(enrollment.EnrollmentId);

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

            var enrollment = await enrollmenRrepository.GetEnrollment(id);

            if (enrollment == null)
                return NotFound();

            mapper.Map<EnrollmentResource, Enrollment>(enrollmentResource, enrollment);
            await unitOfWork.Complete();

            var result = mapper.Map<Enrollment, EnrollmentResource>(enrollment);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var enrollment = await enrollmenRrepository.GetEnrollment(id, includeRelated: false);

            if (enrollment == null)
            {
                return NotFound();
            }

            enrollmenRrepository.RemoveEnrollment(enrollment);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var enrollment = await enrollmenRrepository.GetEnrollment(id);

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
            var enrollments = await enrollmenRrepository.GetEnrollments();
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
