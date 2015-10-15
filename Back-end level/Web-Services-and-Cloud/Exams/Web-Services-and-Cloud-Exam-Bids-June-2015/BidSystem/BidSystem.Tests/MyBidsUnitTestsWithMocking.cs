using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.SessionState;
using System.Web.UI;
using BidSystem.Data;
using BidSystem.Data.Models;
using BidSystem.Data.UnitOfWork;
using BidSystem.RestServices.Controllers;
using BidSystem.RestServices.Infrastructure;
using BidSystem.RestServices.Models;
using BidSystem.RestServices.ViewModels;
using BidSystem.Tests.Mocks;
using BidSystem.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BidSystem.Tests
{
    [TestClass]
    public class MyBidsUnitTestsWithMocking
    {
        private MockContainer mocks;

        [TestInitialize]
        public void InitTest()
        {
            this.mocks = new MockContainer();
            this.mocks.PrepareMocks();
        }

        [TestMethod]
        public void test()
        {
            var bids = new List<Bid>();
            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault();
            var mockContext = new Mock<IBidSystemData>();
            mockContext.Setup(c => c.Bids)
            .Returns(this.mocks.BidRepositoryMock.Object);
            var mockIdProvider = new Mock<IUserProvider>();
            mockIdProvider.Setup(id => id.GetUserId())
            .Returns(fakeUser.Id);
            this.mocks.BidRepositoryMock
                .Setup(r => r.Add(It.IsAny<Bid>()))
                .Callback((Bid bid) =>
                {
                    bid.BidderId = fakeUser.Id;
                    bid.Bidder = fakeUser;
                    bids.Add(bid);
                });

            var bidsController = new BidsController(mockContext.Object);
            bidsController.UserIdProvider = mockIdProvider.Object;
            this.SetupControllerForTesting(bidsController, "Bids");

            var respone = bidsController.GetBids();
            Assert.AreEqual(2, respone.ToList().Count);

            //this.mocks.BidRepositoryMock
            //    .Setup(r => r.Add(It.IsAny<Bid>()))
            //    .Callback((Bid bid) =>
            //    {
            //        bid.Bidder = fakeUser;
            //        bids.Add(bid);
            //    });

        }

        private void SetupControllerForTesting(ApiController controller, string controllerName)
        {
            string serverUrl = "http://sample-url.com";

            // Setup the Request object of the controller
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(serverUrl)
            };

            controller.Request = request;

            // Setup the configuration of the controller
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            controller.Configuration = config;

            // Apply the routes to the controller
            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary
                {
                    { "controller", controllerName }
                });
        }
    }
}
