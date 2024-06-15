using CodePulse.API.Data;
using CodePulse.API.Model.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> UpdateCategory(Category category)
        {
            var filteredCategory = await dbContext.Categories.FirstOrDefaultAsync(x =>  x.Id == category.Id);
            if (filteredCategory != null)
            {
                dbContext.Entry(filteredCategory).CurrentValues.SetValues(category);
                await dbContext.SaveChangesAsync();
                return category;
            }

            return null;
        }
        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var filteredItem = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (filteredItem == null)
            {
                return false;
            }

            dbContext.Categories.Remove(filteredItem);
            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
