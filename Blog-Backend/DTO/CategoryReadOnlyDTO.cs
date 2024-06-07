namespace Blog_Backend.DTO
{
    public class CategoryReadOnlyDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? UrlHandle { get; set; }
    }
}
