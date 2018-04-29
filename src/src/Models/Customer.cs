using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class Customer : BaseEntity
    {
        public Customer()
        {
            this.thumbUrl = "/images/no-image-available.png";
            this.customerType = Enum.CustomerType.Internal;
        }
        public Guid customerId { get; set; }
        [Display(Name = "Customer Name")]
        [StringLength(100)]
        [Required]
        public string customerName { get; set; }
        [StringLength(200)]
        [Display(Name = "Description")]
        public string description { get; set; }
        [StringLength(255)]
        [Display(Name = "Thumb Url")]
        public string thumbUrl { get; set; }
        [Display(Name = "Customer Type")]
        public Enum.CustomerType customerType { get; set; }

        //address
        [Display(Name = "Full Address")]
        [StringLength(100)]
        public string address { get; set; }
        [Display(Name = "Phone")]
        [StringLength(20)]
        public string phone { get; set; }
        [Display(Name = "Email")]
        [StringLength(100)]
        public string email { get; set; }
        [Display(Name = "Website")]
        [StringLength(100)]
        public string website { get; set; }
        [Display(Name = "Linkedin")]
        [StringLength(100)]
        public string linkedin { get; set; }

        public Guid organizationId { get; set; }
        public Organization organization { get; set; }

        //contacts
        public ICollection<Contact> contacts { get; set; }
    }
}
