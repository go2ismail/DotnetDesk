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
        [HttpGet("{organizationId}")]
        public IActionResult GetSupportAgent([FromRoute]Guid organizationId)
        {
            return Json(new { data = _context.SupportAgent.Where(x => x.organizationId.Equals(organizationId)).ToList() });
        }

        // POST: api/SupportAgent
        [HttpPost]
        public async Task<IActionResult> PostSupportAgent([FromBody] SupportAgent supportAgent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (supportAgent.supportAgentId == 0)
            {
                _context.SupportAgent.Add(supportAgent);

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Add new data success." });
            }
            else
            {
                _context.Update(supportAgent);

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Edit data success." });
            }
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

            return Json(new { success = true, message = "Delete success." });
        }

        private bool SupportAgentExists(int id)
        {
            return _context.SupportAgent.Any(e => e.supportAgentId == id);
        }
    }
}