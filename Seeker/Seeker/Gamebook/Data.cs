using Android.Content.Res;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Xamarin.Forms;

namespace Seeker.Gamebook
{
    class Data
    {
        public static Dictionary<int, Paragraph> Paragraphs = new Dictionary<int, Paragraph>();

        public static void LoadGameBook(string name)
        {
            Paragraphs.Clear();

            string content = DependencyService.Get<Other.IAssets>().GetFromAssets(name);

            XmlDocument xmlFile = new XmlDocument();
            xmlFile.LoadXml(content);

            foreach (XmlNode xmlNode in xmlFile.SelectNodes("GameBook/Paragraphs/Paragraph"))
            {
                Paragraph paragraph = new Paragraph
                {
                    Options = new List<Option>()
                };

                bool success = Int32.TryParse(xmlNode["ID"].InnerText, out int value);
                int idParagraph = (success ? value : 0);

                paragraph.Title = xmlNode["Title"].InnerText;
                paragraph.Text = xmlNode["Text"].InnerText;

                foreach (XmlNode xmlOption in xmlNode.SelectNodes("Options/Option"))
                {
                    bool optionExist = Int32.TryParse(xmlOption.Attributes["Destination"].Value, out int desination);

                    if (!optionExist)
                        continue;

                    Option option = new Option
                    {
                        Destination = desination,
                        Text = xmlOption.Attributes["Text"].Value
                    };

                    paragraph.Options.Add(option);
                }

                Paragraphs.Add(idParagraph, paragraph);
            }
        }
    }
}
