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
    [Route("api/Ticket")]
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Ticket
        [HttpGet("{organizationId}")]
        public IActionResult GetTicket([FromRoute]Guid organizationId)
        {
            return Json(new { data = _context.Ticket.Where(x => x.organizationId.Equals(organizationId)).ToList() });
        }

        // POST: api/Ticket
        [HttpPost]
        public async Task<IActionResult> PostTicket([FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ticket.ticketId == 0)
            {
                _context.Ticket.Add(ticket);

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Add new data success." });
            }
            else
            {
                _context.Update(ticket);

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Edit data success." });
            }
        }

        // DELETE: api/Ticket/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticket = await _context.Ticket.SingleOrDefaultAsync(m => m.ticketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Delete success." });
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.ticketId == id);
        }
    }
}