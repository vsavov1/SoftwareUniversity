namespace Problem_11_XMLSchemaValidation
{
    using System;
    using System.Xml;
    using System.Xml.Schema;

    class Program
    {
        static void Main(string[] args)
        {
            XmlReaderSettings albumSettings = new XmlReaderSettings();
            albumSettings.Schemas.Add("", "../../catalog.xsd");
            albumSettings.ValidationType = ValidationType.Schema;
            albumSettings.ValidationEventHandler += albumSettingsValidationEventHandler;
            XmlReader album = XmlReader.Create("../../../Problem_01_CatalogOfMusicalAlbumsInXMLFormat/catalog.xml", albumSettings);
            Console.WriteLine("Validated");
        }

        static void albumSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                Console.Write("WARNING: ");
                Console.WriteLine(e.Message);
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                Console.Write("ERROR: ");
                Console.WriteLine(e.Message);
            }
        }
    }
}
