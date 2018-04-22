using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Enum
{
    public enum TaskStatus
    {
        [Display(Name = "Not Started")]
        NotStarted = 1,
        Deferred = 2,
        [Display(Name = "In Progress")]
        InProgress = 3,
        Completed = 4
    }
}
