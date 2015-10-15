namespace BidSystem.RestServices.Infrastructure
{
    using System.Threading;
    using Microsoft.AspNet.Identity;

    public class AspNetUserIdProvider   : IUserProvider
    {
        public string GetUserId()
        {
            return Thread.CurrentPrincipal.Identity.GetUserId();
        }
    }
}