using Blog_Backend.Models;

namespace Blog_Backend.Repositories
{
    public interface IBlogImageRepository
    {
        Task<BlogImage> Upload(IFormFile file, BlogImage blogImage);
        Task<IEnumerable<BlogImage>> GetAllBlogImages();
    }
}
