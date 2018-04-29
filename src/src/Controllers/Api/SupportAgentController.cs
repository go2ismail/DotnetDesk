using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;
using src.Services;

namespace src.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/SupportAgent")]
    [Authorize]
    public class SupportAgentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDotnetdesk _dotnetdesk;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public SupportAgentController(ApplicationDbContext context,
            IDotnetdesk dotnetdesk,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender)
        {
            _context = context;
            _dotnetdesk = dotnetdesk;
            _userManager = userManager;
            _emailSender = emailSender;
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

            if (supportAgent.supportAgentId == Guid.Empty)
            {
                try
                {
                    var user = new ApplicationUser { UserName = supportAgent.Email, Email = supportAgent.Email, FullName = supportAgent.supportAgentName };
                    
                    user.IsSupportAgent = true;
                    var randomPassword = new Random().Next(0, 999999);
                    var result = await _userManager.CreateAsync(user, randomPassword.ToString());
                    if (result.Succeeded)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);

                        await _emailSender.SendEmailAsync(supportAgent.Email, "Confirm your email and Registration",
                        $"Your email has been registered. With username:'{supportAgent.Email}'  and temporary  password:'{randomPassword.ToString()}' .Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>link</a>");

                        supportAgent.applicationUser = user;
                        Organization org = _context.Organization.Where(x => x.organizationId.Equals(supportAgent.organizationId)).FirstOrDefault();
                        supportAgent.organization = org;

                        supportAgent.supportAgentId = Guid.NewGuid();
                        _context.SupportAgent.Add(supportAgent);

                        await _context.SaveChangesAsync();

                        return Json(new { success = true, message = "Add new data success." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "UserManager CreateAsync Fail." });
                    }
                    
                }
                catch (Exception ex)
                {

                    return Json(new { success = false, message = ex.Message });
                }

             

               
            }
            else
            {
                try
                {
                    _context.Update(supportAgent);

                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Edit data success." });
                }
                catch (Exception ex)
                {

                    return Json(new { success = false, message = ex.Message });
                }

            }
        }

        // DELETE: api/SupportAgent/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupportAgent([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var supportAgent = await _context.SupportAgent.SingleOrDefaultAsync(m => m.supportAgentId == id);
                if (supportAgent == null)
                {
                    return NotFound();
                }

                string applicationUserId = supportAgent.applicationUserId;

                _context.SupportAgent.Remove(supportAgent);
                await _context.SaveChangesAsync();

                ApplicationUser appUser = await _userManager.FindByIdAsync(applicationUserId);
                await _userManager.DeleteAsync(appUser);

                return Json(new { success = true, message = "Delete success." });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }

           
        }

        private bool SupportAgentExists(Guid id)
        {
            return _context.SupportAgent.Any(e => e.supportAgentId == id);
        }
    }
}