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
        public enum ColorTypes { Background, Font, ActionBox, StatusBar, StatusFont, StatusBorder,
            AdditionalStatus, AdditionalFont }

        public static Paragraph CurrentParagraph { get; set; }
        public static int CurrentParagraphID { get; set; }
        public static bool ShowDisabledOption { get; set; }

        public static Dictionary<int, XmlNode> XmlParagraphs = new Dictionary<int, XmlNode>();

        public static List<string> Triggers = new List<string>();

        public static Abstract.IParagraphs Paragraphs;
        public static Abstract.IActions Actions;
        public static Abstract.IConstants Constants;

        public static Gamebook.Description.ProtagonistMethod Protagonist;
        public static Gamebook.Description.CheckOnlyIfMethod CheckOnlyIf;
        public static Gamebook.Description.SaveMethod Save;
        public static Gamebook.Description.LoadMethod Load;

        public static void GameLoad(string name)
        {
            XmlParagraphs.Clear();
            Triggers.Clear();

            if (String.IsNullOrEmpty(name))
                return;

            Gamebook.Description gamebook = Gamebook.List.GetDescription(name);

            string content = DependencyService.Get<Abstract.IAssets>().GetFromAssets(gamebook.XmlBook);

            XmlDocument xmlFile = new XmlDocument();
            xmlFile.LoadXml(content);

            foreach (XmlNode xmlNode in xmlFile.SelectNodes("Paragraphs/Paragraph"))
                XmlParagraphs.Add(Game.Xml.IntParse(xmlNode["ID"]), xmlNode);

            Paragraphs = gamebook.Paragraphs;
            Actions = gamebook.Actions;
            Constants = gamebook.Constants;
            Protagonist = gamebook.Protagonist;
            Save = gamebook.Save;
            Load = gamebook.Load;
            CheckOnlyIf = gamebook.CheckOnlyIf;
            ShowDisabledOption = gamebook.ShowDisabledOption;
        }
    }
}
