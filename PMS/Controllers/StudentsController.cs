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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Controllers
{

    [Route("/api/students")]
    public class StudentsController : Controller
    {
        private IMapper mapper;
        private ApplicationDbContext context;

        public StudentsController(IMapper mapper, ApplicationDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody]StudentResource studentResource)
        {
            var student = mapper.Map<StudentResource, Student>(studentResource);
            context.Students.Add(student);
            await context.SaveChangesAsync();

            var result = mapper.Map<Student, StudentResource>(student);
            return Ok(result);
        }
    }
}
