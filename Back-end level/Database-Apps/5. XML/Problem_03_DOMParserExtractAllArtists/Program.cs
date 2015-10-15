namespace Problem_03_DOMParserExtractAllArtists
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
            SortedSet<string> artists = new SortedSet<string>();
            foreach (XmlNode album in root.ChildNodes)
            {
                artists.Add(album["artist"].InnerText);
            }

            Console.WriteLine("Artists: {0}", string.Join(", ", artists));
        }
    }
}
