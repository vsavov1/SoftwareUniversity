using BidSystem.RestServices.Infrastructure;

namespace BidSystem.RestServices.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using BidSystem.Data;
    using BidSystem.Data.Models;
    using BidSystem.Data.UnitOfWork;
    using BidSystem.RestServices.BindingModels;
    using BidSystem.RestServices.ViewModels;
    using Microsoft.AspNet.Identity;

    public class OffersController : ApiController
    {
        public OffersController()
           : this(new BidSystemData(new BidSystemDbContext()), new AspNetUserIdProvider())
        {
        }

        public OffersController(IBidSystemData data, IUserProvider userIdProvider)
        {
            this.UserIdProvider = UserIdProvider;
            this.db = data;
        }

        private IBidSystemData db { get; set; }
        private IBidSystemData UserIdProvider { get; set; }


        // GET: api/Offers
        [HttpGet]
        [Route("api/offers/all")]
        public IQueryable<OfferViewModel> GetOffers()
        {
            return this.db.Offers.All()
                .Select(OfferViewModel.Create)
                .OrderByDescending(d => d.DatePublished)
                .AsQueryable();
        }

        [HttpGet]
        [Route("api/offers/active")]
        public IQueryable<OfferViewModel> GetActiveOffers()
        {
            return this.db.Offers.All()
                .Where(b => DateTime.Now < b.ExpirationDate)
                .Select(OfferViewModel.Create)
                .OrderByDescending(d => d.DatePublished)
                .AsQueryable();
        }

        [HttpGet]
        [Route("api/offers/expired")]
        public IQueryable<OfferViewModel> GetExpiredOffers()
        {
            return this.db.Offers.All()
                .Where(b => DateTime.Now > b.ExpirationDate)
                .Select(OfferViewModel.Create)
                .OrderByDescending(d => d.DatePublished)
                .AsQueryable();
        }

        // GET: api/Offers/5
        [HttpGet]
        [Route("api/offers/details/{id}")]
        public IHttpActionResult GetOffer(int id)
        {
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return NotFound();
            }

            var offerViewModel = new OfferDetailsViewModel()
            {
                Id = offer.Id,
                Title = offer.Title,
                Description = offer.Description,
                Seller = offer.Seller.UserName,
                DatePublished = offer.PublishDate,
                InitialPrice = offer.InitialPrice,
                IsExpired = offer.PublishDate < offer.ExpirationDate,
                ExpirationDateTime = offer.ExpirationDate,
                BidsCount = offer.Bids.Count,
                BidWinner =
                    offer.PublishDate < offer.ExpirationDate ? 
                    null: offer.Bids.OrderBy(x => x.Price).FirstOrDefault().Bidder.UserName,
                Bids = offer.Bids.Select(x => new BidViewModel()
                {
                    Id = x.Id,
                    OfferId = x.Offer.Id,
                    DateCreated = x.Date,
                    Bidder = x.Bidder.UserName,
                    OfferdPrice = x.Price,
                    Comment = x.Comment
                })
                .OrderByDescending(d => d.DateCreated)
                .ToList()
            };

            return Ok(offerViewModel);
        }

        [HttpGet]
        [Route("api/offers/my")]
        public IHttpActionResult GetOwnOffers()
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.db.Users.Find(currentUserId);

            if (currentUser == null)
            {
                return this.Unauthorized();
            }

            var results = this.db.Offers.All()
                .Select(OfferViewModel.Create)
                .Where(u => u.Seller == currentUser.UserName)
                .OrderByDescending(d => d.DatePublished);

            return this.Ok(results);
        }

        // POST: api/Offers
        [HttpPost]
        [Authorize]
        public IHttpActionResult PostOffer(OfferBindingModel offer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (offer == null)
            {
                return BadRequest();
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.db.Users.Find(currentUserId);

            var newOffer = new Offer()
            {
                Title = offer.Title,
                ExpirationDate = offer.ExpirationDateTime,
                InitialPrice = offer.InitialPrice,
                PublishDate = DateTime.Now,
                Seller = currentUser,
                Description = offer.Description
            };

            db.Offers.Add(newOffer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = newOffer.Id },
                new
                {
                    Id = newOffer.Id,
                    Seller = currentUser.UserName,
                    Message = "Offer created."
                });
        }
    }
}