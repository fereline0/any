using static any.Models.User;

namespace any.DTO
{
    public class UserDTO : BaseEntityDTO
    {
        public string? Name { get; set; }
        public string Login { get; set; }
        public RoleType Role { get; set; }
    }
}
