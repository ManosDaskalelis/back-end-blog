using Blog_Backend.Data;
using Blog_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog_Backend.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _dataContext;

        public CategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Category> AddAsync(Category category)
        {
            await _dataContext.Categories.AddAsync(category);
            await _dataContext.SaveChangesAsync();

            return category;
        }


        public async Task<IEnumerable<Category>> GetAllASync()
        {
            return await _dataContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdASync(Guid id)
        {
            return await _dataContext.Set<Category>().FindAsync(id);
        }

        public async Task<Category?> UpdateCategoryAsync(Category category)
        {
            _dataContext.Entry(category).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();

            return category;
        }
        public async Task<Category?> DeleteAsync(Guid id)
        {
            var category = await _dataContext.Set<Category>().FindAsync(id);
            if (category is null)
            {
                return null;
            }
            _dataContext.Set<Category>().Remove(category);
            await _dataContext.SaveChangesAsync();
            return category;
        }
    }
}
