namespace Problem_07_DOMParserAndXPathOldAlbums
{
    using System;
    using System.Xml;

    static class Program
    {
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../Problem_01_CatalogOfMusicalAlbumsInXMLFormat/catalog.xml");
            string xPathQuery = "//albums";
            XmlNodeList artistsList = doc.SelectNodes(xPathQuery);

            foreach (XmlNode album in artistsList)
            {
                if (int.Parse(album.SelectSingleNode("year").InnerText) < 2000)
                {
                    continue;
                }

                XmlNode albumTitle = album.SelectSingleNode("name");
                XmlNode albumPrice = album.SelectSingleNode("price");
                Console.WriteLine("Title: {0}, price: {1}", albumTitle.InnerText, albumPrice.InnerText);
            }
        }
    }
}
