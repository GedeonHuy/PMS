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
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Microsoft.AspNetCore.Hosting;
using System.IO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Controllers
{
    [Route("/api/projects")]
    public class ProjectController : Controller
    {
        private readonly int MAX_BYTES = 10 * 1024 * 1024;
        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".xlsx", ".xlsm", ".xlsb", ".xltx",
        ".xltm",".xls",".xlt",".xls",".xml",".xlam",".xla",".xlw",".xlr"};
        private IMapper mapper;
        private IProjectRepository projectRepository;
        private IMajorRepository majorRepository;
        private IHostingEnvironment host;
        private IUnitOfWork unitOfWork;
        private IExcelRepository excelRepository;
        private ILecturerRepository lecturerRepository;

        public ProjectController(IMapper mapper, IUnitOfWork unitOfWork,
            IProjectRepository projectRepository, IMajorRepository majorRepository,
            IHostingEnvironment host, IExcelRepository excelRepository, ILecturerRepository lecturerRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.projectRepository = projectRepository;
            this.majorRepository = majorRepository;
            this.host = host;
            this.excelRepository = excelRepository;
            this.lecturerRepository = lecturerRepository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateProject([FromBody]ProjectResource projectResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = mapper.Map<ProjectResource, Project>(projectResource);

            var major = await majorRepository.GetMajor(projectResource.MajorId);
            project.Major = major;

            var lecturer = await lecturerRepository.GetLecturer(projectResource.LecturerId);
            project.Lecturer = lecturer;

            projectRepository.AddProject(project);

            projectRepository.UpdateGroups(project, projectResource);
            projectRepository.UpdateTagProjects(project, projectResource);

            await unitOfWork.Complete();

            project = await projectRepository.GetProject(project.ProjectId);

            var result = mapper.Map<Project, ProjectResource>(project);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody]ProjectResource projectResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = await projectRepository.GetProject(id);

            if (project == null)
                return NotFound();

            mapper.Map<ProjectResource, Project>(projectResource, project);

            projectRepository.UpdateGroups(project, projectResource);

            var major = await majorRepository.GetMajor(projectResource.MajorId);
            project.Major = major;

            var lecturer = await lecturerRepository.GetLecturer(projectResource.LecturerId);
            project.Lecturer = lecturer;

            projectRepository.UpdateGroups(project, projectResource);
            projectRepository.UpdateTagProjects(project, projectResource);

            await unitOfWork.Complete();

            var result = mapper.Map<Project, ProjectResource>(project);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await projectRepository.GetProject(id, includeRelated: false);

            if (project == null)
            {
                return NotFound();
            }

            projectRepository.RemoveProject(project);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getproject/{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await projectRepository.GetProject(id);

            if (project == null)
            {
                return NotFound();
            }

            var projectResource = mapper.Map<Project, ProjectResource>(project);

            return Ok(projectResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<QueryResultResource<ProjectResource>> GetProjects(QueryResource queryResource)
        {
            var query = mapper.Map<QueryResource, Query>(queryResource);

            var queryResult = await projectRepository.GetProjects(query);

            return mapper.Map<QueryResult<Project>, QueryResultResource<ProjectResource>>(queryResult);
        }

        [HttpGet]
        [Route("getprojectsbymajor/{id}")]
        public async Task<QueryResultResource<ProjectResource>> GetProjectsByMajor(int id)
        {
            QueryResource queryResource = new QueryResource();

            var query = mapper.Map<QueryResource, Query>(queryResource);
            var queryResult = await projectRepository.GetProjectsByMajor(id, query);

            return mapper.Map<QueryResult<Project>, QueryResultResource<ProjectResource>>(queryResult);
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadLecturerFile(IFormFile file)
        {

            var uploadFolderPath = Path.Combine(host.ContentRootPath, "uploads/project");
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

            // add projects
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
                            Project project = new Project();
                            await excelRepository.AddProject(project, worksheet, row);
                            projectRepository.AddProject(project);
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
    }
}
