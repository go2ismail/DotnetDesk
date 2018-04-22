using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Enum
{
    public enum CustomerType
    {
        Other = 0,
        [Display(Name = "Small Business")]
        SmallBusiness = 1,
        Enterprise = 2,
        Government = 3,
        NGO = 4,
        Internal = 5
    }
}
