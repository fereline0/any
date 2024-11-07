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
    public class BooksController : ControllerBase
    {
        private readonly anyContext _context;

        public BooksController(anyContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<PagedResultDTO<BookDTO>>> GetBook(int page, int limit)
        {
            var total = await _context.Book.CountAsync();
            int skip = (page - 1) * limit;

            var books = await _context
                .Book.Include(b => b.Categories)
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

            var res = new PagedResultDTO<BookDTO>(total, books);
            return Ok(res);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            var book = await _context
                .Book.Include(b => b.Categories)
                .Where(b => b.Id == id)
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
                .FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookDTO bookDto)
        {
            if (id != bookDto.Id)
            {
                return BadRequest();
            }

            var book = await _context
                .Book.Include(b => b.Categories)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            var existingCategories = await _context
                .Category.Where(c => bookDto.CategoryIds.Contains(c.Id))
                .ToListAsync();

            if (existingCategories.Count != bookDto.CategoryIds.Count)
            {
                return BadRequest("Some categories do not exist.");
            }

            book.Title = bookDto.Title;
            book.Description = bookDto.Description;
            book.Price = bookDto.Price;
            book.Image = bookDto.Image;
            book.AuthorId = bookDto.AuthorId;

            book.Categories.Clear();
            foreach (var category in existingCategories)
            {
                book.Categories.Add(category);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookDTO bookDto)
        {
            var existingCategories = await _context
                .Category.Where(c => bookDto.CategoryIds.Contains(c.Id))
                .ToListAsync();

            if (existingCategories.Count != bookDto.CategoryIds.Count)
            {
                return BadRequest("Some categories do not exist.");
            }

            var book = new Book
            {
                Title = bookDto.Title,
                Description = bookDto.Description,
                Price = bookDto.Price,
                Image = bookDto.Image,
                AuthorId = bookDto.AuthorId,
                Categories = existingCategories,
            };

            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
