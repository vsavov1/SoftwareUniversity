using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace ProductsShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public Product()
        {
            this.Categories = new HashSet<Category>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is requied")]
        [MinLength(3,ErrorMessage = "Product name must be at least 3 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product price is requied")]
        public decimal Price  { get; set; }

        [Required(ErrorMessage = "Product seller is requied")]
        public int SellerId { get; set; }
        public virtual User Seller { get; set; }

        public int? BuyerId { get; set; }
        public virtual User Buyer { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
