namespace Problem_08_LINQToXMLOldAlbums
{
    using System;
    using System.Linq;
    using System.Xml.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            XDocument doc = XDocument.Load("../../../Problem_01_CatalogOfMusicalAlbumsInXMLFormat/catalog.xml");
            var albums =
                from album in doc.Descendants("albums")
                where int.Parse(album.Element("year").Value) >= 2000
                select new
                {
                    Title = album.Element("name").Value,
                    Price = album.Element("price").Value
                };

            foreach (var album in albums)
            {
                Console.WriteLine("Title{0}, price: {1}", album.Title, album.Price);
            }
        }
    }
}
