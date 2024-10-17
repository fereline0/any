using System.ComponentModel.DataAnnotations;

namespace any.Models
{
    public class User : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
