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

namespace PMS.Controllers
{
    [Route("/api/announcements")]
    public class AnnouncementController : Controller
    {
        private IAnnouncementRepository repository;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;

        public AnnouncementController(IMapper mapper, IUnitOfWork unitOfWork, IAnnouncementRepository repository)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateAnnouncement([FromBody]AnnouncementResource announcementResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var announcement = mapper.Map<AnnouncementResource, Announcement>(announcementResource);

            repository.AddAnnouncement(announcement);
            await unitOfWork.Complete();

            announcement = await repository.GetAnnouncement(announcement.AnnouncementId);

            var result = mapper.Map<Announcement, AnnouncementResource>(announcement);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateAnnouncement(int id, [FromBody]AnnouncementResource announcementResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var announcement = await repository.GetAnnouncement(id);

            if (announcement == null)
                return NotFound();

            mapper.Map<AnnouncementResource, Announcement>(announcementResource, announcement);
            await unitOfWork.Complete();

            var result = mapper.Map<Announcement, AnnouncementResource>(announcement);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            var grade = await repository.GetAnnouncement(id, includeRelated: false);

            if (grade == null)
            {
                return NotFound();
            }

            repository.RemoveAnnouncement(grade);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getannouncement/{id}")]
        public async Task<IActionResult> GetAnnouncement(int id)
        {
            var announcement = await repository.GetAnnouncement(id);

            if (announcement == null)
            {
                return NotFound();
            }

            var announcementResource = mapper.Map<Announcement, AnnouncementResource>(announcement);

            return Ok(announcementResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<QueryResultResource<AnnouncementResource>> GetAnnouncements(QueryResource queryResource)
        {
            var query = mapper.Map<QueryResource, Query>(queryResource);
            var queryResult = await repository.GetAnnouncements(query);

            return mapper.Map<QueryResult<Announcement>, QueryResultResource<AnnouncementResource>>(queryResult);
        }
    }
}