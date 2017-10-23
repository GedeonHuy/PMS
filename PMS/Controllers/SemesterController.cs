using System;
using PMS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using PMS.Persistence.IRepository;
using PMS.Persistence;
using PMS.Resources;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Controllers
{
    [Route("/api/semesters")]
    public class SemesterController : Controller
    {
        private IMapper mapper;
        private ISemesterRepository repository;
        private IUnitOfWork unitOfWork;

        public SemesterController(IMapper mapper, IUnitOfWork unitOfWork, ISemesterRepository repository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateSemester([FromBody]SemesterResource semesterResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var semester = mapper.Map<SemesterResource, Semester>(semesterResource);

            repository.AddSemester(semester);
            await unitOfWork.Complete();

            semester = await repository.GetSemester(semester.SemesterId);

            var result = mapper.Map<Semester, SemesterResource>(semester);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateSemester(int id, [FromBody]SemesterResource semesterResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var semester = await repository.GetSemester(id);

            if (semester == null)
                return NotFound();

            mapper.Map<SemesterResource, Semester>(semesterResource, semester);
            await unitOfWork.Complete();

            var result = mapper.Map<Semester, SemesterResource>(semester);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteSemester(int id)
        {
            var semester = await repository.GetSemester(id, includeRelated: false);

            if (semester == null)
            {
                return NotFound();
            }

            repository.RemoveSemester(semester);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getgrade/{id}")]
        public async Task<IActionResult> Getsemester(int id)
        {
            var semester = await repository.GetSemester(id);

            if (semester == null)
            {
                return NotFound();
            }

            var semesterResource = mapper.Map<Semester, SemesterResource>(semester);

            return Ok(semesterResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetSemesters()
        {
            var semesters = await repository.GetSemesters();
            var semesterResource = mapper.Map<IEnumerable<Semester>, IEnumerable<SemesterResource>>(semesters);
            return Ok(semesterResource);
        }
    }
}
