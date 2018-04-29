using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class SupportEngineer
    {
        public Guid supportEngineerId { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string supportEngineerName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string applicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }

        public Guid organizationId { get; set; }
        public Organization organization { get; set; }
    }
}
