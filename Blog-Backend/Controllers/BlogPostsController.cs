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

        public BlogPostsController(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
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
            };
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
