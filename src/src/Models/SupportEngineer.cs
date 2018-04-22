using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class SupportEngineer
    {
        public int supportEngineerId { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Description")]
        public string supportEngineerName { get; set; }

        public string applicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }

        public Guid organizationId { get; set; }
        public Organization organization { get; set; }
    }
}
