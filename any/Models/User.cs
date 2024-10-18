using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace any.Models
{
    public class User : BaseEntity
    {
        public enum RoleType
        {
            User,
            Admin,
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public RoleType Role { get; set; }
    }
}
