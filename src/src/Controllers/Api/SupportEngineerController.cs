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
    [Route("api/SupportEngineer")]
    [Authorize]
    public class SupportEngineerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDotnetdesk _dotnetdesk;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public SupportEngineerController(ApplicationDbContext context,
            IDotnetdesk dotnetdesk,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender)
        {
            _context = context;
            _dotnetdesk = dotnetdesk;
            _userManager = userManager;
            _emailSender = emailSender;
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

            if (supportEngineer.supportEngineerId == Guid.Empty)
            {
                
                try
                {
                    var user = new ApplicationUser { UserName = supportEngineer.Email, Email = supportEngineer.Email, FullName = supportEngineer.supportEngineerName };

                    user.IsSupportEngineer = true;
                    var randomPassword = new Random().Next(0, 999999);
                    var result = await _userManager.CreateAsync(user, randomPassword.ToString());
                    if (result.Succeeded)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);

                        await _emailSender.SendEmailAsync(supportEngineer.Email, "Confirm your email and Registration",
                        $"Your email has been registered. With username: '{supportEngineer.Email}'  and temporary  password: '{randomPassword.ToString()}' .Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>link</a>");

                        supportEngineer.applicationUser = user;
                        Organization org = _context.Organization.Where(x => x.organizationId.Equals(supportEngineer.organizationId)).FirstOrDefault();
                        supportEngineer.organization = org;

                        supportEngineer.supportEngineerId = Guid.NewGuid();
                        _context.SupportEngineer.Add(supportEngineer);

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
                    _context.Update(supportEngineer);

                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Edit data success." });
                }
                catch (Exception ex)
                {

                    return Json(new { success = false, message = ex.Message });
                }


            }
        }

        // DELETE: api/SupportEngineer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupportEngineer([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var supportEngineer = await _context.SupportEngineer.SingleOrDefaultAsync(m => m.supportEngineerId == id);
                if (supportEngineer == null)
                {
                    return NotFound();
                }

                string applicationUserId = supportEngineer.applicationUserId;

                _context.SupportEngineer.Remove(supportEngineer);
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

        private bool SupportEngineerExists(Guid id)
        {
            return _context.SupportEngineer.Any(e => e.supportEngineerId == id);
        }
    }
}