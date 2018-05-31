using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PMS.Data;
using PMS.Persistence;
using PMS.Resources;
using AutoMapper;
using PMS.Models;
using PMS.Persistence.IRepository;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace PMS.Controllers
{
    [Route("/api/uploadfiles")]
    public class UploadedFileController : Controller
    {
        private ApplicationDbContext context;
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private IUploadedFileRepository uploadedFileRepository;
        private IGroupRepository groupRepository;
        private IHostingEnvironment host;

        public UploadedFileController(ApplicationDbContext context, IUnitOfWork unitOfWork,
            IMapper mapper, IUploadedFileRepository uploadedFileRepository, IHostingEnvironment host,
            IGroupRepository groupRepository)
        {
            this.context = context;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.uploadedFileRepository = uploadedFileRepository;
            this.groupRepository = groupRepository;
            this.host = host;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateUploadedFile([FromBody]UploadedFileResource uploadedFileResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var uploadedFile = mapper.Map<UploadedFileResource, UploadedFile>(uploadedFileResource);

            var group = await groupRepository.GetGroup(uploadedFileResource.GroupId);
            uploadedFile.Group = group;


            uploadedFileRepository.AddUploadedFile(uploadedFile);
            await unitOfWork.Complete();

            uploadedFile = await uploadedFileRepository.GetUploadedFile(uploadedFile.UploadedFileId);

            var result = mapper.Map<UploadedFile, UploadedFileResource>(uploadedFile);

            return Ok(result);
        }

        [HttpPost]
        [Route("addfile/{id}")]
        public async Task<IActionResult> UploadFile(int id, IFormFile file)
        {
            var uploadedFile = await uploadedFileRepository.GetUploadedFile(id);
            if (uploadedFile == null)
            {
                return BadRequest("No Id of uploaded file can be found");
            }

            var uploadFolderPath = Path.Combine(host.ContentRootPath, "uploads/uploadedFile");
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
            //extension
            if (Path.GetExtension(file.FileName) == ".xlsx")
            {
                uploadedFile.Url = "fa fa-file-excel-o";
            }
            else if (Path.GetExtension(file.FileName) == ".doc")
            {
                uploadedFile.Url = "fa fa-file-word-o";
            }
            else if (Path.GetExtension(file.FileName) == ".pdf")
            {
                uploadedFile.Url = "fa fa-file-pdf-o";
            }
            else if (Path.GetExtension(file.FileName) == ".ppt" || Path.GetExtension(file.FileName) == ".pptx")
            {
                uploadedFile.Url = "fa fa-file-pdf-o";
            }
            else if (Path.GetExtension(file.FileName) == ".png" || Path.GetExtension(file.FileName) == ".bmp"
                || Path.GetExtension(file.FileName) == ".jpeg" || Path.GetExtension(file.FileName) == ".jpg")
            {
                uploadedFile.Url = "fa fa-picture-o";
            }
            else if (Path.GetExtension(file.FileName) == ".rar" || Path.GetExtension(file.FileName) == ".zip"
                || Path.GetExtension(file.FileName) == ".zip5" || Path.GetExtension(file.FileName) == ".7zip")
            {
                uploadedFile.Url = "fa-file-archive-o";
            }
            else if (Path.GetExtension(file.FileName) == ".flv" || Path.GetExtension(file.FileName) == ".avi"
                || Path.GetExtension(file.FileName) == ".mp4" || Path.GetExtension(file.FileName) == ".wmv")
            {
                uploadedFile.Url = "fa-file-video-o";
            }
            else
            {
                uploadedFile.Url = "fa fa-files-o";
            }

            var fileName = uploadedFile.UploadedFileId + Path.GetExtension(file.FileName);
            uploadedFile.FileName = fileName;

            var filePath = Path.Combine(uploadFolderPath, fileName);
            //create file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            await unitOfWork.Complete();
            var result = mapper.Map<UploadedFile, UploadedFileResource>(uploadedFile);

            return Ok(result);
        }

        [HttpGet]
        [Route("downloadfile/{id}")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var uploadedFile = await uploadedFileRepository.GetUploadedFile(id);
            if (uploadedFile == null)
            {
                return BadRequest("No Id of uploaded file can be found");
            }

            var contentType = "application/octet-stream";
            var downloadPath = Path.Combine(host.WebRootPath, "uploads/uploadedFile/" + uploadedFile.FileName);

            var memory = new MemoryStream();
            using (var stream = new FileStream(downloadPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, contentType, uploadedFile.Title + Path.GetExtension(downloadPath));
        }


        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateUploadedFile(int id, [FromBody]UploadedFileResource uploadedFileResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var uploadedFile = await uploadedFileRepository.GetUploadedFile(id);

            if (uploadedFile == null)
                return NotFound();

            mapper.Map<UploadedFileResource, UploadedFile>(uploadedFileResource, uploadedFile);
            await unitOfWork.Complete();

            var result = mapper.Map<UploadedFile, UploadedFileResource>(uploadedFile);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteUploadedFile(int id)
        {
            var uploadedFile = await uploadedFileRepository.GetUploadedFile(id, includeRelated: false);

            if (uploadedFile == null)
            {
                return NotFound();
            }

            uploadedFileRepository.RemoveUploadedFile(uploadedFile);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getuploadedfile/{id}")]
        public async Task<IActionResult> GetUploadedFile(int id)
        {
            var uploadedFile = await uploadedFileRepository.GetUploadedFile(id);

            if (uploadedFile == null)
            {
                return NotFound();
            }

            var uploadedFileResource = mapper.Map<UploadedFile, UploadedFileResource>(uploadedFile);

            return Ok(uploadedFileResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetUploadedFiles()
        {
            var uploadedFiles = await uploadedFileRepository.GetUploadedFiles();
            var uploadedFilesResource = mapper.Map<IEnumerable<UploadedFile>, IEnumerable<UploadedFileResource>>(uploadedFiles);
            return Ok(uploadedFilesResource);
        }
    }
}