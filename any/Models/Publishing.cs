namespace any.Models
{
    public class Publishing : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
