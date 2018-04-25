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
        [HttpGet("{organizationId}")]
        public IActionResult GetSupportEngineer([FromRoute]Guid organizationId)
        {
            return Json(new { data = _context.SupportEngineer.Where(x => x.organizationId.Equals(organizationId)).ToList() });
        }


        // POST: api/SupportEngineer
        [HttpPost]
        public async Task<IActionResult> PostSupportEngineer([FromBody] SupportEngineer supportEngineer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (supportEngineer.supportEngineerId == 0)
            {
                _context.SupportEngineer.Add(supportEngineer);

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Add new data success." });
            }
            else
            {
                _context.Update(supportEngineer);

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Edit data success." });
            }
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

            return Json(new { success = true, message = "Delete success." });
        }

        private bool SupportEngineerExists(int id)
        {
            return _context.SupportEngineer.Any(e => e.supportEngineerId == id);
        }
    }
}