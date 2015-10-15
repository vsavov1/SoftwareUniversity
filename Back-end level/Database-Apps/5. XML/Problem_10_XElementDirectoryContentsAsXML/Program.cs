namespace Problem_10_XElementDirectoryContentsAsXML
{
    using System;
    using System.Xml.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var dir = new XElement("root-dir",
            new XElement("photos",
                new XElement("photo", "2.jpg"),
                new XElement("photo", "1.jpg")
            ),
            new XElement("videos",
                new XElement("video", "video"),
                new XElement("video", "video")
            ));

            Console.WriteLine(dir);
            //just no time for more....
        }
    }
}
