using System.ComponentModel.DataAnnotations;

namespace any.Models
{
    public class Cart : BaseEntity
    {
        public int UserId { get; set; }

        public int BookId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
