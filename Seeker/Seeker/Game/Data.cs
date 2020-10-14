﻿using Android.Content.Res;
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
        public enum ColorTypes { Background, Font, ActionBox, StatusBar }

        public static Paragraph CurrentParagraph { get; set; }
        public static int CurrentParagraphID { get; set; }
        public static bool ShowDisabledOption { get; set; }

        public static Dictionary<int, string> TextOfParagraphs = new Dictionary<int, string>();

        public static List<string> Triggers = new List<string>();

        public static Interfaces.IParagraphs Paragraphs;
        public static Interfaces.IActions Actions;
        public static Interfaces.IConstants Constants;

        public static Gamebook.Description.ProtagonistInit Protagonist;
        public static Gamebook.Description.CheckOnlyIfFunc CheckOnlyIf;

        public static void Load(string name)
        {
            TextOfParagraphs.Clear();
            Triggers.Clear();

            if (String.IsNullOrEmpty(name))
                return;

            Gamebook.Description gamebook = Gamebook.List.GetDescription(name);

            string content = DependencyService.Get<Other.IAssets>().GetFromAssets(gamebook.XmlBook);

            XmlDocument xmlFile = new XmlDocument();
            xmlFile.LoadXml(content);

            foreach (XmlNode xmlNode in xmlFile.SelectNodes("Paragraphs/Paragraph"))
            {
                bool success = Int32.TryParse(xmlNode["ID"].InnerText, out int value);
                int idParagraph = (success ? value : 0);
                string text = System.Text.RegularExpressions.Regex.Unescape(xmlNode["Text"].InnerText);

                TextOfParagraphs.Add(idParagraph, text);
            }

            Paragraphs = gamebook.Paragraphs;
            Actions = gamebook.Actions;
            Constants = gamebook.Constants;
            Protagonist = gamebook.Protagonist;
            CheckOnlyIf = gamebook.CheckOnlyIf;
            ShowDisabledOption = gamebook.ShowDisabledOption;
        }
    }
}
