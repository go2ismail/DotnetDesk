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
    [Route("api/Customer")]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet("{organizationId}")]
        public IActionResult GetCustomer([FromRoute]Guid organizationId)
        {
            return Json(new { data = _context.Customer.Where(x => x.organizationId.Equals(organizationId)).ToList() });
        }



        // POST: api/Customer
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (customer.customerId == 0)
            {
                _context.Customer.Add(customer);

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Add new data success." });
            }
            else
            {
                _context.Update(customer);

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Edit data success." });
            }
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.customerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Delete success." });
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.customerId == id);
        }
    }
}