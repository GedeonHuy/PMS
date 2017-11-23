using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using PMS.Persistence;
using PMS.Resources;
using PMS.Models;
using PMS.Data;
using Microsoft.AspNetCore.Identity;
using PMS.Persistence.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using OfficeOpenXml;

// For more information on enabling MVC for empty lecturers, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Controllers
{
    [Route("/api/lecturers")]
    public class LecturerController : Controller
    {
        private readonly int MAX_BYTES = 10 * 1024 * 1024;
        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".xlsx", ".xlsm", ".xlsb", ".xltx",
        ".xltm",".xls",".xlt",".xls",".xml",".xlam",".xla",".xlw",".xlr"};
        private IMapper mapper;
        private ILecturerRepository lecturerRepository;
        private IMajorRepository majorRepository;
        private IUnitOfWork unitOfWork;
        private IHostingEnvironment host;
        private IExcelRepository excelRepository;
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public LecturerController(ApplicationDbContext context, IMapper mapper,
            ILecturerRepository lecturerRepository, IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager, IMajorRepository majorRepository,
            IHostingEnvironment host, IExcelRepository excelRepository)
        {
            this.userManager = userManager;
            this.context = context;
            this.mapper = mapper;
            this.lecturerRepository = lecturerRepository;
            this.majorRepository = majorRepository;
            this.unitOfWork = unitOfWork;
            this.host = host;
            this.excelRepository = excelRepository;
        }


        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateLecturer([FromBody]LecturerResource lecturerResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lecturer = mapper.Map<LecturerResource, Lecturer>(lecturerResource);

            var major = await majorRepository.GetMajor(lecturerResource.MajorId);
            lecturer.Major = major;

            var user = new ApplicationUser
            {
                FullName = lecturer.Name,
                Major = lecturer.Major.MajorName,
                Email = lecturer.Email,
                UserName = lecturer.Email
            };
            if (RoleExists("Lecturer"))
            {
                //Check Student Existence
                if (!LecturerExists(user.Email))
                {
                    var password = "eiu@123"; // Password Default
                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Lecturer");
                }
                else
                {
                    ModelState.AddModelError("", "Email is registered");
                }
            }
            else
            {
                ModelState.AddModelError("", "'Lecturer' role does not exist");
            }
            lecturerRepository.AddLecturer(lecturer);
            await unitOfWork.Complete();

            lecturer = await lecturerRepository.GetLecturer(lecturer.LecturerId);

            var result = mapper.Map<Lecturer, LecturerResource>(lecturer);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateLecturer(int id, [FromBody]LecturerResource lecturerResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lecturer = await lecturerRepository.GetLecturer(id);

            if (lecturer == null)
                return NotFound();

            mapper.Map<LecturerResource, Lecturer>(lecturerResource, lecturer);

            var major = await majorRepository.GetMajor(lecturerResource.MajorId);
            lecturer.Major = major;

            await unitOfWork.Complete();

            var result = mapper.Map<Lecturer, LecturerResource>(lecturer);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteLecturer(int id)
        {
            var lecturer = await lecturerRepository.GetLecturer(id, includeRelated: false);

            if (lecturer == null)
            {
                return NotFound();
            }

            lecturerRepository.RemoveLecturer(lecturer);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getlecturer/{id}")]
        public async Task<IActionResult> GetLecturer(int id)
        {
            var lecturer = await lecturerRepository.GetLecturer(id);

            if (lecturer == null)
            {
                return NotFound();
            }

            var lecturerResource = mapper.Map<Lecturer, LecturerResource>(lecturer);

            return Ok(lecturerResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<QueryResultResource<LecturerResource>> GetLecturers(QueryResource queryResource)
        {
            var query = mapper.Map<QueryResource, Query>(queryResource);

            var queryResult = await lecturerRepository.GetLecturers(query);
            return mapper.Map<QueryResult<Lecturer>, QueryResultResource<LecturerResource>>(queryResult);
        }

        [HttpGet]
        [Route("finishgrouping")]
        public async Task<IActionResult> FinishGrouping(string email, int QuarterId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var erollments = await lecturerRepository.FinishGroupingAsync(email, QuarterId);
            await unitOfWork.Complete();

            var enrollmentResource = mapper.Map<IEnumerable<Enrollment>, IEnumerable<EnrollmentResource>>(erollments);
            return Ok(enrollmentResource);
        }

        [HttpGet]
        [Route("getenrollments/{email}")]
        public async Task<QueryResultResource<EnrollmentResource>> GetEnrollments(string email, QueryResource queryResource)
        {
            var query = mapper.Map<QueryResource, Query>(queryResource);
            var queryResult = await lecturerRepository.GetEnrollments(query, email);

            return mapper.Map<QueryResult<Enrollment>, QueryResultResource<EnrollmentResource>>(queryResult);
        }

        [HttpGet]
        [Route("getgroups/{email}")]
        public async Task<QueryResultResource<GroupResource>> GetGroups(string email, QueryResource queryResource)
        {
            var query = mapper.Map<QueryResource, Query>(queryResource);
            var queryResult = await lecturerRepository.GetGroups(query, email);

            return mapper.Map<QueryResult<Group>, QueryResultResource<GroupResource>>(queryResult);
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadLecturerFile(IFormFile file)
        {
            var uploadFolderPath = Path.Combine(host.WebRootPath, "uploads/lecturer");
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
                            Lecturer lecturer = new Lecturer();
                            await excelRepository.AddLecturer(lecturer, worksheet, row);
                            lecturerRepository.AddLecturer(lecturer);
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

        private bool LecturerExists(string email)
        {
            return context.Lecturers.Any(e => e.Email == email);
        }
    }
}
