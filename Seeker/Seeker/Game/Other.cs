using System;
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

        public static string Сomparison(int a, int b)
        {
            if (a > b)
                return "больше";

            else if (a < b)
                return "меньше";

            else
                return "равно";
        }

        public static string CoinsNoun(int value, string one, string two, string five)
        {
            int absValue = Math.Abs(value);

            if (absValue % 10 == 5)
                return five;

            absValue %= 100;

            if ((absValue >= 5) && (absValue <= 20))
                return five;

            absValue %= 10;

            if (absValue == 1)
                return one;

            if ((absValue >= 2) && (absValue <= 5))
                return two;

            return five;
        }

        public static int LevelParse(string option) => int.Parse(option.Contains("=") ? option.Split('=')[1] : option.Split('>', '<')[1]);

        public static bool DoNothing() => true;
    }
}
