namespace BookShopSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Purchase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Book Book { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public bool IsRecalled { get; set; }
    }
}
