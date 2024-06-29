using Blog_Backend.Data;
using Blog_Backend.Models;
using Microsoft.AspNetCore.Mvc;
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


        public async Task<IEnumerable<Category>> GetAllASync(
            string? query = null,
            string? sortBy = null,
            string? sortDirection = null,
            int? pageNumber = 1,
            int? pageSize = 100)
        {
            var categories = _dataContext.Categories.AsQueryable();

            if (string.IsNullOrWhiteSpace(query) == false)
            {
                categories = categories.Where(x => x.Name.Contains(query));
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;

                    categories = isAsc ? categories.OrderBy(x => x.Name) : categories.OrderByDescending(x => x.Name);
                }

                if (string.Equals(sortBy, "URL", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;

                    categories = isAsc ? categories.OrderBy(x => x.UrlHandle) : categories.OrderByDescending(x => x.UrlHandle);
                }
            }

            var skipResults = (pageNumber - 1) * pageSize;
            categories = categories.Skip(skipResults ?? 0).Take(pageSize ?? 100);

            return await categories.ToListAsync();
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

        public async Task<int> GetCountAsync()
        {
            return await _dataContext.Categories.CountAsync();
        }
    }
}
