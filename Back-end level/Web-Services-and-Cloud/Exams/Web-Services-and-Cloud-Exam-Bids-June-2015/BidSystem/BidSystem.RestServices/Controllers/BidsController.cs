namespace BidSystem.RestServices.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using Data;
    using Data.Models;
    using Data.UnitOfWork;
    using BindingModels;
    using ViewModels;
    using Microsoft.AspNet.Identity;
    using BidSystem.RestServices.Infrastructure;

    public class BidsController : ApiController
    {
        public BidsController()
           : this(new BidSystemData(new BidSystemDbContext()))
        {
        }

        public BidsController(IBidSystemData data)
        {
            this.UserIdProvider = UserIdProvider;
            this.db = data;
        }

        private IBidSystemData db { get; set; }
        public IUserProvider UserIdProvider { get; set; }

        [HttpGet]
        [Route("api/bids/my")]
        [Authorize]
        public IQueryable<BidViewModel> GetBids()
        {
            //var currentUserId = User.Identity.GetUserId();
            var currentUserId = this.UserIdProvider.GetUserId();
//            var currentUser = this.db.Users.Find(currentUserId);
            var test = db.Bids.All();
            var results = db.Bids.All().Where(u => u.Bidder.Id == currentUserId).Select(BidViewModel.Create).AsQueryable();
            return results;
        }

        [HttpGet]
        [Route("api/bids/won")]
        [Authorize]
        public IQueryable<BidViewModel> GetOwnWonBids()
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.db.Users.Find(currentUserId);

            var results = db.Bids.All()
                .Where(b => DateTime.Now > b.Offer.ExpirationDate && b.BidderId == currentUser.Id)
                .Select(BidViewModel.Create)
                .AsQueryable();
            return results;
        }

        [HttpPost]
        [Route("api/offers/{id}/bid")]
        [Authorize]
        public IHttpActionResult PostBid([FromUri] int id, [FromBody]BidBindingModel model)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.db.Users.Find(currentUserId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model == null)
            {
                return BadRequest();
            }

            var offer = this.db.Offers.Find(id);
            if (offer == null)
            {
                return NotFound();
            }

            if (DateTime.Now > offer.ExpirationDate)
            {
                return BadRequest("Offer has expired.");
            }

            var offerCurrentPrice = offer.Bids.OrderByDescending(b => b.Price).Select(b => b.Price).FirstOrDefault() >
                                    offer.InitialPrice
                ? offer.Bids.OrderByDescending(b => b.Price).Select(b => b.Price).FirstOrDefault()
                : offer.InitialPrice;

            if (model.BidPrice <= offerCurrentPrice)
            {
                return BadRequest("Your bid should be > " + offerCurrentPrice);
            }

            var newBid = new Bid()
            {
                Price = model.BidPrice,
                Bidder = currentUser,
                Comment = model.Comment,
                Date = DateTime.Now,
                Offer = offer
            };

            offer.Bids.Add(newBid);
            db.SaveChanges();

            return this.Ok(
                new
                {
                    Id = newBid.Id,
                    Bidder = currentUser.UserName,
                    Message = "Bid created."
                });
        }
    }
}