using System;
using System.Threading.Tasks;
using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using PMS.Data;
using PMS.Models;
using PMS.Persistence;
using PMS.Persistence.IRepository;
using PMS.Resources;
using PMS.Services;

namespace PMS.Controllers
{
    // [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class RolesController : Controller
    {

        private readonly IMapper mapper;
        private readonly IRoleRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IEmailSender emailSender;
        private readonly IConfiguration config;

        public RolesController(IConfiguration config, IEmailSender emailSender, IMapper mapper, IRoleRepository repository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.emailSender = emailSender;
            this.config = config;
            this.unitOfWork = unitOfWork;
        }

        [Route("getall")]
        public async Task<IActionResult> GetAll()
        {
            var roles = await repository.GetRoles();
            return Ok(roles);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] RoleResource roleResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = mapper.Map<RoleResource, ApplicationRole>(roleResource);

            repository.AddRole(role);
            await unitOfWork.Complete();

            var result = mapper.Map<ApplicationRole, RoleResource>(role);
            //await _emailSender.SendEmailAsync("quanhmp@gmail.com", "Text send mail",
            //                   $"Hello The World");
            return Ok(result);
        }

        [Route("mail")]
        public void TestAction()
        {
            try
            {
                string FromAddress = "quanhmp@gmail.com";
                string FromAdressTitle = "Email from PMS!";
                //To Address  
                string ToAddress = "quan.huynh.k3set@eiu.edu.vn";
                string ToAdressTitle = "Microsoft ASP.NET Core";
                string Subject = "Notifications from Admin";
                string BodyContent = "ASP.NET Core was previously called ASP.NET 5. It was renamed in January 2016. It supports cross-platform frameworks ( Windows, Linux, Mac ) for building modern cloud-based internet-connected applications like IOT, web apps, and mobile back-end.";

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

    [HttpGet]
    [Route("getrole/{id}")]
    public async Task<IActionResult> GetRole(string id)
    {
        var role = await repository.GetRole(id);

        if (role == null)
        {
            return NotFound();
        }

        var roleResource = mapper.Map<ApplicationRole, RoleResource>(role);
        return Ok(roleResource);
    }



    [HttpPut]
    [Route("update/{id}")]
    public async Task<IActionResult> Update(string id, [FromBody]RoleResource roleResource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var role = await repository.GetRole(id);

        if (role == null)
            return NotFound();

        mapper.Map<RoleResource, ApplicationRole>(roleResource, role);
        await unitOfWork.Complete();

        var result = mapper.Map<ApplicationRole, RoleResource>(role);
        return Ok(result);
    }

    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var role = await repository.GetRole(id);

        if (role == null)
        {
            return NotFound();
        }

        repository.RemoveRole(role);
        await unitOfWork.Complete();

        return Ok(id);
    }
}
}