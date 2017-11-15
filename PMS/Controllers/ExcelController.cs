using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using PMS.Models;
using AutoMapper;
using PMS.Resources;
using PMS.Persistence;
using PMS.Persistence.IRepository;

namespace PMS.Controllers
{
    [Route("/api/students/upload")]
    public class ExcelController : Controller
    {
        private readonly int MAX_BYTES = 10 * 1024 * 1024;
        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".xlsx", ".xlsm", ".xlsb", ".xltx",
        ".xltm",".xls",".xlt",".xls",".xml",".xlam",".xla",".xlw",".xlr"};
        private IHostingEnvironment host;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        private IExcelRepository excelRepository;

        public ExcelController(IHostingEnvironment host, IExcelRepository excelRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.host = host;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.excelRepository = excelRepository;
        }
        [HttpPost]
        public async Task<IActionResult> UploadFileAsync(IFormFile file)
        {
            var uploadFolderPath = Path.Combine(host.WebRootPath, "uploads");
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

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var excel = new Excel { FileName = fileName };
            excelRepository.AddExcel(excel);
            await unitOfWork.Complete();

            return Ok(mapper.Map<Excel, ExcelResource>(excel));
        }
    }
}