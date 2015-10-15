using NewsDB.Data.Migrations;

namespace NewsDB.Data
{
    using System.Data.Entity;
    using Models;

    public class NewsModel : DbContext
    {
      
        public NewsModel()
            : base("name=NewsModel")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NewsModel, Configuration>());
        }

        public virtual DbSet<News> News { get; set; }
    }
}