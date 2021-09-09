using System;
using System.Xml;
using Xamarin.Forms;

namespace Seeker.Game
{
    public class Other
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

            foreach (XmlNode xmlNode in xmlFile.SelectNodes("Gamebook/Paragraphs/Paragraph"))
                Game.Data.XmlParagraphs.Add(Game.Xml.IntParse(xmlNode["ID"]), xmlNode);

            Game.Data.Paragraphs = gamebook.Links.Paragraphs;
            Game.Data.Actions = gamebook.Links.Actions;
            Game.Data.Constants = gamebook.Links.Constants;
            Game.Data.Protagonist = gamebook.Links.Protagonist;
            Game.Data.Save = gamebook.Links.Save;
            Game.Data.Load = gamebook.Links.Load;
            Game.Data.CheckOnlyIf = gamebook.Links.CheckOnlyIf;
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
