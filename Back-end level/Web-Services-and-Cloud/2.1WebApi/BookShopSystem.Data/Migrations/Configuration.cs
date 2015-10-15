using BookShopSystem.Data.Models;

namespace BookShopSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Globalization;
    using System.IO;
    using System.Threading;

    internal sealed class Configuration : DbMigrationsConfiguration<BookShopSystem.Data.BookShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            this.ContextKey = "BookShopSystem.Data.BookShopContext";
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BookShopContext context)
        {
            if (context.Authors.Count() == 0)
            {
                using (var reader = new StreamReader("../../../authors.txt"))
                {
                    var line = reader.ReadLine();
                    line = reader.ReadLine();
                    while (line != null)
                    {
                        var data = line.Split(new[] { ' ' }, 2);
                        var firstName = data[0];
                        var lastName = data[1];
                        context.Authors.Add(new Author()
                        {
                            FirstName = firstName,
                            LastName = lastName
                        });

                        line = reader.ReadLine();
                    }

                    context.SaveChanges();
                }
            }

            if (context.Books.Count() == 0)
            {
                using (var reader = new StreamReader("../../../books.txt"))
                {
                    var line = reader.ReadLine();
                    line = reader.ReadLine();
                    while (line != null)
                    {
                        Thread.Sleep(10);
                        var data = line.Split(new[] { ' ' }, 6);
                        int authorIndex = new Random().Next(0, context.Authors.Count());
                        var author = context.Authors.ToList().ElementAt(authorIndex);
                        var edition = (EditionType)int.Parse(data[0]);
                        var releaseDate = DateTime.ParseExact(data[1], "d/M/yyyy", CultureInfo.InvariantCulture);
                        var copies = int.Parse(data[2]);
                        var price = decimal.Parse(data[3]);
                        var ageRestriction = (AgeRestriction)int.Parse(data[4]);
                        var title = data[5];

                        context.Books.Add(new Book()
                        {
                            Author = author,
                            EditionType = edition,
                            ReleaseDate = releaseDate,
                            Copies = copies,
                            Price = price,
                            AgeRestriction = ageRestriction,
                            Title = title
                        });

                        line = reader.ReadLine();
                    }
                    context.SaveChanges();
                }
            }

            if (context.Categories.Count() == 0)
            {
                using (var reader = new StreamReader("../../../categories.txt"))
                {
                    var line = reader.ReadLine();
                    line = reader.ReadLine();
                    while (line != null)
                    {
                        if (string.IsNullOrEmpty(line))
                        {
                            line = reader.ReadLine();
                            continue;
                        }

                        context.Categories.Add(new Category()
                        {
                            Name = line
                        });

                        line = reader.ReadLine();
                    }

                    context.SaveChanges();

                    var books = context.Books.ToList();
                    var categories = context.Categories.ToList();
                    var authors = context.Authors.ToList();

                    foreach (var book in books)
                    {
                        Thread.Sleep(11);
                        Console.WriteLine("Seeding database, please wait..");
                        book.Categories.Add(categories.ElementAt(new Random().Next(0, categories.Count())));
                        if (new Random().Next(0, 2) == 1)
                        {
                            book.Categories.Add(categories.ElementAt(new Random().Next(0, categories.Count() - 1)));
                        }

                        Thread.Sleep(6);
                        if (new Random().Next(0, 2) == 0)
                        {
                            book.Categories.Add(categories.ElementAt(new Random().Next(0, categories.Count() - 1)));
                        }

                        Thread.Sleep(15);
                        book.Author = authors.ElementAt(new Random().Next(0, authors.Count() - 1));
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}