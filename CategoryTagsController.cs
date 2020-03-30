using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gIdeas.Models;

namespace gIdeas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryTagsController : ControllerBase
    {
        private readonly gAppDbContext _context;

        public CategoryTagsController(gAppDbContext context)
        {
            _context = context;
        }

        // GET: api/CategoryTags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<gCategoryTag>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/CategoryTags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<gCategoryTag>> GetgCategoryTag(int id)
        {
            var gCategoryTag = await _context.Categories.FindAsync(id);

            if (gCategoryTag == null)
            {
                return NotFound();
            }

            return gCategoryTag;
        }

        // PUT: api/CategoryTags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutgCategoryTag(int id, gCategoryTag gCategoryTag)
        {
            if (id != gCategoryTag.Id)
            {
                return BadRequest();
            }

            _context.Entry(gCategoryTag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!gCategoryTagExists(id))
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

        // POST: api/CategoryTags
        [HttpPost]
        public async Task<ActionResult<gCategoryTag>> PostgCategoryTag(gCategoryTag gCategoryTag)
        {
            _context.Categories.Add(gCategoryTag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetgCategoryTag", new { id = gCategoryTag.Id }, gCategoryTag);
        }

        // DELETE: api/CategoryTags/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<gCategoryTag>> DeletegCategoryTag(int id)
        {
            var gCategoryTag = await _context.Categories.FindAsync(id);
            if (gCategoryTag == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(gCategoryTag);
            await _context.SaveChangesAsync();

            return gCategoryTag;
        }

        private bool gCategoryTagExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
