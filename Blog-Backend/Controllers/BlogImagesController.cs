using Blog_Backend.DTO;
using Blog_Backend.Models;
using Blog_Backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogImagesController : ControllerBase
    {
        private readonly IBlogImageRepository _blogImageRepository;

        public BlogImagesController(IBlogImageRepository blogImageRepository)
        {
            _blogImageRepository = blogImageRepository;
        }

        [HttpPost("AddBlogPostImages")]
        public async Task<IActionResult> AddBlogImage([FromForm] IFormFile file,
            [FromForm] string fileName, [FromForm] string title)
        {
            ValidateFileUpload(file);

            if (ModelState.IsValid)
            {
                var blogImage = new BlogImage
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = title,
                    DateCreated = DateTime.Now
                };

                blogImage = await _blogImageRepository.Upload(file, blogImage);

                var response = new BlogImageReadOnlyDTO
                {
                    Id = blogImage.Id,
                    FileExtension = blogImage.FileExtension,
                    Title = blogImage.Title,
                    DateCreated = blogImage.DateCreated,
                    FileName = blogImage.FileName,
                    Url = blogImage.Url,

                };

                return Ok(response);
            }
            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file format");
            }

            if (file.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size cannot be more than 10MB");
            }
        }

        [HttpGet("GetAllBlogImages")]
        public async Task<IActionResult> GetAllBlogImages()
        {
            var images = await _blogImageRepository.GetAllBlogImages();

            var response = new List<BlogImageReadOnlyDTO>();
            foreach (var image in images)
            {
                response.Add(new BlogImageReadOnlyDTO
                {
                    Id = image.Id,
                    FileExtension = image.FileExtension,
                    Title = image.Title,
                    DateCreated = image.DateCreated,
                    FileName = image.FileName,
                    Url = image.Url,
                });
            }
            return Ok(response);
        }
    }
}
