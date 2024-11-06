namespace any.DTO
{
    public class BookDTO : BaseEntityDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public int AuthorId { get; set; }
        public int PublishingId { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}
