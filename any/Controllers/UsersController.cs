﻿using System;
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
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers(int page, int limit)
        {
            var total = await _context.Author.CountAsync();
            int skip = (page - 1) * limit;

            var users = await _context
                .User.Select(u => new UserDTO
                {
                    Id = u.Id,
                    Login = u.Login,
                    Name = u.Name,
                    Role = u.Role,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt,
                })
                .Take(total)
                .Skip(skip)
                .ToListAsync();

            var res = new PagedResultDTO<UserDTO>(total, users);

            return Ok(res);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _context
                .User.Where(u => u.Id == id)
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    Login = u.Login,
                    Name = u.Name,
                    Role = u.Role,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt,
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/{id}/Cart
        [HttpGet("{id}/Cart")]
        public async Task<ActionResult<PagedResultDTO<Cart>>> GetUserCart(
            int id,
            int page,
            int limit
        )
        {
            var cart = _context.Cart.Where(c => c.UserId == id);

            var total = await cart.CountAsync();
            var skip = (page - 1) * limit;

            var cartItems = await cart.Skip(skip).Take(limit).ToListAsync();

            var result = new PagedResultDTO<Cart>(total, cartItems);

            return Ok(result);
        }

        // GET: api/Users/{userId}/Cart/isCreated?bookId={bookId}
        [HttpGet("{userId}/Cart/isCreated")]
        public async Task<ActionResult<Cart>> GetUserCartIsCreated(int userId, int bookId)
        {
            var cart = await _context
                .Cart.Where(c => c.UserId == userId && c.BookId == bookId)
                .FirstOrDefaultAsync();

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
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
