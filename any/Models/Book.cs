using System.Reflection.Metadata;

namespace any.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string[]? Images { get; set; }
        public int AuthorId { get; set; }
        public int PublishingId { get; set; }
        public int CategoryId { get; set; }
    }
}
