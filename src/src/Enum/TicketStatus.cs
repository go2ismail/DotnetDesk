using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Enum
{
    public enum TicketStatus
    {
        Open = 1,
        [Display(Name = "On Hold")]
        OnHold = 2,
        Escalated = 3,
        Closed = 4
    }
}
