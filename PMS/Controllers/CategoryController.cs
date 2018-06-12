using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PMS.Models;
using PMS.Models.TaskingModels;
using PMS.Persistence;
using PMS.Persistence.IRepository;
using PMS.Resources;
using PMS.Resources.TaskResources;

namespace PMS.Controllers
{
    [Route("/api/categories")]
    public class CategoryController : Controller
    {
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        private ICategoryRepository categoryRepository;
        private IProjectRepository projectRepository;

        public CategoryController(IMapper mapper, IUnitOfWork unitOfWork,
         ICategoryRepository categoryRepository, IProjectRepository projectRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.categoryRepository = categoryRepository;
            this.projectRepository = projectRepository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateCategory([FromBody]CategoryResource categoryResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = mapper.Map<CategoryResource, Category>(categoryResource);

            category.Project = await projectRepository.GetProject(categoryResource.ProjectId);

            categoryRepository.AddCategory(category);
            await unitOfWork.Complete();

            var result = mapper.Map<Category, CategoryResource>(category);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody]CategoryResource categoryResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await categoryRepository.GetCategory(id);

            if (category == null)
                return NotFound();

            mapper.Map<CategoryResource, Category>(categoryResource, category);

            category.Project = await projectRepository.GetProject(categoryResource.ProjectId);

            await unitOfWork.Complete();

            var result = mapper.Map<Category, CategoryResource>(category);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var category = await categoryRepository.GetCategory(id, includeRelated: false);

            if (category == null)
            {
                return NotFound();
            }

            categoryRepository.RemoveCategory(category);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getcategory/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await categoryRepository.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryResource = mapper.Map<Category, CategoryResource>(category);

            return Ok(categoryResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<QueryResultResource<CategoryResource>> GetCategories(QueryResource queryResource)
        {
            var query = mapper.Map<QueryResource, Query>(queryResource);

            var queryResult = await categoryRepository.GetCategories(query);
            return mapper.Map<QueryResult<Category>, QueryResultResource<CategoryResource>>(queryResult);
        }
    }
}