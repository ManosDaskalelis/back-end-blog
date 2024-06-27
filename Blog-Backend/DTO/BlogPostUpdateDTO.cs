namespace Blog_Backend.DTO
{
    public class BlogPostUpdateDTO
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? FullContent { get; set; }
        public string? ImageUrl { get; set; }
        public string? UrlHandle { get; set; }
        public DateTime DateCreated { get; set; }
        public string? Author { get; set; }
        public bool IsVisible { get; set; }

        public List<Guid> Categories { get; set; } = new List<Guid>();
    }
}
