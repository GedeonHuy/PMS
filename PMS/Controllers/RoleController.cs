using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Data;
using PMS.Models;
using PMS.Persistence;
using PMS.Persistence.IRepository;
using PMS.Resources;

namespace PMS.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class RolesController : Controller
    {

        private readonly IMapper mapper;
        private readonly IRoleRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public RolesController(IMapper mapper, IRoleRepository repository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        [Route("getall")]
        public async Task<IActionResult> GetAll()
        {
            var roles = await repository.GetRoles();
            return Ok(roles);
        }


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

            return Ok(result);
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