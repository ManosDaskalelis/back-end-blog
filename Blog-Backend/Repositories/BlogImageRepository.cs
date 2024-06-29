using Blog_Backend.Data;
using Blog_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog_Backend.Repositories
{
    public class BlogImageRepository : IBlogImageRepository
    {   
        private readonly IWebHostEnvironment _webhostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _dataContext;

        public BlogImageRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            DataContext dataContext)
        {
            _webhostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _dataContext = dataContext;
        }


        public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
        {
            var localPath = Path.Combine(_webhostEnvironment.ContentRootPath, "Images", $"{blogImage.FileName}{blogImage.FileExtension}");

            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            var httpRequest = _httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";

            blogImage.Url = urlPath;

            await _dataContext.BlogImages.AddAsync(blogImage);
            await _dataContext.SaveChangesAsync();

            return blogImage;
        }
        public async Task<IEnumerable<BlogImage>> GetAllBlogImages()
        {
           return await _dataContext.BlogImages.ToListAsync();
        }
    }
}
