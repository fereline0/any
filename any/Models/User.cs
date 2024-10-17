using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace any.Models
{
    public class User : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [JsonIgnore]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}
