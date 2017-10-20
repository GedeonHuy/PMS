using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using PMS.Persistence;
using PMS.Resources;
using PMS.Models;
using PMS.Data;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty lecturers, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Controllers
{
    [Route("/api/lecturers")]
    public class LecturerController : Controller
    {

        private IMapper mapper;
        private ILecturerRepository repository;
        private IUnitOfWork unitOfWork;
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public LecturerController(ApplicationDbContext context, IMapper mapper, ILecturerRepository repository, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.context = context;
            this.mapper = mapper;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
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

            var user = new ApplicationUser
            {
                FullName = lecturer.Name,
                Email = lecturer.Email,
                UserName = lecturer.Email
            };
            if (RoleExists("Lecturer"))
            {
                //Check Student Existence
                if (!LecturerExists(user.Email))
                {
                    var password = "eiu@123"; // Password Default
                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Lecturer");
                }
                else
                {
                    ModelState.AddModelError("", "Email is registered");
                }
            }
            else
            {
                ModelState.AddModelError("", "'Lecturer' role does not exist");
            }
            repository.AddLecturer(lecturer);
            await unitOfWork.Complete();

            lecturer = await repository.GetLecturer(lecturer.LecturerId);

            var result = mapper.Map<Lecturer, LecturerResource>(lecturer);

            return Ok(result);
        }

        [HttpPut("{id}")]
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
            var lecturerResource = mapper.Map<IEnumerable<Lecturer>, IEnumerable<LecturerResource>>(lecturers);
            return Ok(lecturerResource);
        }

        private bool RoleExists(string roleName)
        {
            return context.ApplicationRole.Any(r => r.Name == roleName);
        }

        private bool LecturerExists(string email)
        {
            return context.Lecturers.Any(e => e.Email == email);
        }
    }
}
