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
using Microsoft.AspNetCore.SignalR;
using PMS.Hubs;
using PMS.Persistence.IRepository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Controllers
{

    [Route("/api/students")]
    public class StudentController : Controller
    {
        private IMapper mapper;
        private IStudentRepository studentRepository;
        private IMajorRepository majorRepository;
        private IUnitOfWork unitOfWork;
        private IHubContext<PMSHub> hubContext { get; set; }
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public StudentController(IHubContext<PMSHub> hubContext, ApplicationDbContext context,
            IMapper mapper, IStudentRepository studentRepository,
            IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager,
            IMajorRepository majorRepository)
        {
            this.userManager = userManager;
            this.context = context;
            this.mapper = mapper;
            this.studentRepository = studentRepository;
            this.majorRepository = majorRepository;
            this.unitOfWork = unitOfWork;
            this.hubContext = hubContext;
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

            var major = await majorRepository.GetMajor(studentResource.MajorId);
            student.Major = major;

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

            studentRepository.AddStudent(student);
            await unitOfWork.Complete();

            student = await studentRepository.GetStudent(student.Id);
            await hubContext.Clients.All.InvokeAsync("Send", student);
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

            var student = await studentRepository.GetStudent(id);

            if (student == null)
                return NotFound();

            mapper.Map<SaveStudentResource, Student>(studentResource, student);

            var major = await majorRepository.GetMajor(studentResource.MajorId);
            student.Major = major;

            await unitOfWork.Complete();

            var result = mapper.Map<Student, StudentResource>(student);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await studentRepository.GetStudent(id, includeRelated: false);

            if (student == null)
            {
                return NotFound();
            }

            studentRepository.RemoveStudent(student);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getstudent/{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await studentRepository.GetStudent(id);

            if (student == null)
            {
                return NotFound();
            }

            var studentResource = mapper.Map<Student, StudentResource>(student);

            return Ok(studentResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetStudents(QueryResource filterResource)
        {
            var filter = mapper.Map<QueryResource, Query>(filterResource);

            var students = await studentRepository.GetStudents(filter);
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
