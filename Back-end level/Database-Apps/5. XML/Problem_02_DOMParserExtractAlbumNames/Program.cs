namespace Problem_02_DOMParserExtractAlbumNames
{
    using System;
    using System.Xml;

    static class Program
    {
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../Problem_01_CatalogOfMusicalAlbumsInXMLFormat/catalog.xml");
            XmlNode root = doc.DocumentElement;

            foreach (XmlNode album in root.ChildNodes)
            {
                Console.WriteLine("Album: " + album["name"]?.InnerText);
            }
        }
    }
}
