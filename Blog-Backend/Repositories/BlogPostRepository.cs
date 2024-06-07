using Blog_Backend.Data;
using Blog_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog_Backend.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly DataContext _dataContext;

        public BlogPostRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await _dataContext.BlogPosts.AddAsync(blogPost);
            await _dataContext.SaveChangesAsync();

            return blogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _dataContext.BlogPosts.ToListAsync();
        }
    }
}
