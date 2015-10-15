namespace Problem_06_DOMParserDeleteAlbums
{   
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
                if (int.Parse(album["price"].InnerText) > 20)
                {
                    album.ParentNode.RemoveChild(album);
                }
            }

            doc.Save("../../cheap-albums-catalog.xml");
        }
    }
}
