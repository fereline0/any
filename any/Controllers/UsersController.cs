using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using any.Data;
using any.DTO;
using any.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace any.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly anyContext _context;

        public UsersController(anyContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagedResultDTO<UserDTO>>>> GetUser(
            int page = 1,
            int limit = 20
        )
        {
            var total = await _context.Author.CountAsync();
            var skip = (page - 1) * limit;
            var users = await _context.User.Skip(skip).Take(limit).ToListAsync();

            var userDTO = users
                .Select(user => new UserDTO
                {
                    Id = user.Id,
                    Login = user.Login,
                    Name = user.Name,
                    Role = user.Role.ToString(),
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                })
                .ToList();

            var result = new PagedResultDTO<object>(total, userDTO);
            return Ok(result);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

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

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

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

            return CreatedAtAction("GetUser", new { id = user.Id }, userDto);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
