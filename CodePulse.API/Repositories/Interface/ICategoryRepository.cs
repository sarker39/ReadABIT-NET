using CodePulse.API.Model.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategoryAsync(Category category);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
}

