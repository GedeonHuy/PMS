using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using PMS.Persistence;
using PMS.Persistence.IRepository;
using PMS.Resources;
using PMS.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Controllers
{
    [Route("/api/majors")]
    public class MajorController : Controller
    {
        private IMapper mapper;
        private IMajorRepository repository;
        private IUnitOfWork unitOfWork;

        public MajorController(IMapper mapper, IUnitOfWork unitOfWork, IMajorRepository repository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateMajor([FromBody]MajorResource majorResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var major = mapper.Map<MajorResource, Major>(majorResource);

            repository.AddMajor(major);
            await unitOfWork.Complete();

            major = await repository.GetMajor(major.MajorId);

            var result = mapper.Map<Major, MajorResource>(major);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateMajor(int id, [FromBody]MajorResource majorResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var major = await repository.GetMajor(id);

            if (major == null)
                return NotFound();

            mapper.Map<MajorResource, Major>(majorResource, major);
            await unitOfWork.Complete();

            var result = mapper.Map<Major, MajorResource>(major);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteMajor(int id)
        {
            var major = await repository.GetMajor(id, includeRelated: false);

            if (major == null)
            {
                return NotFound();
            }

            repository.RemoveMajor(major);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getgrade/{id}")]
        public async Task<IActionResult> GetMajor(int id)
        {
            var major = await repository.GetMajor(id);

            if (major == null)
            {
                return NotFound();
            }

            var majorResource = mapper.Map<Major, MajorResource>(major);

            return Ok(majorResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetMajors()
        {
            var majors = await repository.GetMajors();
            var majorResource = mapper.Map<IEnumerable<Major>, IEnumerable<MajorResource>>(majors);
            return Ok(majorResource);
        }
    }
}
