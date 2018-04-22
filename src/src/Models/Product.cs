using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class Product : BaseEntity
    {
        public Product()
        {
            this.thumbUrl = "/images/no-image-available.png";
            this.productCategory = Enum.ProductCategory.Other;
        }
        public int productId { get; set; }
        [Display(Name = "Product Name")]
        [Required]
        [StringLength(100)]
        public string productName { get; set; }
        [StringLength(200)]
        [Display(Name = "Description")]
        public string description { get; set; }
        [StringLength(255)]
        [Display(Name = "Thumb URL")]
        public string thumbUrl { get; set; }
        [Display(Name = "Product Category")]
        public Enum.ProductCategory productCategory { get; set; }


        public int organizationId { get; set; }
        public Organization organization { get; set; }
    }
}
