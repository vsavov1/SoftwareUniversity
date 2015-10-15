using System.Data;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OnlineShop.Data;
using OnlineShop.Models;
using OnlineShop.Services.Models.BindingModels;
using OnlineShop.Services.Models.ViewModels;

namespace OnlineShop.Services.Controllers
{
    public class AdsController : BaseApiController
    {
        // GET: api/Ads
        [System.Web.Http.AllowAnonymous]
        public IHttpActionResult GetAds()
        {
            var outputAds = this.Data.Ads
                .OrderBy(a => a.Type.Name)
                .ThenByDescending(a => a.PostedOn)
                .Select(AdViewModel.Create);

            return this.Ok(outputAds);
        }

        // POST: api/Ads
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateAd(CreateAdBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            if (model == null)
            {
                return BadRequest("Missing data!");
            }

            if (model.Categories.Count() < 0 && model.Categories.Count() > 3)
            {
                return BadRequest("Invalid data!1");
            }

            if (model.Categories.Any(category => !this.Data.Categories.Any(c => c.Id == category)))
            {
                return BadRequest("Invalid data!2");
            }

            if (!this.Data.AdTypes.Any(t => t.Id == model.TypeId))
            {
                return BadRequest("Invalid data!3");
            }

            string userId = User.Identity.GetUserId();
            var user = this.Data.Users.FirstOrDefault(x => x.Id == userId);
            var ad = new Ad
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                TypeId = model.TypeId,
                PostedOn = DateTime.Now,
                Owner = user

            };

            foreach (var c in model.Categories)
            {
                ad.Categories.Add(Data.Categories.FirstOrDefault(x => x.Id == c));
            }

            this.Data.Ads.AddOrUpdate(ad);
            this.Data.SaveChanges();

            AdViewModel result = this.Data.Ads
                .Where(a => a.Id == ad.Id)
                .Select(AdViewModel.Create)
                .FirstOrDefault();
            return Ok(result);
        }

        // PUT: api/Ads/5/close
        [System.Web.Http.Route("api/ads/{id}/close")]
        [System.Web.Http.HttpPut]
        public IHttpActionResult CloseAd(int id)
        {
            Ad ad = this.Data.Ads.FirstOrDefault(a => a.Id == id);
            string userId = (User.Identity.GetUserId());
            if (ad == null)
            {
                return this.NotFound();
            }

            if (userId == null)
            {
                return BadRequest("Not logged.");
            }

            if (ad.OwnerId != userId)
            {
                return StatusCode(HttpStatusCode.MethodNotAllowed);
            }

            ad.ClosedOn = DateTime.Now;
            ad.Status = AdStatus.Closed;
            this.Data.SaveChanges();
            return this.Ok();
        }
    }
}