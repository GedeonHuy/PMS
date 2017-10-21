using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PMS.Data;
using PMS.Hubs;

namespace PMS.Controllers
{
    // [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IHubContext<PMSHub> hubContext;
        public ValuesController(ApplicationDbContext context, IHubContext<PMSHub> hubContext)
        {
            this.context = context;
            this.hubContext = hubContext;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var users = context.Users.ToList();
            hubContext.Clients.All.InvokeAsync("Send", "Hello World");
            return Ok(users);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
