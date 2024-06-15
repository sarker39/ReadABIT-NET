using CodePulse.API.Model.Domain;
using CodePulse.API.Model.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    //https://localhost:xxxx/api/categories
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryRequestDto request)
        {
            //Map DTO to Domain Model
            var category = new Category
            {
                //entity framework core will generate id as type guid
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };

            this.categoryRepository.CreateCategoryAsync(category);

            //Domain Model to DTO
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };


            return Ok(response);

        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categoryLists = await this.categoryRepository.GetAllCategoriesAsync();

            var response = new List<CategoryDto>();

            foreach(var category in categoryLists)
            {
                response.Add(new CategoryDto
                {
                    Id=category.Id,
                    Name = category.Name,
                    UrlHandle=category.UrlHandle
                });
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var filteredCategory = await this.categoryRepository.GetCategoryByIdAsync(id);

            if(filteredCategory is null)
            {
                return NotFound();
            }

            var response = new Category
            {
                Id = filteredCategory.Id,
                Name = filteredCategory.Name,
                UrlHandle = filteredCategory.UrlHandle
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] CategoryRequestDto request)
        {
            //converting DTO to domain model
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };
            var filteredCategory = await this.categoryRepository.UpdateCategory(category);
            if(filteredCategory is null)
            {
                return BadRequest();
            }
            //converting Domain to DTO

            var response = new Category
            {
                Id = filteredCategory.Id,
                Name = filteredCategory.Name,
                UrlHandle = filteredCategory.UrlHandle
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategoryById([FromRoute] Guid id)
        {
            var filteredCategory = await this.categoryRepository.DeleteCategoryAsync(id);

            if (!filteredCategory)
            {
                return NotFound();
            }

            return Ok(true);
        }
    }
}
