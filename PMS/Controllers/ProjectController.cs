using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Storage.v1;
using Google.Cloud.Language.V1;
using Google.Cloud.Storage.V1;
using Grpc.Auth;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using PMS.Models;
using PMS.Persistence;
using PMS.Persistence.IRepository;
using PMS.Resources;
using static Google.Cloud.Language.V1.AnnotateTextRequest.Types;
using Accord.Math;
using Google.Cloud.Translation.V2;
using Microsoft.EntityFrameworkCore;
using PMS.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Controllers {
    [Route ("/api/projects")]
    public class ProjectController : Controller {
        private readonly int MAX_BYTES = 10 * 1024 * 1024;
        private readonly string[] ACCEPTED_FILE_TYPES = new [] {
            ".xlsx",
            ".xlsm",
            ".xlsb",
            ".xltx",
            ".xltm",
            ".xls",
            ".xlt",
            ".xls",
            ".xml",
            ".xlam",
            ".xla",
            ".xlw",
            ".xlr"
        };
        private IMapper mapper;
        private IProjectRepository projectRepository;
        private IMajorRepository majorRepository;
        private IHostingEnvironment host;
        private IUnitOfWork unitOfWork;
        private IExcelRepository excelRepository;
        private ILecturerRepository lecturerRepository;

        private readonly ApplicationDbContext context;
        public ProjectController (ApplicationDbContext context, IMapper mapper, IUnitOfWork unitOfWork,
            IProjectRepository projectRepository, IMajorRepository majorRepository,
            IHostingEnvironment host, IExcelRepository excelRepository, ILecturerRepository lecturerRepository) {
            this.context = context;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.projectRepository = projectRepository;
            this.majorRepository = majorRepository;
            this.host = host;
            this.excelRepository = excelRepository;
            this.lecturerRepository = lecturerRepository;
        }

        [HttpPost]
        [Route ("add")]
        public async Task<IActionResult> CreateProject ([FromBody] ProjectResource projectResource) {
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }

            var project = mapper.Map<ProjectResource, Project> (projectResource);

            var major = await majorRepository.GetMajor (projectResource.MajorId);
            project.Major = major;

            var lecturer = await lecturerRepository.GetLecturer (projectResource.LecturerId);
            project.Lecturer = lecturer;

            projectRepository.AddProject (project);

            projectRepository.UpdateGroups (project, projectResource);
            projectRepository.UpdateTagProjects (project, projectResource);

            //Add categories into projects
            var categories = GetCategoriesFromDescription (projectResource.Description);
            await projectRepository.UpdateCategories (project, categories);

            await unitOfWork.Complete ();

            project = await projectRepository.GetProject (project.ProjectId);

            var result = mapper.Map<Project, ProjectResource> (project);

            return Ok (result);
        }

        [HttpPut]
        [Route ("update/{id}")]
        public async Task<IActionResult> UpdateProject (int id, [FromBody] ProjectResource projectResource) {
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }

            var project = await projectRepository.GetProject (id);

            if (project == null)
                return NotFound ();

            mapper.Map<ProjectResource, Project> (projectResource, project);

            projectRepository.UpdateGroups (project, projectResource);

            var major = await majorRepository.GetMajor (projectResource.MajorId);
            project.Major = major;

            var lecturer = await lecturerRepository.GetLecturer (projectResource.LecturerId);
            project.Lecturer = lecturer;

            projectRepository.UpdateGroups (project, projectResource);
            projectRepository.UpdateTagProjects (project, projectResource);

            //Add categories into project
            var categories = GetCategoriesFromDescription (projectResource.Description);
            await projectRepository.UpdateCategories (project, categories);

            await unitOfWork.Complete ();

            var result = mapper.Map<Project, ProjectResource> (project);
            return Ok (result);
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteProject (int id) {
            var project = await projectRepository.GetProject (id, includeRelated : false);

            if (project == null) {
                return NotFound ();
            }

            projectRepository.RemoveProject (project);
            await unitOfWork.Complete ();

            return Ok (id);
        }

        [HttpGet]
        [Route ("getproject/{id}")]
        public async Task<IActionResult> GetProject (int id) {
            var project = await projectRepository.GetProject (id);

            if (project == null) {
                return NotFound ();
            }

            var projectResource = mapper.Map<Project, ProjectResource> (project);

            return Ok (projectResource);
        }

        [HttpGet]
        [Route ("getall")]
        public async Task<QueryResultResource<ProjectResource>> GetProjects (QueryResource queryResource) {
            var query = mapper.Map<QueryResource, Query> (queryResource);

            var queryResult = await projectRepository.GetProjects (query);

            return mapper.Map<QueryResult<Project>, QueryResultResource<ProjectResource>> (queryResult);
        }

        [HttpGet]
        [Route ("getprojectsbymajor/{id}")]
        public async Task<QueryResultResource<ProjectResource>> GetProjectsByMajor (int id) {
            QueryResource queryResource = new QueryResource ();

            var query = mapper.Map<QueryResource, Query> (queryResource);
            var queryResult = await projectRepository.GetProjectsByMajor (id, query);

            return mapper.Map<QueryResult<Project>, QueryResultResource<ProjectResource>> (queryResult);
        }

        [HttpPost]
        [Route ("upload")]
        public async Task<IActionResult> UploadLecturerFile (IFormFile file) {

            var uploadFolderPath = Path.Combine (host.ContentRootPath, "uploads/project");
            if (!Directory.Exists (uploadFolderPath)) {
                Directory.CreateDirectory (uploadFolderPath);
            }

            if (file == null) {
                return BadRequest ("STOP HACKING OUR WEBSITE. SEND A FILE FOR US TO EXECUTE, PLEASE");
            }

            if (file.Length == 0) {
                return BadRequest ("DO YOU THINK A EMPTY FILE CAN CRASH OUR WEBSITE");
            }

            if (file.Length > MAX_BYTES) {
                return BadRequest ("PLEASE CHOOSE A FILE WHICH SIZE < 10 MB. OUR SYSTEM IS TOO BUSY TO DO EXECUTE THIS FILE");
            }

            if (!ACCEPTED_FILE_TYPES.Any (s => s == Path.GetExtension (file.FileName.ToLower ()))) {
                return BadRequest ("ARE YOU HAPPY WHEN DO THAT. CHOOSE VALID TYPE, PLEASE");
            }

            var fileName = Guid.NewGuid ().ToString () + Path.GetExtension (file.FileName);
            var filePath = Path.Combine (uploadFolderPath, fileName);

            //create excel
            using (var stream = new FileStream (filePath, FileMode.Create)) {
                await file.CopyToAsync (stream);
            }

            // add projects
            using (ExcelPackage package = new ExcelPackage (new FileInfo (filePath))) {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                int rowCount = worksheet.Dimension.Rows;
                try {
                    for (int row = 3; row <= rowCount; row++) {
                        var val = worksheet.Cells[row, 2].Value;
                        if (val == null) {
                            break;
                        } else {
                            Project project = new Project ();
                            await excelRepository.AddProject (project, worksheet, row);
                            projectRepository.AddProject (project);
                        }
                        await unitOfWork.Complete ();
                    }
                } catch (Exception ex) {
                    return BadRequest ("CHECK YOUR FILE AGAIN, PLEASE. SOME WRONG WITH THIS FILE" +
                        "\n" + "Detail: " + ex.Message);
                }
            }

            //add to db
            var excel = new Excel { FileName = fileName };
            excelRepository.AddExcel (excel);
            await unitOfWork.Complete ();

            return Ok (mapper.Map<Excel, ExcelResource> (excel));
        }

        [HttpGet]
        [Route ("testai")]
        public IActionResult TestAI () {
            var a = GetCategoriesFromDescription ("Google Home enables users to speak voice commands to interact with services through the Home's intelligent personal assistant called Google Assistant. A large number of services, both in-house and third-party, are integrated, allowing users to listen to music, look at videos or photos, or receive news updates entirely by voice.");
            var b = GetCategoriesFromDescription ("Android is a mobile operating system developed by Google, based on the Linux kernel and designed primarily for touchscreen mobile devices such as smartphones and tablets.");
            var c = GetCategoriesFromDescription ("Google Cloud Platform, offered by Google, is a suite of cloud computing services that runs on the same infrastructure that Google uses internally for its end-user products, such as Google Search and YouTube. Alongside a set of management tools, it provides a series of modular cloud services including computing, data storage, data analytics and machine learning.");
            var d = GetCategoriesFromDescription ("Google is an American multinational technology company that specializes in Internet-related services and products. These include online advertising technologies, search, cloud computing, software, and hardware.");

            var norm1 = Norm.Euclidean (a.Values.ToArray ());
            var norm2 = Norm.Euclidean (d.Values.ToArray ());
            var dot = 0.0;
            foreach (var label in a) {
                if (d.ContainsKey (label.Key)) {
                    dot += a[label.Key] * d[label.Key];
                }
            }
            //return Ok(d);
            return Ok (dot / (norm1 * norm2));
        }

        [HttpPost]
        [Route ("analyzeproject")]
        public IActionResult AnalyzeProject ([FromBody] string description) {
            var credential = GoogleCredential.FromFile ("pms-portal-trans.json");
            TranslationClient client = TranslationClient.Create (credential);

            var category = GetCategoriesFromDescription (client.TranslateText (description, "en").TranslatedText);

            // var projects = context.Projects
            //     .Include (p => p.Groups)
            //     .ThenInclude (g => g.Board)
            //     .Where (p => (p.Groups != null || p.Groups.Count > 0) && p.Groups.Any (g => g.Board.ResultScore == null));

            var projects = context.Projects.ToList();
            
            var topSimilarity = new List<String> ();
            var similarity = new Dictionary<Project, double> ();

            foreach (var project in projects) {
                var response = client.TranslateText (project.Description, "en");
                if (Similarity (category, GetCategoriesFromDescription (response.TranslatedText)) >= 0.5) {
                    similarity.Add (project, Math.Round (Similarity (category, GetCategoriesFromDescription (response.TranslatedText)), 3));
                }
            }

            var top3Project = (from entry in similarity orderby entry.Value descending select entry).ToDictionary (
                pair => pair.Key,
                pair => pair.Value
            ).Take (3);

            return Ok (top3Project);
        }

        public double Similarity (Dictionary<string, double> mainDict, Dictionary<string, double> dct2) {
            var norm1 = Norm.Euclidean (mainDict.Values.ToArray ());
            var norm2 = Norm.Euclidean (dct2.Values.ToArray ());

            var dot = 0.0;
            foreach (var label in mainDict) {
                if (dct2.ContainsKey (label.Key)) {
                    dot += mainDict[label.Key] * dct2[label.Key];
                }
            }

            return dot / (norm1 * norm2);
        }

        [HttpGet]
        [Route ("demo")]
        public IActionResult DemoNatural () {

            string text = "The Eastern International University is a business school (Becamex IDC), operating under the model of many universities in a university; It attracts and gathers graduates from many European countries. To train students in the direction of research - application, good at both professional and foreign languages, according to the main idea is close relationship - mutual and community service; Training on the development needs of reality, contributing to promoting the sustainable development of the country.";

            var credential = GoogleCredential.FromFile ("pms-portal.json")
                .CreateScoped (LanguageServiceClient.DefaultScopes);
            var channel = new Grpc.Core.Channel (
                LanguageServiceClient.DefaultEndpoint.ToString (),
                credential.ToChannelCredentials ());
            var client = LanguageServiceClient.Create (channel);

            var response = client.ClassifyText (new Document () {
                Content = text,
                    Type = Document.Types.Type.PlainText
            });
            return Ok (response);
        }

        public Dictionary<string, double> GetCategoriesFromDescription (string text) {
            try {

                var credential = GoogleCredential.FromFile ("pms-portal.json")
                    .CreateScoped (LanguageServiceClient.DefaultScopes);
                var channel = new Grpc.Core.Channel (
                    LanguageServiceClient.DefaultEndpoint.ToString (),
                    credential.ToChannelCredentials ());
                var client = LanguageServiceClient.Create (channel);
                var response = client.AnnotateText (new Document () {
                        Content = text,
                            Type = Document.Types.Type.PlainText
                    },

                    new Features () {
                        ExtractSyntax = true,
                            ExtractDocumentSentiment = true,
                            ExtractEntities = true,
                            ExtractEntitySentiment = true,
                            ClassifyText = true,
                    });
                Dictionary<string, double> categories = new Dictionary<string, double> ();
                foreach (var res in response.Categories) {
                    var tmp = res.Name.Split ("/");
                    foreach (var label in tmp) {
                        if (label != "") {
                            categories[label] = res.Confidence;
                        }
                    }
                }
                return categories;
            } catch {
                Dictionary<string, double> categories = new Dictionary<string, double> ();
                return categories;
            }
        }

        [HttpPost]
        [Route ("getsimilarprojects")]
        public async Task<QueryResultResource<ProjectResource>> GetSimilarProjects ([FromBody] ProjectResource projectResource) {
            QueryResource queryResource = new QueryResource ();

            var query = mapper.Map<QueryResource, Query> (queryResource);
            var queryResult = await projectRepository.GetSimilarProjects (query, projectResource);

            return mapper.Map<QueryResult<Project>, QueryResultResource<ProjectResource>> (queryResult);
        }
    }
}