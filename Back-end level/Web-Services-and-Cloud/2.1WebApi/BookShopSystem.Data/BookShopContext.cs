using System.Runtime.Remoting.Contexts;
using BookShopSystem.Data.Migrations;
using BookShopSystem.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookShopSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BookShopContext : IdentityDbContext<ApplicationUser>
    {
        public BookShopContext()
            : base("name=BookShopContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BookShopContext,Configuration>());
        }

        public static BookShopContext Create()
        {
            return new BookShopContext();
        }

        public IDbSet<Book> Books { get; set; }
        public IDbSet<Author> Authors { get; set; }
        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Purchase> Purchases { get; set; }
        public System.Data.Entity.DbSet<BookShopSystem.Data.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}