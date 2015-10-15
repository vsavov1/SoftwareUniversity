namespace Problem_09_XmlWriterDirectoryContentsAsXML
{
    using System.IO;
    using System.Text;
    using System.Xml;

    static class Program
    {
        static void Main(string[] args)
        {
            string path = "../../dir.xml";
            Encoding encoding = Encoding.GetEncoding("windows-1251");

            using (var writer = new XmlTextWriter(path, encoding))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;
                writer.WriteStartDocument();
                writer.WriteStartElement("root-dir");
                writer.WriteAttributeString("path", path);

                string sourcePath = @"../../example";
                string[] dirs = Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories);

                foreach (var directory in dirs)
                {
                    writer.WriteStartElement("dir");
                    writer.WriteAttributeString("name", directory.Substring(sourcePath.Length + 1));
                    string[] files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);

                    foreach (var file in files)
                    {
                        writer.WriteStartElement("file");
                        writer.WriteAttributeString("name", file.Substring(directory.Length + 1));
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }
            }
        }
    }
}