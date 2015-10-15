namespace ProductsShop.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    using System.Xml;
    using Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ProductsShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ProductsShopContext context)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../users.xml");
            XmlNode root = doc.DocumentElement;

            foreach (XmlNode user in root.ChildNodes)
            {
                var newUser = new User
                {
                    FirstName = user.Attributes["first-name"]?.Value,
                    LastName = user.Attributes["last-name"].Value,
                    Age = int.Parse(user.Attributes["age"]?.Value ?? "0")
                };

                 context.Users.AddOrUpdate(x => x.LastName, newUser);
               
            }
            context.SaveChanges();

            using (StreamReader r = new StreamReader("../../../products.json"))
            {
                string json = r.ReadToEnd();

                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                var users = context.Users.ToList();

                foreach (var product in products)
                {
                    product.SellerId = users.ElementAt(new Random().Next(0, users.Count() - 1)).Id;
                    if (2 == (new Random().Next(0, 4)))
                    {
                        int userId = new Random().Next(0, users.Count());
                        while (product?.SellerId == users.ElementAt(userId).Id)
                        {
                            userId = new Random().Next(0, users.Count());
                        }

                        product.BuyerId = users.ElementAt(userId).Id;
                    }

                    context.Products.AddOrUpdate(x => x.Name, product);
                }
            }

            using (StreamReader r = new StreamReader("../../../categories.json"))
            {
                string json = r.ReadToEnd();
                var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(json);
                var products = context.Products.ToList();

                foreach (var category in categories)
                {
                    context.Categorys.AddOrUpdate(x => x.Name, category);
                }

                context.SaveChanges();
                var ra = new Random();
                var cat = context.Categorys.ToList();
                foreach (var product in products)
                {
                    if (1 == (ra.Next(0,5)))
                    {
                        product.Categories.Add(cat.ElementAt(new Random().Next(0, cat.Count - 1)));
                    }
                }
            }

            context.SaveChanges();
        }
    }
}