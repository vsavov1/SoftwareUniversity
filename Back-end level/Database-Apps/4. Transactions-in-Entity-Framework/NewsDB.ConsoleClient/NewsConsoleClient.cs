using System;
using System.Linq;
using NewsDB.Data;
using NewsDB.Models;

namespace NewsDB.ConsoleClient
{
    public static class NewsConsoleClient
    {
        //------------------------------------------------------------------
        //------------------------Problem 1-2 ------------------------------
        //------------------------------------------------------------------
        static void Main(string[] args)
        {
            var context = new NewsModel();
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                bool isSucced = true;
                while (isSucced)
                {
                    try
                    {
                        isSucced = false;
                        var news = context.News.FirstOrDefault();
                        Console.WriteLine("-=- Edit first news -=-");
                        Console.WriteLine("News id: {0} - Content: {1}", news.Id, news.Content);
                        Console.WriteLine("Enter new content: ");
                        var content = Console.ReadLine();
                        var firstOrDefault = context.News.FirstOrDefault();
                        if (firstOrDefault != null) firstOrDefault.Content = content;
                        context.SaveChanges();
                        dbContextTransaction.Commit();
                        Console.WriteLine("Changes successfully saved in the DB.");
                    }
                    catch (Exception ex)
                    {
                        isSucced = true;
                        Console.WriteLine("Error: " + ex.Message);
                        dbContextTransaction.Rollback();
                    }
                }
               
            }
        }
    }
}
