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
using Microsoft.AspNetCore.Identity;
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
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public StudentController(ApplicationDbContext context, IMapper mapper, IStudentRepository repository, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.context = context;
            this.mapper = mapper;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateStudent([FromBody]SaveStudentResource studentResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = mapper.Map<SaveStudentResource, Student>(studentResource);

            var user = new ApplicationUser
            {
                FullName = student.Name,
                Email = student.Email,
                UserName = student.Email
            };

            if (RoleExists("Student"))
            {
                //Check Student Existence
                if (!StudentExists(user.Email) && !StudentIdExists(student.StudentCode))
                {
                    var password = student.StudentCode.ToString(); // Password Default
                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Student");
                }
            }

            repository.AddStudent(student);
            await unitOfWork.Complete();

            student = await repository.GetStudent(student.Id);

            var result = mapper.Map<Student, StudentResource>(student);

            return Ok(result);
        }

        [HttpPut] /*/api/students/update/id*/
        [Route("update/{id}")]
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

        [HttpDelete]
        [Route("delete/{id}")]
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

        [HttpGet]
        [Route("getstudent/{id}")]
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
        [Route("getall")]
        public async Task<IActionResult> GetStudents()
        {
            var students = await repository.GetStudents();
            var studentResource = mapper.Map<IEnumerable<Student>, IEnumerable<StudentResource>>(students);
            return Ok(studentResource);
        }

        private bool RoleExists(string roleName)
        {
            return context.ApplicationRole.Any(r => r.Name == roleName);
        }

        private bool StudentIdExists(string studentCode)
        {
            return context.Students.Any(r => r.StudentCode == studentCode);
        }

        private bool StudentExists(string email)
        {
            return context.Students.Any(e => e.Email == email);
        }
    }
}
