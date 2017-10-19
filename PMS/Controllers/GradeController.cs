using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using PMS.Persistence.IRepository;
using PMS.Persistence;
using PMS.Resources;
using PMS.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Controllers
{
    [Route("/api/grades")]
    public class GradeController : Controller
    {
        private IMapper mapper;
        private IGradeRepository repository;
        private IUnitOfWork unitOfWork;

        public GradeController(IMapper mapper, IUnitOfWork unitOfWork, IGradeRepository repository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGrade([FromBody]GradeResource gradeResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var grade = mapper.Map<GradeResource, Grade>(gradeResource);

            repository.AddGrade(grade);
            await unitOfWork.Complete();

            grade = await repository.GetGrade(grade.GradeId);

            var result = mapper.Map<Grade, GradeResource>(grade);

            return Ok(result);
        }

        [HttpPut("{id}")] /*/api/Grade/id*/
        public async Task<IActionResult> UpdateGrade(int id, [FromBody]GradeResource gradeResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var grade = await repository.GetGrade(id);

            if (grade == null)
                return NotFound();

            mapper.Map<GradeResource, Grade>(gradeResource, grade);
            await unitOfWork.Complete();

            var result = mapper.Map<Grade, GradeResource>(grade);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            var grade = await repository.GetGrade(id, includeRelated: false);

            if (grade == null)
            {
                return NotFound();
            }

            repository.RemoveGrade(grade);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGrade(int id)
        {
            var grade = await repository.GetGrade(id);

            if (grade == null)
            {
                return NotFound();
            }

            var gradeResource = mapper.Map<Grade, GradeResource>(grade);

            return Ok(gradeResource);
        }

        [HttpGet]
        public async Task<IActionResult> GetGrades()
        {
            var grades = await repository.GetGrades();
            return Ok(grades);
        }
    }
}
