using System.Data.Entity.ModelConfiguration.Conventions;
using ProductsShop.Data.Migrations;
using ProductsShop.Models;

namespace ProductsShop.Data
{
    using System.Data.Entity;

    public class ProductsShopContext : DbContext
    {
      
        public ProductsShopContext()
            : base("name=ProductsShopContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProductsShopContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasRequired(x => x.Seller)
                .WithMany(x => x.ProductsSold)
                .HasForeignKey(x => x.SellerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
              .HasOptional(x => x.Buyer)
              .WithMany(x => x.ProductsBought)
              .HasForeignKey(x => x.BuyerId)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(f => f.Friends)
                .WithMany()
                .Map(m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("FriendId");
                    m.ToTable("UserFriends");
                });
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categorys { get; set; }
    }
}