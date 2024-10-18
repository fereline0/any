using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using any.Data;
using any.Models;

namespace any.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishingsController : ControllerBase
    {
        private readonly anyContext _context;

        public PublishingsController(anyContext context)
        {
            _context = context;
        }

        // GET: api/Publishings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publishing>>> GetPublishing()
        {
            return await _context.Publishing.ToListAsync();
        }

        // GET: api/Publishings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publishing>> GetPublishing(int id)
        {
            var publishing = await _context.Publishing.FindAsync(id);

            if (publishing == null)
            {
                return NotFound();
            }

            return publishing;
        }

        // PUT: api/Publishings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublishing(int id, Publishing publishing)
        {
            if (id != publishing.Id)
            {
                return BadRequest();
            }

            _context.Entry(publishing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublishingExists(id))
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

        // POST: api/Publishings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Publishing>> PostPublishing(Publishing publishing)
        {
            _context.Publishing.Add(publishing);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPublishing", new { id = publishing.Id }, publishing);
        }

        // DELETE: api/Publishings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublishing(int id)
        {
            var publishing = await _context.Publishing.FindAsync(id);
            if (publishing == null)
            {
                return NotFound();
            }

            _context.Publishing.Remove(publishing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PublishingExists(int id)
        {
            return _context.Publishing.Any(e => e.Id == id);
        }
    }
}
