
namespace BookShopSystem.Service.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using Data;

    public class ApplicationUsersController : ApiController
    {
        private BookShopContext db = new BookShopContext();

        [Route("api/user/{username}/purchases")]
        public IHttpActionResult GetPpurchases(string username)
        {
            var results = db.Users
                .Where(p => p.UserName == username)
                .Select(u => new
                {
                    u.UserName,
                    purchases = u.Purchases
                        .Select(p => new
                        {
                            BookTitle = p.Book.Title,
                            PurchasePrice = p.Amount * p.Price,
                            DateOfPurchase = p.DateTime,
                            p.IsRecalled
                        }).OrderBy(p => p.DateOfPurchase)
                });

            return this.Ok(results);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}