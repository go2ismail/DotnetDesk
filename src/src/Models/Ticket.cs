using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class Ticket : BaseEntity
    {
        public Ticket()
        {
            this.ticketStatus = Enum.TicketStatus.Unassigned;
            this.ticketType = Enum.TicketType.Problem;
            this.ticketPriority = Enum.TicketPriority.Low;
        }

        public Guid ticketId { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Ticket Title")]
        public string ticketName { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Problem Description")]
        public string description { get; set; }

        [Display(Name = "Customer ID")]
        public Guid customerId { get; set; }

        [Display(Name = "Contact ID")]
        public Guid contactId { get; set; }

        [StringLength(100)]
        [Display(Name = "Email")]
        public string email { get; set; }
        [StringLength(20)]
        [Display(Name = "Phone")]
        public string phone { get; set; }

        [Display(Name = "Status")]
        public Enum.TicketStatus ticketStatus { get; set; }

        [Display(Name = "Ticket Owner ID")]
        public Guid supportAgentId { get; set; }

        [Display(Name = "Support Enggineer ID")]
        public Guid supportEngineerId { get; set; }

        [Display(Name = "Product ID")]
        public Guid productId { get; set; }

        [Display(Name = "Ticket Type")]
        public Enum.TicketType ticketType { get; set; }
        [Display(Name = "Ticket Priority")]
        public Enum.TicketPriority ticketPriority { get; set; }
        [Display(Name = "Ticket Channel")]
        public Enum.TicketChannel ticketChannel { get; set; }

        public Guid organizationId { get; set; }
        public Organization organization { get; set; }

    }
}
