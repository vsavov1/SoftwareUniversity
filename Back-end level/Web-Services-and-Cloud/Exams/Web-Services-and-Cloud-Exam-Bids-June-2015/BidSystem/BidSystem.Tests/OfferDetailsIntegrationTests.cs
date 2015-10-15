using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BidSystem.Data;
using BidSystem.RestServices.ViewModels;
using BidSystem.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BidSystem.Tests
{
    [TestClass]
    public class OfferDetailsIntegrationTests
    {
        [TestMethod]
        public void GetOffer_WithValidId_ShouldReturn200OKAndDataCorectly()
        {
            //Arrange
            TestingEngine.CleanDatabase();
            var userSession = TestingEngine.RegisterUser("peter", "pAssW@rd#123456");
            var offerModel = new OfferModel() { Title = "Title", Description = "Description", InitialPrice = 200, ExpirationDateTime = DateTime.Now.AddDays(5) };
            var httpResultOffer = TestingEngine.CreateOfferHttpPost(userSession.Access_Token, offerModel.Title, offerModel.Description, offerModel.InitialPrice, offerModel.ExpirationDateTime);

          

            // Act 
            var db = new BidSystemDbContext();
            var offerFromDb = db.Offers.FirstOrDefault();
            var bids = new BidModel[]
            {
                new BidModel() { BidPrice = 250, Comment = "Invalid: less than the initioal price" },
            };

            var httpResultBid0 = TestingEngine.CreateBidHttpPost(userSession.Access_Token, offerFromDb.Id, bids[0].BidPrice, bids[0].Comment);

            db.SaveChanges();

            var offerDetailsResponse = TestingEngine.HttpClient.GetAsync("/api/offers/details/" + offerFromDb.Id).Result;
            var offerResponseContent = httpResultOffer.Content.ReadAsAsync<OfferDetailsViewModel>().Result;

            // Assert 
            Assert.AreEqual(HttpStatusCode.OK, offerDetailsResponse.StatusCode);
        }

        [TestMethod]
        public void GetOffer_WithInValidId_ShouldReturn404NotFound()
        {
            //Arrange
            TestingEngine.CleanDatabase();
            var userSession = TestingEngine.RegisterUser("peter", "pAssW@rd#123456");
            var offerModel = new OfferModel() { Title = "Title", Description = "Description", InitialPrice = 200, ExpirationDateTime = DateTime.Now.AddDays(5) };
            TestingEngine.CreateOfferHttpPost(userSession.Access_Token, offerModel.Title, offerModel.Description, offerModel.InitialPrice, offerModel.ExpirationDateTime);

            // Act 
            var offerDetailsResponse = TestingEngine.HttpClient.GetAsync("/api/offers/details/" +  -1).Result;

            // Assert 
            Assert.AreEqual(HttpStatusCode.NotFound, offerDetailsResponse.StatusCode);
        }
    }
}
