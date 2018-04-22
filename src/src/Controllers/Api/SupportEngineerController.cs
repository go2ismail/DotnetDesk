using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;

namespace src.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/SupportEngineer")]
    public class SupportEngineerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupportEngineerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SupportEngineer
        [HttpGet]
        public IEnumerable<SupportEngineer> GetSupportEngineer()
        {
            return _context.SupportEngineer;
        }

        // GET: api/SupportEngineer/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupportEngineer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var supportEngineer = await _context.SupportEngineer.SingleOrDefaultAsync(m => m.supportEngineerId == id);

            if (supportEngineer == null)
            {
                return NotFound();
            }

            return Ok(supportEngineer);
        }

        // PUT: api/SupportEngineer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupportEngineer([FromRoute] int id, [FromBody] SupportEngineer supportEngineer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != supportEngineer.supportEngineerId)
            {
                return BadRequest();
            }

            _context.Entry(supportEngineer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupportEngineerExists(id))
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

        // POST: api/SupportEngineer
        [HttpPost]
        public async Task<IActionResult> PostSupportEngineer([FromBody] SupportEngineer supportEngineer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SupportEngineer.Add(supportEngineer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupportEngineer", new { id = supportEngineer.supportEngineerId }, supportEngineer);
        }

        // DELETE: api/SupportEngineer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupportEngineer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var supportEngineer = await _context.SupportEngineer.SingleOrDefaultAsync(m => m.supportEngineerId == id);
            if (supportEngineer == null)
            {
                return NotFound();
            }

            _context.SupportEngineer.Remove(supportEngineer);
            await _context.SaveChangesAsync();

            return Ok(supportEngineer);
        }

        private bool SupportEngineerExists(int id)
        {
            return _context.SupportEngineer.Any(e => e.supportEngineerId == id);
        }
    }
}