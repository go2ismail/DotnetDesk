using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Data;
using src.Models;

namespace src.Controllers
{
    public class ConfigController : BaseDotnetDeskController
    {
        private readonly ApplicationDbContext _context;

        public ConfigController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (!this.IsHaveEnoughAccessRight())
            {
                return NotFound();
            }
            return View(_context.Organization.ToList());
        }

        public IActionResult Organization()
        {
            return View();
        }

        public IActionResult AddEditOrganization(Guid id)
        {
            if (Guid.Empty == id)
            {
                return View(new Organization());
            }
            else
            {
                return View(_context.Organization.Where(x => x.organizationId.Equals(id)).FirstOrDefault());
            }

        }
    }
}