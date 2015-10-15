namespace ProductsShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public User()
        {
            this.Friends = new HashSet<User>();
            this.ProductsSold = new HashSet<Product>();
            this.ProductsBought = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "First name must be at least 3 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is requied!")]
        [MinLength(3,ErrorMessage = "Last name must be at least 3 characters")]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public virtual  ICollection<User> Friends { get; set; }

        public virtual ICollection<Product> ProductsSold { get; set; }

        public virtual ICollection<Product> ProductsBought { get; set; }
    }
}
