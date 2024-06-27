﻿using Blog_Backend.Models;

namespace Blog_Backend.Repositories
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> AddAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetBlogPostByIdAsync(Guid id);
        Task<BlogPost?> UpdateBlogPostAsync(BlogPost blogPost);
        Task<BlogPost?> DeleteAsync(Guid id);
    }
}
