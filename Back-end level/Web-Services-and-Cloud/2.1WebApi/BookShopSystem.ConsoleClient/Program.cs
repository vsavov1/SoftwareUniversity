namespace BookShopSystem.ConsoleClient
{
    using System.Linq;
    using Data;

    class Program
    {
        static void Main(string[] args)
        {
            var context = new BookShopContext();
            var count = context.Books.Count();
        }
    }
}
