using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using src.Data;
using src.Models;

namespace src.Controllers
{
    [Authorize]
    public class ConfigController : BaseDotnetDeskController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ConfigController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (!this.IsHaveEnoughAccessRight())
            {
                return NotFound();
            }

            ApplicationUser appUser = await _userManager.GetUserAsync(User);
            var orgList = _context.Organization.Where(x => x.organizationOwnerId.Equals(appUser.Id)).ToList();

            return View(orgList);
        }

        public async Task<IActionResult> Organization()
        {
            ApplicationUser appUser = await _userManager.GetUserAsync(User);
            return View(appUser);
        }

        public async Task<IActionResult> AddEditOrganization(Guid id)
        {
            
            if (Guid.Empty == id)
            {
                ApplicationUser appUser = await _userManager.GetUserAsync(User);
                Organization org = new Organization();
                org.organizationOwnerId = appUser.Id;
                return View(org);
            }
            else
            {
                return View(_context.Organization.Where(x => x.organizationId.Equals(id)).FirstOrDefault());
            }

        }

        public async Task<IActionResult> UserProfile()
        {
            ApplicationUser appUser = await _userManager.GetUserAsync(User);
            return View(appUser);
        }

        public async Task<IActionResult> PersonalProfile()
        {
            ApplicationUser appUser = await _userManager.GetUserAsync(User);
            return View(appUser);
        }
    }
}