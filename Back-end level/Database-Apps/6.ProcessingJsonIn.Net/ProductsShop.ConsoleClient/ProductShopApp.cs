namespace ProductsShop.ConsoleClient
{
    using System.Linq;
    using Data;
    using System;
    using System.Data.Entity;
    using System.Text;
    using System.Xml;
    using Newtonsoft.Json;

    public static class ProductShopApp
    {
        static void Main()
        {
            var context = new ProductsShopContext();
            var testCount = context.Users.Count();

            //Problem 3  ------------------------------------------------------------------------------------

            //Query 1 - Products In Range Get all products in a specified price range (e.g. 500 to 1000) which 
            //have no buyer. Order them by price (from lowest to highest).Select only the product name, price
            //and the full name of the seller.Export the result to JSON

            var productsInRange = context.Products
                .Include(x => x.Seller)
                .Include(x => x.Buyer)
                .Where(x => x.Price >= 500 && x.Price <= 1000 && x.BuyerId == null)
                .OrderBy(x => x.Price)
                .Select(x => new
                {
                    ProductName = x.Name,
                    x.Price,
                    SellerFullName = x.Seller.FirstName + x.Seller.LastName
                });

            string json = JsonConvert.SerializeObject(productsInRange.ToArray());
            System.IO.File.WriteAllText("../../../products-in-range.json", json);



            //Query 2 - Successfully Sold Products Get all users who have at least 1 sold item with a buyer.Order 
            //them by last name, then by first name. Select the person's first and last name. For each of the 
            //sold products (products with buyers), select the product's name, price and the buyer's first and last name.

            var usersWithSoldItem = context.Users
                .Include(x => x.ProductsSold)
                .Where(x => x.ProductsSold.Count() > 0)
                .Select(x => new
                {
                    firstName = x.FirstName ?? "",
                    lastName = x.LastName,
                    soldProducts = x.ProductsSold.Select(p => new
                    {
                        name = p.Name,
                        price = p.Price,
                        buyerFirstName = p.Buyer.FirstName ?? "",
                        buyerLastName = p.Buyer.LastName
                    })
                });

            string json2 = JsonConvert.SerializeObject(usersWithSoldItem);
            System.IO.File.WriteAllText("../../../users-sold-products.json", json2);



            //Query 3 - Categories By Products Count Get all categories. Order them by the number of products in that 
            //category (a product can be in many categories). For each category select its name, the number of products, 
            //the average price of those products and the total revenue (total price sum) of those products(regardless 
            //if they have a buyer or not).

            var categoriesByProducts = context.Categorys
                .Include(x => x.Products)
                .Select(x => new
                {
                    category = x.Name,
                    productsCount = x.Products.Count,
                    averagePrice = x.Products.Average(z => (Decimal?) z.Price),
                    totalRevenue = x.Products.Sum(q => (Decimal?) q.Price)
                });

            string json3 = JsonConvert.SerializeObject(categoriesByProducts);
            System.IO.File.WriteAllText("../../../categories-by-products.json", json3);



            //Query 4 - Users and Products Get all users who have at least 1 sold product. Order them by the number of
            //sold products (from highest to lowest), then by last name(ascending).Select only their first and last name,
            //age and for each product -name and price Export the results to XML.Follow the format below to better 
            //understand how to structure your data. Note: If a user has no first name or age, do not add attributes.

            var usersAndProducs = context.Users
             .Include(x => x.ProductsSold)
             .Where(x => x.ProductsSold.Count() > 0)
             .Select(x => new
             {
                 firstName = x.FirstName ?? "",
                 lastName = x.LastName,
                 age = x.Age ?? 0,
                 soldProducts = x.ProductsSold.Select(p => new
                 {
                     name = p.Name,
                     price = p.Price,
                 })
             })
             .OrderByDescending(x => x.soldProducts.Count())
             .ThenBy(x => x.lastName);


            string path = "../../../users-and-products.xml";
            Encoding encoding = Encoding.GetEncoding("utf-8");
            using (var writer = new XmlTextWriter(path, encoding))
            {
                writer.Formatting =  System.Xml.Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;
                writer.WriteStartDocument();
                writer.WriteStartElement("users");
                writer.WriteAttributeString("count", usersAndProducs.Count().ToString());

                foreach (var user in usersAndProducs)
                {
                    writer.WriteStartElement("user");

                    if (user.firstName != "" || user.firstName != null)
                    {
                        writer.WriteAttributeString("first-name", user.firstName);
                    }

                    writer.WriteAttributeString("last-name", user.lastName);

                    if (user.age != 0)
                    {
                        writer.WriteAttributeString("age", user.age.ToString());
                    }

                    writer.WriteStartElement("sold-products");
                    writer.WriteAttributeString("count", user.soldProducts.Count().ToString());

                    foreach (var product in user.soldProducts)
                    {
                        writer.WriteStartElement("product ");
                        writer.WriteAttributeString("name", product.name);
                        writer.WriteAttributeString("price", product.price.ToString());
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
            }
        }
    }
}