namespace Problem_05_XPathExtractArtistsAlbums
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../Problem_01_CatalogOfMusicalAlbumsInXMLFormat/catalog.xml");
            string xPathQuery = "//albums";
            XmlNodeList artistsList = doc.SelectNodes(xPathQuery);
            Dictionary<string, int> artistsAlbumbs = new Dictionary<string, int>();

            foreach (XmlNode artist in artistsList)
            {
                XmlNode artistName = artist.SelectSingleNode("artist");
                if (!artistsAlbumbs.ContainsKey(artistName.InnerText))
                {
                    artistsAlbumbs.Add(artistName.InnerText, 1);
                }
                else
                {
                    artistsAlbumbs[artistName.InnerText]++;
                }
            }

            foreach (KeyValuePair<string, int> artist in artistsAlbumbs)
            {
                Console.WriteLine("Artist: {0}, albums count: {1}", artist.Key, artist.Value);
            }
        }
    }
}
