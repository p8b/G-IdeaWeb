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
    public class PageViewsController : ControllerBase
    {
        private readonly gAppDbContext _context;

        public PageViewsController(gAppDbContext context)
        {
            _context = context;
        }

        // GET: api/PageViews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<gPageView>>> GetPageViews()
        {
            return await _context.PageViews.ToListAsync();
        }

        // GET: api/PageViews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<gPageView>> GetgPageView(int id)
        {
            var gPageView = await _context.PageViews.FindAsync(id);

            if (gPageView == null)
            {
                return NotFound();
            }

            return gPageView;
        }

        // PUT: api/PageViews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutgPageView(int id, gPageView gPageView)
        {
            if (id != gPageView.Id)
            {
                return BadRequest();
            }

            _context.Entry(gPageView).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!gPageViewExists(id))
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

        // POST: api/PageViews
        [HttpPost]
        public async Task<ActionResult<gPageView>> PostgPageView(gPageView gPageView)
        {
            _context.PageViews.Add(gPageView);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetgPageView", new { id = gPageView.Id }, gPageView);
        }

        // DELETE: api/PageViews/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<gPageView>> DeletegPageView(int id)
        {
            var gPageView = await _context.PageViews.FindAsync(id);
            if (gPageView == null)
            {
                return NotFound();
            }

            _context.PageViews.Remove(gPageView);
            await _context.SaveChangesAsync();

            return gPageView;
        }

        private bool gPageViewExists(int id)
        {
            return _context.PageViews.Any(e => e.Id == id);
        }
    }
}
