using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Data;
using src.Models;

namespace src.Controllers
{
    public class SupportEngineerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupportEngineerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(Guid org)
        {
            if (org == Guid.Empty)
            {
                return NotFound();
            }
            Organization organization = _context.Organization.Where(x => x.organizationId.Equals(org)).FirstOrDefault();
            ViewData["org"] = org;
            return View(organization);
        }

        public IActionResult AddEdit(Guid org, Guid id)
        {
            if (id == Guid.Empty)
            {
                SupportEngineer supportEngineer = new SupportEngineer();
                supportEngineer.organizationId = org;
                return View(supportEngineer);
            }
            else
            {
                return View(_context.SupportEngineer.Where(x => x.supportEngineerId.Equals(id)).FirstOrDefault());
            }

        }
    }
}