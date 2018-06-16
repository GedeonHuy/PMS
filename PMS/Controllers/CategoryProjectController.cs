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
    [Route("/api/categoryprojects")]
    public class CategoryProjectController : Controller
    {
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        private ICategoryProjectRepository categoryProjectRepository;
        private IProjectRepository projectRepository;
        private ICategoryRepository categoryRepository;

        public CategoryProjectController(IMapper mapper, IUnitOfWork unitOfWork,
            ICategoryProjectRepository categoryProjectRepository,
            IProjectRepository projectRepository, ICategoryRepository categoryRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.categoryProjectRepository = categoryProjectRepository;
            this.projectRepository = projectRepository;
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateCategoryProject([FromBody]CategoryProjectResource categoryProjectResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryProject = mapper.Map<CategoryProjectResource, CategoryProject>(categoryProjectResource);
            categoryProject.Project = await projectRepository.GetProject(categoryProjectResource.ProjectId);
            categoryProject.Category = await categoryRepository.GetCategory(categoryProjectResource.CategoryId);

            categoryProjectRepository.AddCategoryProject(categoryProject);

            await unitOfWork.Complete();

            categoryProject = await categoryProjectRepository.GetCategoryProject(categoryProject.CategoryProjectId);

            var result = mapper.Map<CategoryProject, CategoryProjectResource>(categoryProject);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateCategoryProject(int id, [FromBody]CategoryProjectResource categoryProjectResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryProject = await categoryProjectRepository.GetCategoryProject(id);

            if (categoryProject == null)
                return NotFound();

            categoryProject.Project = await projectRepository.GetProject(categoryProjectResource.ProjectId);
            categoryProject.Category = await categoryRepository.GetCategory(categoryProjectResource.CategoryId);

            categoryProjectRepository.AddCategoryProject(categoryProject);

            await unitOfWork.Complete();

            categoryProject = await categoryProjectRepository.GetCategoryProject(categoryProject.CategoryProjectId);

            var result = mapper.Map<CategoryProject, CategoryProjectResource>(categoryProject);

            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteCategoryProject(int id)
        {
            var categoryProject = await categoryProjectRepository.GetCategoryProject(id, includeRelated: false);

            if (categoryProject == null)
            {
                return NotFound();
            }

            categoryProjectRepository.RemoveCategoryProject(categoryProject);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getboardenrollment/{id}")]
        public async Task<IActionResult> GetCategoryProject(int id)
        {
            var categoryProject = await categoryProjectRepository.GetCategoryProject(id);

            if (categoryProject == null)
            {
                return NotFound();
            }

            var categoryProjectResource = mapper.Map<CategoryProject, CategoryProjectResource>(categoryProject);

            return Ok(categoryProjectResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<QueryResultResource<CategoryProjectResource>> GetCategoryProjects(QueryResource queryResource)
        {
            var query = mapper.Map<QueryResource, Query>(queryResource);

            var queryResult = await categoryProjectRepository.GetCategoryProjects(query);

            var a = mapper.Map<QueryResult<CategoryProject>, QueryResultResource<CategoryProjectResource>>(queryResult);

            return a;
        }

    }
}
