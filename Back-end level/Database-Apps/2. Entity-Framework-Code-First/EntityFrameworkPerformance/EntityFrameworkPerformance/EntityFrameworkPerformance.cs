using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace EntityFrameworkPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new AdsEntities();


            // Problem 1 -------------------------
            //foreach (var a in context.Ads)
            //{
            //    Console.WriteLine("- {0} - {1} - {2} - {3} - {4}",
            //        a.Title,
            //        a.AdStatus.Status,
            //        a?.Category?.Name ?? "No category",
            //        a?.Town?.Name ?? "No town",
            //        a?.AspNetUser?.Name ?? "No name");
            //}

            //foreach (var ad in context.Ads
            //    .Include(x => x.AdStatus)
            //    .Include(x => x.Category)
            //    .Include(x => x.Town)
            //    .Include(x => x.AspNetUser))
            //{
            //    Console.WriteLine("- {0} - {1} - {2} - {3} - {4}",
            //        ad.Title,
            //        ad.AdStatus.Status,
            //        ad?.Category?.Name ?? "No category",
            //        ad?.Town?.Name ?? "No town",
            //        ad?.AspNetUser?.Name ?? "No name");
            //}



            //Problem 2 ------------------------------
            //var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            //context.Ads.SqlQuery("CHECKPOINT; DBCC DROPCLEANBUFFERS;");
            //slow version 
            //var ads = context.Ads
            //    .Where(x => x.AdStatus.Status == "Published")
            //    .OrderBy(x => x.Date)
            //    .Select(x => new
            //    {
            //        x.Title,
            //        Town = x.Town.Name,
            //        Category = x.Category.Name
            //    })
            //    .ToList();
            //stopwatch.Stop();

            // optimized version 
            ////var ads = context.Ads
            ////    .Include(x => x.Town)
            ////    .Include(x => x.Category)
            ////    .Where(x => x.AdStatus.Status == "Published")
            ////    .OrderBy(x => x.Date)
            ////    .Select(x => new
            ////    {
            ////        x.Title,
            ////        Town = x.Town.Name,
            ////        Category = x.Category.Name
            ////    })
            ////    .ToList();

            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);


            //Problem 3  ------------------------------------------------------
            //var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            //context.Ads.SqlQuery("CHECKPOINT; DBCC DROPCLEANBUFFERS;");

            //var ads = context.Ads
            //    .Select(x => x);
            //stopwatch.Stop();

            ////var ads = context.Ads
            ////    .Select(x => x.Title);
            ////stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);

        }
    }
}
