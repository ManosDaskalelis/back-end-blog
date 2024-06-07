using Blog_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog_Backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories{ get; set; }
    }
}
