using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using any.Data;
using any.DTO;
using any.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace any.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly anyContext _context;

        public AuthorsController(anyContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<PagedResultDTO<Author>>> GetAuthor(int page, int limit)
        {
            var total = await _context.Author.CountAsync();
            int skip = (page - 1) * limit;

            var authors = await _context.Author.Take(limit).Skip(skip).ToListAsync();

            var res = new PagedResultDTO<Author>(total, authors);

            return Ok(res);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _context.Author.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        // GET: api/Authors/5/Books
        [HttpGet("{id}/Books")]
        public async Task<ActionResult<PagedResultDTO<BookDTO>>> GetBooksByAuthorId(
            int id,
            int page,
            int limit
        )
        {
            var author = await _context.Author.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            var total = await _context.Book.Where(book => book.AuthorId == id).CountAsync();

            var skip = (page - 1) * limit;

            var books = await _context
                .Book.Include(b => b.Categories)
                .Where(book => book.AuthorId == id)
                .Skip(skip)
                .Take(limit)
                .Select(b => new BookDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Price = b.Price,
                    Image = b.Image,
                    AuthorId = b.AuthorId,
                    CategoryIds = b.Categories.Select(c => c.Id).ToList(),
                    CreatedAt = b.CreatedAt,
                    UpdatedAt = b.UpdatedAt,
                })
                .ToListAsync();

            var result = new PagedResultDTO<BookDTO>(total, books);
            return Ok(result);
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Author.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Author.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return _context.Author.Any(e => e.Id == id);
        }
    }
}
