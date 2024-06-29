using Blog_Backend.Models;

namespace Blog_Backend.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> AddAsync(Category category);
        Task<IEnumerable<Category>> GetAllASync(
            string? query = null,
            string? sortBy = null,
            string? sortDirection = null, 
            int? pageNumber = 1,
            int? pageSize = 100);
        Task<Category?> GetCategoryByIdASync(Guid id);
        Task<Category?> UpdateCategoryAsync(Category category);
        Task<Category?> DeleteAsync(Guid id);

        Task<int> GetCountAsync();

    }
}
