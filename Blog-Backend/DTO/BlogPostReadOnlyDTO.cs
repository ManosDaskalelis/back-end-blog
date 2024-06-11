﻿using System.Security.Principal;

namespace Blog_Backend.DTO
{
    public class BlogPostReadOnlyDTO
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? FullContent { get; set; }
        public string? ImageUrl { get; set; }
        public string? UrlHandle { get; set; }
        public DateTime DateCreated { get; set; }
        public string? Author { get; set; }
        public bool IsVisible { get; set; }

        public List<CategoryReadOnlyDTO> Categories { get; set; } = new List<CategoryReadOnlyDTO>();
    }
}
