using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Data;
using src.Models;

namespace src.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Config")]
    public class ConfigController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConfigController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Config/ChangePersonalProfile
        [HttpPost("ChangePersonalProfile")]
        public async Task<IActionResult> ChangePersonalProfile([FromBody] ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ApplicationUser appUser = _context.ApplicationUser.Where(x => x.Id.Equals(applicationUser.Id)).FirstOrDefault();
            appUser.FullName = applicationUser.FullName;
            _context.Update(appUser);

            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Edit data success.", appUser });
        }
    }
}