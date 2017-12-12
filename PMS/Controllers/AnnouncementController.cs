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
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace PMS.Controllers
{
    [Route("/api/announcements")]
    public class AnnouncementController : Controller
    {
        private IAnnouncementRepository repository;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        private readonly IConfiguration config;


        public AnnouncementController(IConfiguration config, IMapper mapper, IUnitOfWork unitOfWork, IAnnouncementRepository repository)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.config = config;
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

            await SendMailAsync(announcement.Content);

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

        public async Task SendMailAsync(String bodyContent)
        {
            var users = await repository.GetAllUsers();
            
            foreach (var user in users)
            {
                try
                {
                    string FromAddress = "quanhmp@gmail.com";
                    string FromAdressTitle = "Email from PMS!";
                    //To Address  
                    string ToAddress = user.Email;
                    string ToAdressTitle = "PMS!";
                    string Subject = "Notifications from Admin";
                    string BodyContent = bodyContent;
                    //Smtp Server  
                    string SmtpServer = this.config["EmailSettings:Server"];
                    //Smtp Port Number  
                    int SmtpPortNumber = Int32.Parse(this.config["EmailSettings:Port"]);

                    var mimeMessage = new MimeMessage();
                    mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));
                    mimeMessage.To.Add(new MailboxAddress(ToAdressTitle, ToAddress));
                    mimeMessage.Subject = Subject;
                    mimeMessage.Body = new TextPart("plain")
                    {
                        Text = BodyContent
                    };

                    using (var client = new SmtpClient())
                    {

                        client.Connect(SmtpServer, SmtpPortNumber, false);
                        // Note: only needed if the SMTP server requires authentication  
                        // Error 5.5.1 Authentication   
                        client.Authenticate(this.config["EmailSettings:Email"], this.config["EmailSettings:Password"]);
                        client.Send(mimeMessage);
                        client.Disconnect(true);

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}