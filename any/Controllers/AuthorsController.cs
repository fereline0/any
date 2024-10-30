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
    public class AuthorsController : ControllerBase
    {
        private readonly anyContext _context;

        public AuthorsController(anyContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<PagedResultDTO<object>>> GetAuthors(
            int page = 1,
            int limit = 20,
            int bookPage = 1,
            int bookLimit = 20
        )
        {
            var total = await _context.Author.CountAsync();
            var skip = (page - 1) * limit;
            var authors = await _context
                .Author.Skip(skip)
                .Take(limit)
                .Include(c => c.Books)
                .ToListAsync();

            var authorDTO = authors
                .Select(author =>
                {
                    var totalBook = author.Books.Count;
                    var bookSkip = (bookPage - 1) * bookLimit;
                    var pagedBooks = author.Books.Skip(bookSkip).Take(bookLimit).ToList();

                    return new
                    {
                        author.Id,
                        author.Name,
                        author.CreatedAt,
                        author.UpdatedAt,
                        Total = totalBook,
                        Books = new PagedResultDTO<Book>(totalBook, pagedBooks),
                    };
                })
                .ToList();

            var result = new PagedResultDTO<object>(total, authorDTO);
            return Ok(result);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetAuthor(int id, int page = 1, int limit = 20)
        {
            var author = await _context
                .Author.Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            var totalBook = author.Books.Count;
            var skip = (page - 1) * limit;
            var pagedBooks = author.Books.Skip(skip).Take(limit).ToList();

            var result = new
            {
                author.Id,
                author.Name,
                author.CreatedAt,
                author.UpdatedAt,
                Books = new PagedResultDTO<Book>(totalBook, pagedBooks),
            };

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
