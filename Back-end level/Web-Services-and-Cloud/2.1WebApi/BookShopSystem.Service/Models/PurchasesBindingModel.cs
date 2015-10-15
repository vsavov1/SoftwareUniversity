namespace BookShopSystem.Service.Models
{
    
    using System;

    public class PurchasesBindingModel
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public decimal Price { get; set; }

        public DateTime PurchaseDate { get; set; }

        public bool IsRecalled { get; set; }
    }
}