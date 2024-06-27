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
            return await _dataContext.BlogPosts.Include(x => x.Categories).ToListAsync();
        }

        public async Task<BlogPost?> GetBlogPostByIdAsync(Guid id)
        {
            return await _dataContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<BlogPost?> UpdateBlogPostAsync(BlogPost blogPost)
        {
            var existingBlogPost = await _dataContext.BlogPosts.Include(x => x.Categories)
                 .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlogPost is null)
            {
                return null;
            }

            _dataContext.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);
            existingBlogPost.Categories = blogPost.Categories;
            await _dataContext.SaveChangesAsync();

            return existingBlogPost;
        }
        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var blogPost = await _dataContext.Set<BlogPost>().FindAsync(id);

            if (blogPost is not null)
            {
                _dataContext.Set<BlogPost>().Remove(blogPost);
                await _dataContext.SaveChangesAsync();
                return blogPost;
            }
            return null;

        }
    }
}
