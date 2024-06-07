using Blog_Backend.Models;

namespace Blog_Backend.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> AddAsync(Category category);
        Task<IEnumerable<Category>> GetAllASync();
        Task<Category?> GetCategoryByIdASync(Guid id);
        Task<Category?> UpdateCategoryAsync(Category category);
        Task<Category?> DeleteAsync(Guid id);

    }
}
