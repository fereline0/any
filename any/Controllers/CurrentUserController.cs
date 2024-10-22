using System.Security.Claims;
using any.Data;
using any.DTO;
using any.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace any.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CurrentUserController : ControllerBase
    {
        private readonly anyContext _context;

        public CurrentUserController(anyContext context)
        {
            _context = context;
        }

        // GET: api/UserProfile
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim.Value);
            User? user = await _context.User.FindAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            UserDTO userDto =
                new()
                {
                    Id = user.Id,
                    Login = user.Login,
                    Name = user.Name,
                    Role = user.Role.ToString(),
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                };

            return Ok(userDto);
        }
    }
}
