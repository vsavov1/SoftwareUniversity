namespace BidSystem.RestServices.ViewModels
{
    using System;
    using System.Linq.Expressions;
    using Data.Models;

    public class BidViewModel
    {
        public int Id { get; set; }

        public int OfferId { get; set; }

        public DateTime DateCreated { get; set; }

        public string Bidder { get; set; }

        public decimal OfferdPrice { get; set; }

        public string Comment { get; set; }

        public static Expression<Func<Bid, BidViewModel>> Create
        {
            get
            {
                return x => new BidViewModel()
                {
                    Id = x.Id,
                    OfferId = x.Offer.Id,
                    DateCreated = x.Date,
                    Bidder = x.Bidder.UserName,
                    OfferdPrice = x.Price,
                    Comment = x.Comment
                };
            }
        }
    }
}