using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using PMS.Persistence;
using PMS.Resources;
using PMS.Models;

// For more information on enabling MVC for empty lecturers, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Controllers
{
    [Route("/api/lecturers")]
    public class LecturerController : Controller
    {
        private IMapper mapper;
        private ILecturerRepository repository;
        private IUnitOfWork unitOfWork;

        public LecturerController(IMapper mapper, IUnitOfWork unitOfWork, ILecturerRepository repository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
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

            repository.AddLecturer(lecturer);
            await unitOfWork.Complete();

            lecturer = await repository.GetLecturer(lecturer.LecturerId);

            var result = mapper.Map<Lecturer, LecturerResource>(lecturer);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateLecturer(int id, [FromBody]LecturerResource lecturerResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lecturer = await repository.GetLecturer(id);

            if (lecturer == null)
                return NotFound();

            mapper.Map<LecturerResource, Lecturer>(lecturerResource, lecturer);
            await unitOfWork.Complete();

            var result = mapper.Map<Lecturer, LecturerResource>(lecturer);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteLecturer(int id)
        {
            var lecturer = await repository.GetLecturer(id, includeRelated: false);

            if (lecturer == null)
            {
                return NotFound();
            }

            repository.RemoveLecturer(lecturer);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getlecturer/{id}")]
        public async Task<IActionResult> GetLecturer(int id)
        {
            var lecturer = await repository.GetLecturer(id);

            if (lecturer == null)
            {
                return NotFound();
            }

            var lecturerResource = mapper.Map<Lecturer, LecturerResource>(lecturer);

            return Ok(lecturerResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetLecturers()
        {
            var lecturers = await repository.GetLecturers();
            return Ok(lecturers);
        }
    }
}
