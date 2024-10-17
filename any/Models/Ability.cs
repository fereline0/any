using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Hosting;

namespace any.Models
{
    public class Ability : BaseEntity
    {
        [Required]
        public string slug { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
