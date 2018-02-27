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
using OfficeOpenXml;

namespace PMS.Controllers
{

    public class ExcelController : Controller
    {
        //private readonly int MAX_BYTES = 10 * 1024 * 1024;
        //private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".xlsx", ".xlsm", ".xlsb", ".xltx",
        //".xltm",".xls",".xlt",".xls",".xml",".xlam",".xla",".xlw",".xlr"};
        private IHostingEnvironment host;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        private IExcelRepository excelRepository;
        private IStudentRepository studentRepository;
        private ILecturerRepository lecturerRepository;

        public ExcelController(IHostingEnvironment host, IExcelRepository excelRepository,
            IStudentRepository studentRepository, ILecturerRepository lecturerRepository,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.host = host;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.excelRepository = excelRepository;
            this.studentRepository = studentRepository;
            this.lecturerRepository = lecturerRepository;
        }

    }
}