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
    [Route("/api/councilenrollments")]
    public class CouncilEnrollmentController : Controller
    {
        private IMapper mapper;
        private ICouncilEnrollmentRepository councilEnrollmentRepository;
        private ILecturerRepository lecturerRepository;
        private ICouncilRepository councilRepository;
        private IUnitOfWork unitOfWork;

        public CouncilEnrollmentController(IMapper mapper, IUnitOfWork unitOfWork,
            ICouncilEnrollmentRepository councilEnrollmentRepository,
            ILecturerRepository lecturerRepository, ICouncilRepository councilRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.councilEnrollmentRepository = councilEnrollmentRepository;
            this.lecturerRepository = lecturerRepository;
            this.councilRepository = councilRepository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateCouncilEnrollment([FromBody]CouncilEnrollmentResource councilEnrollmentResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var councilEnrollment = mapper.Map<CouncilEnrollmentResource, CouncilEnrollment>(councilEnrollmentResource);
            councilEnrollment.Lecturer = await lecturerRepository.GetLecturer(councilEnrollmentResource.LecturerID);
            councilEnrollment.Council = await councilRepository.GetCouncil(councilEnrollmentResource.CouncilID);
            if (councilEnrollment.Score != null)
            {
                councilEnrollment.isMarked = true;
            }

            councilEnrollmentRepository.AddCouncilEnrollment(councilEnrollment);
            await unitOfWork.Complete();

            councilEnrollment = await councilEnrollmentRepository.GetCouncilEnrollment(councilEnrollment.CouncilEnrollmentId);

            var result = mapper.Map<CouncilEnrollment, CouncilEnrollmentResource>(councilEnrollment);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateCouncilEnrollment(int id, [FromBody]CouncilEnrollmentResource councilEnrollmentResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var councilEnrollment = await councilEnrollmentRepository.GetCouncilEnrollment(id);

            if (councilEnrollment == null)
                return NotFound();

            mapper.Map<CouncilEnrollmentResource, CouncilEnrollment>(councilEnrollmentResource, councilEnrollment);
            councilEnrollment.Lecturer = await lecturerRepository.GetLecturer(councilEnrollmentResource.LecturerID);
            councilEnrollment.Council = await councilRepository.GetCouncil(councilEnrollmentResource.CouncilID);
            if (councilEnrollment.Score != null)
            {
                councilEnrollment.isMarked = true;
            }
            await unitOfWork.Complete();

            var result = mapper.Map<CouncilEnrollment, CouncilEnrollmentResource>(councilEnrollment);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteCouncilEnrollment(int id)
        {
            var councilEnrollment = await councilEnrollmentRepository.GetCouncilEnrollment(id, includeRelated: false);

            if (councilEnrollment == null)
            {
                return NotFound();
            }

            councilEnrollmentRepository.RemoveCouncilEnrollment(councilEnrollment);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getcouncilenrollment/{id}")]
        public async Task<IActionResult> GetCouncilEnrollment(int id)
        {
            var councilEnrollment = await councilEnrollmentRepository.GetCouncilEnrollment(id);

            if (councilEnrollment == null)
            {
                return NotFound();
            }

            var councilEnrollmentResource = mapper.Map<CouncilEnrollment, CouncilEnrollmentResource>(councilEnrollment);

            return Ok(councilEnrollmentResource);
        }

        [HttpGet]
        [Route("getcouncilenrollmentsbylectureremail/{email}")]
        public async Task<IActionResult> GetcouncilenrollmentsByLecturerEmail(string email)
        {
            var councilEnrollments = await councilEnrollmentRepository.GetCouncilEnrollmentsByLecturerEmail(email);
            if (councilEnrollments == null)
            {
                return NotFound();
            }

            return Ok(councilEnrollments);
        }

        [HttpGet]
        [Route("getcouncilenrollmentbylectureremail/{email}")]
        public async Task<IActionResult> GetCouncilEnrollmentByLecturerEmail(string email, [FromBody]CouncilResource councilResource)
        {
            var councilEnrollment = await councilEnrollmentRepository.GetCouncilEnrollmentByLecturerEmail(email, councilResource);
            if (councilEnrollment == null)
            {
                return NotFound();
            }
            var councilEnrollmentResource = mapper.Map<CouncilEnrollment, CouncilEnrollmentResource>(councilEnrollment);

            return Ok(councilEnrollmentResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<QueryResultResource<CouncilEnrollmentResource>> GetCouncilEnrollments(QueryResource queryResource)
        {
            var query = mapper.Map<QueryResource, Query>(queryResource);

            var queryResult = await councilEnrollmentRepository.GetCouncilEnrollments(query);
            return mapper.Map<QueryResult<CouncilEnrollment>, QueryResultResource<CouncilEnrollmentResource>>(queryResult);
        }

        [HttpGet]
        [Route("getcouncilenrollmentsbycouncilid/{id}")]
        public async Task<IActionResult> GetCouncilEnrollmentsByCouncilId(int id)
        {
            var councilEnrollments = await councilEnrollmentRepository.GetCouncilEnrollmentsByCouncilId(id);
            var councilEnrollmentResource = mapper.Map<IEnumerable<CouncilEnrollment>, IEnumerable<CouncilEnrollmentResource>>(councilEnrollments);
            return Ok(councilEnrollmentResource);
        }
    }
}
