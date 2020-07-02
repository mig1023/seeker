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
        public static Paragraph CurrentParagraph { get; set; }

        public static Dictionary<int, string> TextOfParagraphs = new Dictionary<int, string>();

        public static List<string> OpenedOption = new List<string>();

        public static Interfaces.IParagraphs Paragraphs;
        public static Interfaces.IActions Actions;

        public static void Load(string name)
        {
            TextOfParagraphs.Clear();
            OpenedOption.Clear();

            if (String.IsNullOrEmpty(name))
                return;

            string content = DependencyService.Get<Other.IAssets>().GetFromAssets(Gamebook.List.GetDescription(name).XmlBook);

            XmlDocument xmlFile = new XmlDocument();
            xmlFile.LoadXml(content);

            foreach (XmlNode xmlNode in xmlFile.SelectNodes("Paragraphs/Paragraph"))
            {
                bool success = Int32.TryParse(xmlNode["ID"].InnerText, out int value);
                int idParagraph = (success ? value : 0);
                string text = System.Text.RegularExpressions.Regex.Unescape(xmlNode["Text"].InnerText);

                TextOfParagraphs.Add(idParagraph, text);
            }

            Paragraphs = Gamebook.List.GetDescription(name).Paragraphs;
            Actions = Gamebook.List.GetDescription(name).Actions;

            Gamebook.List.GetDescription(name).Protagonist();
        }
    }
}
