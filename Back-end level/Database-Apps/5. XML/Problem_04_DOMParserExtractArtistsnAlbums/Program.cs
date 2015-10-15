namespace Problem_04_DOMParserExtractArtistsnAlbums
{
    using System;
    using System.Xml;
    using System.Collections.Generic;

    static class Program
    {
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../Problem_01_CatalogOfMusicalAlbumsInXMLFormat/catalog.xml");
            XmlNode root = doc.DocumentElement;
            Dictionary<string, int> artistsAlbumbs = new Dictionary<string, int>();
            foreach (XmlNode album in root.ChildNodes)
            {
                if (!artistsAlbumbs.ContainsKey(album["artist"].InnerText))
                {
                    artistsAlbumbs.Add(album["artist"].InnerText, 1);
                }
                else
                {
                    artistsAlbumbs[album["artist"].InnerText]++;
                }
            }

            foreach (KeyValuePair<string, int> artist in artistsAlbumbs)
            {
                Console.WriteLine("Artist: {0}, albums count: {1}", artist.Key, artist.Value);
            }
        }
    }
}
