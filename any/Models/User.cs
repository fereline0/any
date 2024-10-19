using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace any.Models
{
    [Index(nameof(Name), IsUnique = true)]
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
