using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Data;
using src.Models;

namespace src.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult AddEdit(Guid cust, Guid id)
        {
            if (id == Guid.Empty)
            {
                Contact contact = new Contact();
                contact.customerId = cust;
                return View(contact);
            }
            else
            {
                return View(_context.Contact.Where(x => x.contactId.Equals(id)).FirstOrDefault());
            }

        }
    }
}