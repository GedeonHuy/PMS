using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PMS.Models;
using PMS.Resources;
using AutoMapper;
using PMS.Data;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using PMS.Persistence;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Controllers
{

    [Route("/api/students")]
    public class StudentController : Controller
    {
        private IMapper mapper;
        private IStudentRepository repository;
        private IUnitOfWork unitOfWork;

        public StudentController(IMapper mapper, IStudentRepository repository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody]SaveStudentResource studentResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = mapper.Map<SaveStudentResource, Student>(studentResource);

            repository.AddStudent(student);
            await unitOfWork.Complete();

            student = await repository.GetStudent(student.StudentId);

            var result = mapper.Map<Student, StudentResource>(student);

            return Ok(result);
        }

        [HttpPut("{id}")] /*/api/students/id*/
        public async Task<IActionResult> UpdateStudent(int id, [FromBody]SaveStudentResource studentResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = await repository.GetStudent(id);

            if (student == null)
                return NotFound();

            mapper.Map<SaveStudentResource, Student>(studentResource, student);
            await unitOfWork.Complete();

            var result = mapper.Map<Student, StudentResource>(student);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await repository.GetStudent(id, includeRelated: false);

            if (student == null)
            {
                return NotFound();
            }

            repository.RemoveStudent(student);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await repository.GetStudent(id);

            if (student == null)
            {
                return NotFound();
            }

            var studentResource = mapper.Map<Student, StudentResource>(student);

            return Ok(studentResource);
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await repository.GetStudents();
            return Ok(students);
        }
    }
}
