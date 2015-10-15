namespace Problem_12_XMLToHTMLThroughXSLStylesheet
{
    using System.Xml.Xsl;

    class Program
    {
        static void Main(string[] args)
        {
            var xslt = new XslCompiledTransform();
            xslt.Load("../../catalog.xsl");
            xslt.Transform("../../../Problem_01_CatalogOfMusicalAlbumsInXMLFormat/catalog.xml", "../../catalog.html");
        }
    }
}
