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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Controllers
{

    [Route("/api/students")]
    public class StudentController : Controller
    {
        private readonly int MAX_BYTES = 10 * 1024 * 1024;
        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".xlsx", ".xlsm", ".xlsb", ".xltx",
        ".xltm",".xls",".xlt",".xls",".xml",".xlam",".xla",".xlw",".xlr"};
        private IMapper mapper;
        private IStudentRepository studentRepository;
        private IMajorRepository majorRepository;
        private IUnitOfWork unitOfWork;
        private IHubContext<PMSHub> hubContext { get; set; }

        private IHostingEnvironment host;
        private IExcelRepository excelRepository;
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public StudentController(IHubContext<PMSHub> hubContext, ApplicationDbContext context,
            IMapper mapper, IStudentRepository studentRepository,
            IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager,
            IMajorRepository majorRepository, IHostingEnvironment host, IExcelRepository excelRepository)
        {
            this.userManager = userManager;
            this.context = context;
            this.mapper = mapper;
            this.studentRepository = studentRepository;
            this.majorRepository = majorRepository;
            this.unitOfWork = unitOfWork;
            this.hubContext = hubContext;
            this.host = host;
            this.excelRepository = excelRepository;
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
                Avatar = "/assets/images/user.png",
                Major = student.Major.MajorName,
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
            await hubContext.Clients.All.InvokeAsync("LoadData");
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
        [Route("getenrollments/{email}")]
        public async Task<QueryResultResource<EnrollmentResource>> GetEnrollments(string email, QueryResource queryResource)
        {
            var query = mapper.Map<QueryResource, Query>(queryResource);
            var queryResult = await studentRepository.GetEnrollments(query, email);

            return mapper.Map<QueryResult<Enrollment>, QueryResultResource<EnrollmentResource>>(queryResult);
        }

        [HttpGet]
        [Route("getgroups/{email}")]
        public async Task<QueryResultResource<GroupResource>> GetGroups(string email, QueryResource queryResource)
        {
            var query = mapper.Map<QueryResource, Query>(queryResource);
            var queryResult = await studentRepository.GetGroups(query, email);

            return mapper.Map<QueryResult<Group>, QueryResultResource<GroupResource>>(queryResult);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<QueryResultResource<StudentResource>> GetStudents(QueryResource queryResource)
        {
            var query = mapper.Map<QueryResource, Query>(queryResource);
            var queryResult = await studentRepository.GetStudents(query);

            return mapper.Map<QueryResult<Student>, QueryResultResource<StudentResource>>(queryResult);
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadStudentFile(IFormFile file)
        {
            var uploadFolderPath = Path.Combine(host.WebRootPath, "uploads/student");
            if (!Directory.Exists(uploadFolderPath))
            {
                Directory.CreateDirectory(uploadFolderPath);
            }

            if (file == null)
            {
                return BadRequest("STOP HACKING OUR WEBSITE. SEND A FILE FOR US TO EXECUTE, PLEASE");
            }

            if (file.Length == 0)
            {
                return BadRequest("DO YOU THINK A EMPTY FILE CAN CRASH OUR WEBSITE");
            }

            if (file.Length > MAX_BYTES)
            {
                return BadRequest("PLEASE CHOOSE A FILE WHICH SIZE < 10 MB. OUR SYSTEM IS TOO BUSY TO DO EXECUTE THIS FILE");
            }

            if (!ACCEPTED_FILE_TYPES.Any(s => s == Path.GetExtension(file.FileName.ToLower())))
            {
                return BadRequest("ARE YOU HAPPY WHEN DO THAT. CHOOSE VALID TYPE, PLEASE");
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadFolderPath, fileName);

            //create excel
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // add students
            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                int rowCount = worksheet.Dimension.Rows;
                try
                {
                    for (int row = 3; row <= rowCount; row++)
                    {
                        var val = worksheet.Cells[row, 2].Value;
                        if (val == null)
                        {
                            break;
                        }
                        else
                        {
                            Student student = new Student();
                            await excelRepository.AddStudent(student, worksheet, row);
                            studentRepository.AddStudent(student);
                        }
                        await unitOfWork.Complete();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest("CHECK YOUR FILE AGAIN, PLEASE. SOME WRONG WITH THIS FILE"
                        + "\n" + "Detail: " + ex.Message);
                }
            }

            //add to db
            var excel = new Excel { FileName = fileName };
            excelRepository.AddExcel(excel);
            await unitOfWork.Complete();

            return Ok(mapper.Map<Excel, ExcelResource>(excel));
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
