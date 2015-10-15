namespace BidSystem.RestServices.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Data.Models;

    public class OfferDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Seller { get; set; }

        public DateTime DatePublished { get; set; }

        public decimal InitialPrice { get; set; }

        public DateTime ExpirationDateTime { get; set; }

        public bool IsExpired { get; set; }

        public int BidsCount { get; set; }

        public string BidWinner { get; set; }

        public List<BidViewModel> Bids { get; set; }

        public static Expression<Func<Offer, OfferDetailsViewModel>> Create
        {
            get
            {
                return b => new OfferDetailsViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Seller = b.Seller.UserName,
                    DatePublished = b.PublishDate,
                    InitialPrice = b.InitialPrice,
                    IsExpired = b.PublishDate < b.ExpirationDate,
                    ExpirationDateTime = b.ExpirationDate,
                    BidsCount = b.Bids.Count,
                    BidWinner = b.PublishDate < b.ExpirationDate
                    ? null : b.Bids.OrderBy(x => x.Price).FirstOrDefault().Bidder.UserName,
                    Bids = b.Bids.Select(x => new BidViewModel()
                    {
                        Id = x.Id,
                        OfferId = x.Offer.Id,
                        DateCreated = x.Date,
                        Bidder = x.Bidder.UserName,
                        OfferdPrice = x.Price,
                        Comment = x.Comment
                    }).ToList()
                };
            }
        }
    }
}