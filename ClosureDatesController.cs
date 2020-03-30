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
    public class ClosureDatesController : ControllerBase
    {
        private readonly gAppDbContext _context;

        public ClosureDatesController(gAppDbContext context)
        {
            _context = context;
        }

        // GET: api/ClosureDates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<gClosureDates>>> GetClosureDates()
        {
            return await _context.ClosureDates.ToListAsync();
        }

        // GET: api/ClosureDates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<gClosureDates>> GetgClosureDates(int id)
        {
            var gClosureDates = await _context.ClosureDates.FindAsync(id);

            if (gClosureDates == null)
            {
                return NotFound();
            }

            return gClosureDates;
        }

        // PUT: api/ClosureDates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutgClosureDates(int id, gClosureDates gClosureDates)
        {
            if (id != gClosureDates.Id)
            {
                return BadRequest();
            }

            _context.Entry(gClosureDates).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!gClosureDatesExists(id))
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

        // POST: api/ClosureDates
        [HttpPost]
        public async Task<ActionResult<gClosureDates>> PostgClosureDates(gClosureDates gClosureDates)
        {
            _context.ClosureDates.Add(gClosureDates);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetgClosureDates", new { id = gClosureDates.Id }, gClosureDates);
        }

        // DELETE: api/ClosureDates/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<gClosureDates>> DeletegClosureDates(int id)
        {
            var gClosureDates = await _context.ClosureDates.FindAsync(id);
            if (gClosureDates == null)
            {
                return NotFound();
            }

            _context.ClosureDates.Remove(gClosureDates);
            await _context.SaveChangesAsync();

            return gClosureDates;
        }

        private bool gClosureDatesExists(int id)
        {
            return _context.ClosureDates.Any(e => e.Id == id);
        }
    }
}
