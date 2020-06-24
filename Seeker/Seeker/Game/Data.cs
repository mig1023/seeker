using Android.Content.Res;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Xamarin.Forms;

namespace Seeker.Game
{
    class Data
    {
        public static Dictionary<int, string> Paragraphs = new Dictionary<int, string>();

        public static void Load(string name)
        {
            Paragraphs.Clear();

            string content = DependencyService.Get<Other.IAssets>().GetFromAssets(name);

            XmlDocument xmlFile = new XmlDocument();
            xmlFile.LoadXml(content);

            foreach (XmlNode xmlNode in xmlFile.SelectNodes("Paragraphs/Paragraph"))
            {
                bool success = Int32.TryParse(xmlNode["ID"].InnerText, out int value);
                int idParagraph = (success ? value : 0);
                string text = xmlNode["Text"].InnerText;

                Paragraphs.Add(idParagraph, text);
            }
        }
    }
}
