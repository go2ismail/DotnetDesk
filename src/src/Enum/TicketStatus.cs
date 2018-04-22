using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Enum
{
    public enum TicketStatus
    {
        Unassigned = 1,
        Open = 2,
        [Display(Name = "On Hold")]
        OnHold = 3,
        Escalated = 4,
        Closed = 5
    }
}
