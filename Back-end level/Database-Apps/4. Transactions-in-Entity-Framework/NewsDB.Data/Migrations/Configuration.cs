using NewsDB.Models;

namespace NewsDB.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NewsDB.Data.NewsModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "NewsDB.Data.NewsModel";
        }

        protected override void Seed(NewsDB.Data.NewsModel context)
        {
            for (int i = 0; i < 250; i++)
            {
                News news = new News {Content = "abrakadbradamkafuckadbra" + i};
                context.News.AddOrUpdate(x => x.Content, news);
            }

            context.SaveChanges();
        }
    }
}
