namespace BidSystem.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BidSystem.Data.Models;
    using BidSystem.Data.Repositories;
    using Moq;

    public class MockContainer
    {
        public Mock<IRepository<Bid>> BidRepositoryMock { get; set; }
        public Mock<IRepository<Offer>> OfferRepositoryMock { get; set; }
        public Mock<IRepository<User>> UserRepositoryMock { get; set; }

        public void PrepareMocks()
        {
            this.SetupFakeBids();
            this.SetupFakeOffers();
            this.SetupFakeUsers();
        }

        private void SetupFakeUsers()
        {
            var fakeUsers = new List<User>()
            {
                new User() {UserName = "vesko", Id = "1"},
                new User() {UserName = "vesko2", Id = "2"}
            };

            this.UserRepositoryMock = new Mock<IRepository<User>>();
            this.UserRepositoryMock.Setup(u => u.All())
                .Returns(fakeUsers.AsQueryable());

            this.UserRepositoryMock.Setup(u => u.Find(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    if (fakeUsers[id] != null)
                    {
                        return fakeUsers[id];
                    }

                    return null;
                });
        }

        private void SetupFakeOffers()
        {
            //var fakeUsers = new List<User>()
            //{
            //    new User() {UserName = "vesko", Id = "1"},
            //    new User() {UserName = "vesko2", Id = "2"}
            //};

            //var fakeOffer = new List<Offer>()
            //{
            //    new Offer()
            //    {
            //        Description = "Offer one description",
            //        ExpirationDate = DateTime.Now.AddDays(5),
            //        Id = 1,
            //        InitialPrice = 100,
            //        PublishDate = DateTime.Now,
            //        Title = "Offer one title",
            //        Seller = fakeUsers[0]
            //    }
            //};

            //fakeOffer[0].Bids = new List<Bid>()
            //{
            //    new Bid()
            //    {
            //        Bidder = fakeUsers[1],
            //        Comment = "bid comment one",
            //        Date = DateTime.Now,
            //        Id = 1,
            //        Price = 500,
            //        Offer = fakeOffer[0]
            //    },
            //    new Bid()
            //    {
            //        Bidder = fakeUsers[1],
            //        Comment = "bid comment two",
            //        Date = DateTime.Now,
            //        Id = 2,
            //        Price = 700,
            //        Offer = fakeOffer[0]
            //    }
            //};

            //this.OfferRepositoryMock = new Mock<IRepository<Offer>>();
            //this.OfferRepositoryMock.Setup(u => u.All())
            //    .Returns(fakeOffer.AsQueryable());
            //this.OfferRepositoryMock.Setup(u => u.Find(It.IsAny<int>()))
            //    .Returns((int id) =>
            //    {
            //        if (fakeOffer[id] != null)
            //        {
            //            return fakeOffer[id];
            //        }

            //        return null;
            //    });
        }

        private void SetupFakeBids()
        {
            var fakeUsers = new List<User>()
            {
                new User() {UserName = "vesko", Id = "1"}
            };

            var fakeOffer = new List<Offer>()
            {
                new Offer()
                {
                    Description = "Offer one description",
                    ExpirationDate = DateTime.Now.AddDays(5),
                    Id = 1,
                    InitialPrice = 100,
                    PublishDate = DateTime.Now,
                    Title = "Offer one title",
                    Seller = fakeUsers[0]
                }
            };

            var fakeBids = new List<Bid>()
            {
                new Bid()
                {
                    Bidder = fakeUsers[0],
                    Comment = "bid comment one",
                    Date = DateTime.Now,
                    Id = 1,
                    Price = 500,
                    Offer = fakeOffer[0]
                },
                new Bid()
                {
                    Bidder = fakeUsers[0],
                    Comment = "bid comment two",
                    Date = DateTime.Now,
                    Id = 2,
                    Price = 700,
                    Offer = fakeOffer[0]
                }
            };

            fakeOffer[0].Bids = fakeBids;

            this.BidRepositoryMock = new Mock<IRepository<Bid>>();
            this.BidRepositoryMock.Setup(u => u.All())
                .Returns(fakeBids.AsQueryable());
            this.BidRepositoryMock.Setup(u => u.Find(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    if (fakeBids[id] != null)
                    {
                        return fakeBids[id];
                    }

                    return null;
                });
        }
    }
}
