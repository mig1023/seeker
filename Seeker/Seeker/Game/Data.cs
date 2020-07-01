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

        public static Gamebook.BlackCastleDungeon.Character Protagonist = new Gamebook.BlackCastleDungeon.Character();

        public static Dictionary<int, string> Paragraphs = new Dictionary<int, string>();

        public static List<string> OpenedOption = new List<string>();

        public static void Load(string name)
        {
            Paragraphs.Clear();
            OpenedOption.Clear();

            if (String.IsNullOrEmpty(name))
                return;

            string content = DependencyService.Get<Other.IAssets>().GetFromAssets(name);

            XmlDocument xmlFile = new XmlDocument();
            xmlFile.LoadXml(content);

            foreach (XmlNode xmlNode in xmlFile.SelectNodes("Paragraphs/Paragraph"))
            {
                bool success = Int32.TryParse(xmlNode["ID"].InnerText, out int value);
                int idParagraph = (success ? value : 0);
                string text = System.Text.RegularExpressions.Regex.Unescape(xmlNode["Text"].InnerText);

                Paragraphs.Add(idParagraph, text);
            }

            Protagonist.Init();
        }
    }
}
