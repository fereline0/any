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

        public string Name { get; set; }
        public string Password { get; set; }
        public string? Image { get; set; }
        public RoleType Role { get; set; }
    }
}
