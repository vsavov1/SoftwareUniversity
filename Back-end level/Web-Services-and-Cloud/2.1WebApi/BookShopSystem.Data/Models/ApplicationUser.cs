namespace BookShopSystem.Data.Models
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser()
        {
            this.Purchases = new HashSet<Purchase>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync
            (UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            return userIdentity;
        }

        public ICollection<Purchase> Purchases { get; set; }
    }
}
