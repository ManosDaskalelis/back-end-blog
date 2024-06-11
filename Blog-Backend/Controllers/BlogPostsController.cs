using Blog_Backend.DTO;
using Blog_Backend.Models;
using Blog_Backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            _blogPostRepository = blogPostRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpPost("AddBlogPosts")]
        public async Task<IActionResult> CreateBlogPost(BlogPostAddDTO addDTO)
        {
            var blogPost = new BlogPost
            {
                Author = addDTO.Author,
                FullContent = addDTO.FullContent,
                ImageUrl = addDTO.ImageUrl,
                IsVisible = addDTO.IsVisible,
                DateCreated = addDTO.DateCreated,
                Content = addDTO.Content,
                Title = addDTO.Title,
                UrlHandle = addDTO.UrlHandle,
                Categories = new List<Category>()
            };

            foreach (var category in addDTO.Categories)
            {
                var existingCategory = await _categoryRepository.GetCategoryByIdASync(category);

                if (existingCategory is not null) 
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }

            blogPost = await _blogPostRepository.AddAsync(blogPost);

            var response = new BlogPostReadOnlyDTO
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                FullContent = blogPost.FullContent,
                ImageUrl = blogPost.ImageUrl,
                IsVisible = blogPost.IsVisible,
                DateCreated = blogPost.DateCreated,
                Content = blogPost.Content,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                Categories = blogPost.Categories.Select(x => new CategoryReadOnlyDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };
            return Ok(response);
        }

        [HttpGet("GetBlogPosts")]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogPosts = await _blogPostRepository.GetAllAsync();

            var response = new List<BlogPostReadOnlyDTO>();
            foreach (var blogPost in blogPosts)
            {
                response.Add(new BlogPostReadOnlyDTO
                {
                    Id = blogPost.Id,
                    Author = blogPost.Author,
                    FullContent = blogPost.FullContent,
                    ImageUrl = blogPost.ImageUrl,
                    IsVisible = blogPost.IsVisible,
                    DateCreated = blogPost.DateCreated,
                    Content = blogPost.Content,
                    Title = blogPost.Title,
                    UrlHandle = blogPost.UrlHandle,
                });
            }
            return Ok(response);
        }
    }
}
