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
            var blogpost = new BlogPost
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
                    blogpost.Categories.Add(existingCategory);
                }
            }

            blogpost = await _blogPostRepository.AddAsync(blogpost);

            var response = new BlogPostReadOnlyDTO
            {
                Id = blogpost.Id,
                Author = blogpost.Author,
                FullContent = blogpost.FullContent,
                ImageUrl = blogpost.ImageUrl,
                IsVisible = blogpost.IsVisible,
                DateCreated = blogpost.DateCreated,
                Content = blogpost.Content,
                Title = blogpost.Title,
                UrlHandle = blogpost.UrlHandle,
                Categories = blogpost.Categories.Select(x => new CategoryReadOnlyDTO
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
                    Categories = blogPost.Categories.Select(x => new CategoryReadOnlyDTO
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle,
                    }).ToList()
                });
            }
            return Ok(response);
        }

        [HttpGet("GetBlogPost/{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById(Guid id)
        {
            var blogPost = await _blogPostRepository.GetBlogPostByIdAsync(id);

            if (blogPost is null)
            {
                return NotFound();
            }

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
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPut("UpdateBlogPost/{id:Guid}")]
        public async Task<IActionResult> UpdateBlogPostById(Guid id, BlogPostUpdateDTO updateDTO)
        {
            var blogpost = new BlogPost
            {
                Id = id,
                Author = updateDTO.Author,
                FullContent = updateDTO.FullContent,
                ImageUrl = updateDTO.ImageUrl,
                IsVisible = updateDTO.IsVisible,
                DateCreated = updateDTO.DateCreated,
                Content = updateDTO.Content,
                Title = updateDTO.Title,
                UrlHandle = updateDTO.UrlHandle,
                Categories = new List<Category>()
            };

            foreach (var category in updateDTO.Categories)
            {
               var existingCategory = await _categoryRepository.GetCategoryByIdASync(category);
               
                if (existingCategory != null) 
                {
                    blogpost.Categories.Add(existingCategory);
                }
            }

           var updatedBlogPost = await _blogPostRepository.UpdateBlogPostAsync(blogpost);

            if (updatedBlogPost is null)
            {
                return NotFound();
            }

            var response = new BlogPostReadOnlyDTO
            {
                Id = blogpost.Id,
                Author = blogpost.Author,
                FullContent = blogpost.FullContent,
                ImageUrl = blogpost.ImageUrl,
                IsVisible = blogpost.IsVisible,
                DateCreated = blogpost.DateCreated,
                Content = blogpost.Content,
                Title = blogpost.Title,
                UrlHandle = blogpost.UrlHandle,
                Categories = blogpost.Categories.Select(x => new CategoryReadOnlyDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };
            
            return Ok(response);
        }

        [HttpDelete("DeleteBlogPost/{id:Guid}")]
        public async Task<IActionResult> DeleteBlogPost(Guid id)
        {
            var blogPost = await _blogPostRepository.DeleteAsync(id);

            if (blogPost is null)
            {
                return NotFound();
            }

            var response = new BlogPostReadOnlyDTO{
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
    }
}
