namespace BidSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Bid
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string BidderId { get; set; }

        [Required]
        public virtual User  Bidder { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Comment { get; set; }

        public virtual Offer Offer { get; set; }
    }
}
