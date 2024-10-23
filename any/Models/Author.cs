namespace any.Models
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string[]? Images { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
