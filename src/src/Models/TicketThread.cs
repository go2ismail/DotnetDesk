using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class TicketThread : BaseEntity
    {
        public Guid ticketThreadId { get; set; }
        [Required]
        [StringLength(250)]
        [Display(Name = "Comment")]
        public string Comment { get; set; }
        public Guid ticketId { get; set; }
    }
}
