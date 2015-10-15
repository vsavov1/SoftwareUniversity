namespace ProductsShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "First name must be at least 3 characters")]
        [MaxLength(15, ErrorMessage = "Category name cannot be more than 15 characters")]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
