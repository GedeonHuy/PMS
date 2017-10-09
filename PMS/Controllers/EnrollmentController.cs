using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using PMS.Persistence;
using PMS.Models;
using PMS.Resources;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Controllers
{
    [Route("/api/enrollments")]
    public class EnrollmentController : Controller
    {
        private IMapper mapper;
        private IEnrollmentRepository repository;
        private IUnitOfWork unitOfWork;

        public EnrollmentController(IMapper mapper, IUnitOfWork unitOfWork, IEnrollmentRepository repository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnrollment([FromBody]EnrollmentResource enrollmentResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var enrollment = mapper.Map<EnrollmentResource, Enrollment>(enrollmentResource);
            /////just for test/////
            //enrollment.GradeId = null;
            //enrollment.GroupId = null;
            //////////////////////

            repository.AddEnrollment(enrollment);
            await unitOfWork.Complete();

            enrollment = await repository.GetEnrollment(enrollment.EnrollmentId);

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

            var enrollment = await repository.GetEnrollment(id);

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
            var enrollment = await repository.GetEnrollment(id, includeRelated: false);

            if (enrollment == null)
            {
                return NotFound();
            }

            repository.RemoveEnrollment(enrollment);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var enrollment = await repository.GetEnrollment(id);

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
            var enrollments = await repository.GetEnrollments();
            return Ok(enrollments);
        }
    }
}
