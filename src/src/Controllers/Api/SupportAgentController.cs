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
    [Route("api/SupportAgent")]
    public class SupportAgentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupportAgentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SupportAgent
        [HttpGet]
        public IEnumerable<SupportAgent> GetSupportAgent()
        {
            return _context.SupportAgent;
        }

        // GET: api/SupportAgent/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupportAgent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var supportAgent = await _context.SupportAgent.SingleOrDefaultAsync(m => m.supportAgentId == id);

            if (supportAgent == null)
            {
                return NotFound();
            }

            return Ok(supportAgent);
        }

        // PUT: api/SupportAgent/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupportAgent([FromRoute] int id, [FromBody] SupportAgent supportAgent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != supportAgent.supportAgentId)
            {
                return BadRequest();
            }

            _context.Entry(supportAgent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupportAgentExists(id))
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

        // POST: api/SupportAgent
        [HttpPost]
        public async Task<IActionResult> PostSupportAgent([FromBody] SupportAgent supportAgent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SupportAgent.Add(supportAgent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupportAgent", new { id = supportAgent.supportAgentId }, supportAgent);
        }

        // DELETE: api/SupportAgent/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupportAgent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var supportAgent = await _context.SupportAgent.SingleOrDefaultAsync(m => m.supportAgentId == id);
            if (supportAgent == null)
            {
                return NotFound();
            }

            _context.SupportAgent.Remove(supportAgent);
            await _context.SaveChangesAsync();

            return Ok(supportAgent);
        }

        private bool SupportAgentExists(int id)
        {
            return _context.SupportAgent.Any(e => e.supportAgentId == id);
        }
    }
}