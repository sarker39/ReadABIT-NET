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
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
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
    }
}
