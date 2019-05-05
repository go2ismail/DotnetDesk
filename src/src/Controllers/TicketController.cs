using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                ticket.CreateBy = _userManager.GetUserName(User);
                ticket.organizationId = org;

                IList<Product> products = _context.Product.Where(x => x.organizationId.Equals(org)).ToList();
                ViewBag.productId = new SelectList(products, "productId", "productName");

                IList<SupportAgent> agents = _context.SupportAgent.Where(x => x.organizationId.Equals(org)).ToList();
                ViewBag.supportAgentId = new SelectList(agents, "supportAgentId", "supportAgentName");

                IList<SupportEngineer> engineers = _context.SupportEngineer.Where(x => x.organizationId.Equals(org)).ToList();
                ViewBag.supportEngineerId = new SelectList(engineers, "supportEngineerId", "supportEngineerName");

                IList<Contact> contacts = _context.Contact
                    .Where(x => x.customer.organizationId.Equals(org)).ToList();
                ViewBag.contactId = new SelectList(contacts, "contactId", "contactName");

                return View(ticket);
            }
            else
            {
                Ticket ticket = _context.Ticket.Where(x => x.ticketId.Equals(id)).FirstOrDefault();

                IList<Product> products = _context.Product.Where(x => x.organizationId.Equals(ticket.organizationId)).ToList();
                ViewBag.productId = new SelectList(products, "productId", "productName", ticket.productId);

                IList<SupportAgent> agents = _context.SupportAgent.Where(x => x.organizationId.Equals(ticket.organizationId)).ToList();
                ViewBag.supportAgentId = new SelectList(agents, "supportAgentId", "supportAgentName", ticket.supportAgentId);

                IList<SupportEngineer> engineers = _context.SupportEngineer.Where(x => x.organizationId.Equals(ticket.organizationId)).ToList();
                ViewBag.supportEngineerId = new SelectList(engineers, "supportEngineerId", "supportEngineerName", ticket.supportEngineerId);

                IList<Contact> contacts = _context.Contact
                    .Where(x => x.customer.organizationId.Equals(ticket.organizationId)).ToList();
                ViewBag.contactId = new SelectList(contacts, "contactId", "contactName", ticket.contactId);

                return View(ticket);
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

                IList<Product> products = _context.Product.Where(x => x.organizationId.Equals(customer.organizationId)).ToList();
                ViewBag.productId = new SelectList(products, "productId", "productName");

                return View(ticket);
            }
            else
            {
                Ticket ticket = _context.Ticket.Where(x => x.ticketId.Equals(id)).FirstOrDefault();

                IList<Product> products = _context.Product.Where(x => x.organizationId.Equals(ticket.organizationId)).ToList();
                ViewBag.productId = new SelectList(products, "productId", "productName", ticket.productId);

                return View(ticket);
            }

        }

        public IActionResult Detail(Guid ticketId)
        {
            if(ticketId == Guid.Empty)
            {
                return NotFound();
            }
            Ticket ticket = _context.Ticket.Where(x => x.ticketId.Equals(ticketId)).FirstOrDefault();
            Product product = _context.Product.Where(x => x.productId.Equals(ticket.productId)).FirstOrDefault();
            SupportAgent agent = _context.SupportAgent.Where(x => x.supportAgentId.Equals(ticket.supportAgentId)).FirstOrDefault();
            SupportEngineer engineer = _context.SupportEngineer.Where(x => x.supportEngineerId.Equals(ticket.supportEngineerId)).FirstOrDefault();
            Contact contact = _context.Contact.Where(x => x.contactId.Equals(ticket.contactId)).FirstOrDefault();
            ViewData["ticket"] = ticket.ticketId;
            ViewBag.productName = product.productName;
            ViewBag.supportAgentName = agent.supportAgentName;
            ViewBag.supportEngineerName = engineer.supportEngineerName;
            ViewBag.contactName = contact.contactName;
            return View(ticket);
        }

        public IActionResult AddComment(Guid ticketId)
        {
            if(ticketId  == Guid.Empty)
            {
                return NotFound();
            }
            TicketThread ticketThread = new TicketThread();
            ticketThread.ticketId = ticketId;
            ticketThread.CreateBy = _userManager.GetUserName(User);
            return View(ticketThread);
        }

        public IActionResult ShowComments(Guid ticketId)
        {
            if(ticketId == Guid.Empty)
            {
                return NotFound();
            }
            TicketThread ticketThread = _context.TicketThread.Where(x => x.ticketId.Equals(ticketId)).FirstOrDefault();
            return View(ticketThread);
        }
    }
}