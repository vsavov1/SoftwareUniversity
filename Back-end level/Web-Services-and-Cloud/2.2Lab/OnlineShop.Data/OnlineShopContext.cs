using System.Data.Entity;
using OnlineShop.Data.Migrations;

namespace OnlineShop.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    public class OnlineShopContext : IdentityDbContext<ApplicationUser>
    {
     
        public OnlineShopContext()
            : base("OnlineShopContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<OnlineShopContext, Configuration>());
        }

        public static OnlineShopContext Create()
        {
                return new OnlineShopContext();
        }
        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Ad> Ads { get; set; }
        public virtual DbSet<AdType> AdTypes { get; set; }
        //public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}