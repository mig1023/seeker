﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Xamarin.Forms;

namespace Seeker.Game
{
    class Other
    {
        public static void GameLoad(string name)
        {
            Game.Data.XmlParagraphs.Clear();
            Game.Data.Triggers.Clear();
            Healing.Clear();

            if (String.IsNullOrEmpty(name))
                return;

            Gamebook.Description gamebook = Gamebook.List.GetDescription(name);

            string content = DependencyService.Get<Abstract.IAssets>().GetFromAssets(gamebook.XmlBook);

            XmlDocument xmlFile = new XmlDocument();
            xmlFile.LoadXml(content);

            foreach (XmlNode xmlNode in xmlFile.SelectNodes("Paragraphs/Paragraph"))
                Game.Data.XmlParagraphs.Add(Game.Xml.IntParse(xmlNode["ID"]), xmlNode);

            Game.Data.Paragraphs = gamebook.Paragraphs;
            Game.Data.Actions = gamebook.Actions;
            Game.Data.Constants = gamebook.Constants;
            Game.Data.Protagonist = gamebook.Protagonist;
            Game.Data.Save = gamebook.Save;
            Game.Data.Load = gamebook.Load;
            Game.Data.CheckOnlyIf = gamebook.CheckOnlyIf;
            Game.Data.ShowDisabledOption = gamebook.ShowDisabledOption;
        }

        public static bool DoNothing() => true;
    }
}
