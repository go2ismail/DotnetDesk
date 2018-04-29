using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class Contact : BaseEntity
    {
        public Contact()
        {
            this.thumbUrl = "/images/no-image-available.png";
        }

        public Guid contactId { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string contactName { get; set; }
        [StringLength(200)]
        [Display(Name = "Description")]
        public string description { get; set; }
        [StringLength(255)]
        [Display(Name = "Thumb Url")]
        public string thumbUrl { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Email")]
        public string email { get; set; }
        [StringLength(100)]
        [Display(Name = "Secondary Email")]
        public string secondaryEmail { get; set; }
        [Display(Name = "Phone")]
        [StringLength(20)]
        public string phone { get; set; }
        [Display(Name = "Website")]
        [StringLength(100)]
        public string website { get; set; }
        [Display(Name = "Linkedin")]
        [StringLength(100)]
        public string linkedin { get; set; }

        public string applicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }

        public Guid customerId { get; set; }
        public Customer customer { get; set; }
        

    }
}
