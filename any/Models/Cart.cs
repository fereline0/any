namespace any.Models
{
    public class Cart : BaseEntity
    {
        public int UserId { get; set; }

        public int BookId { get; set; }
    }
}
