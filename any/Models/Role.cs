using System.ComponentModel.DataAnnotations;

namespace any.Models
{
    public class Role : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public ICollection<Ability> Abilities { get; set; }
    }
}
