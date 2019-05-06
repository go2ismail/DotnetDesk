using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Services
{
    public class SmtpLocalOptions
    {
        public string localDestination {get; set;}
        public string fromEmail { get; set; }
    }
}
