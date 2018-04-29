using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using src.Data;
using src.Models;

namespace src.Controllers
{
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        public IActionResult Customer(Guid cust)
        {
            if (cust == Guid.Empty)
            {
                return NotFound();
            }
            Customer customer = _context.Customer.Where(x => x.customerId.Equals(cust)).FirstOrDefault();
            ViewData["cust"] = cust;
            return View(customer);
        }

        public IActionResult AddEdit(Guid org, Guid id)
        {
            if (id == Guid.Empty)
            {
                Ticket ticket = new Ticket();
                ticket.organizationId = org;
                return View(ticket);
            }
            else
            {
                return View(_context.Ticket.Where(x => x.ticketId.Equals(id)).FirstOrDefault());
            }

        }

        public IActionResult AddEditCustomerTicket(Guid cust, Guid id)
        {
            if (id == Guid.Empty)
            {
                Customer customer = _context.Customer.Where(x => x.customerId.Equals(cust)).FirstOrDefault();
                var applicationUserId = _userManager.GetUserId(User);
                Contact contact = _context.Contact.Where(x => x.applicationUserId.Equals(applicationUserId)).FirstOrDefault();
                Ticket ticket = new Ticket();
                ticket.customerId = cust;
                ticket.organizationId = customer.organizationId;
                ticket.contactId = contact.contactId;
                return View(ticket);
            }
            else
            {
                return View(_context.Ticket.Where(x => x.ticketId.Equals(id)).FirstOrDefault());
            }

        }
    }
}