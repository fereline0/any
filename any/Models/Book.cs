﻿using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace any.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public int AuthorId { get; set; }

        [JsonIgnore]
        public ICollection<Category> Categories { get; set; } = [];
    }
}
