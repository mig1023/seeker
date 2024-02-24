using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace Seeker.Game
{
    class Data
    {
        public delegate void DisableMethodDelegate(string name);

        public static string CurrentGamebook { get; set; }
        public static Paragraph CurrentParagraph { get; set; }
        public static int CurrentParagraphID { get; set; }
        public static string CurrentSelectedOption { get; set; }

        public enum ColorTypes
        {
            Background,
            Font,
            ActionBox,
            StatusBar,
            StatusFont,
            StatusBorder,
            AdditionalStatus,
            AdditionalFont,
            BookColor,
            BookFontColor,
            BookBorderColor,
            SystemFont,
            GoodColor,
            BadColor,
        }

        public const int PhysicalStartParagraph = 0;

        public const string DescriptionXml = "Descriptions.xml";
        public const string SettingsXml = "Settings.xml";

        public static List<string> OuterGameVariable = new List<string>
        {
            "ChooseCthulhu_Cursed",
        };

        public static Dictionary<int, XmlNode> XmlParagraphs = new Dictionary<int, XmlNode>();

        public static List<string> Triggers = new List<string>();

        public static List<string> Path = new List<string>();

        public static Abstract.IParagraphs Paragraphs;
        public static Abstract.IActions Actions;
        public static Abstract.IConstants Constants;
        public static Abstract.ICharacter Character;

        private static object CreateGamebookInstance(string name, string type) =>
            Activator.CreateInstance(Type.GetType($"Seeker.Gamebook.{name}.{type}"));

        public static void CreateGamebookInstances(string name)
        {
            Data.Constants = (Abstract.IConstants)CreateGamebookInstance(name, "Constants");
            Data.Paragraphs = (Abstract.IParagraphs)CreateGamebookInstance(name, "Paragraphs");
            Data.Actions = (Abstract.IActions)CreateGamebookInstance(name, "Actions");
            Data.Character = (Abstract.ICharacter)CreateGamebookInstance(name, "Character");

            Data.Character.Set(Data.Character);
        }

        public static DisableMethodDelegate DisableMethod { get; set; }

        public static string InputResponse { get; set; }

        public static void Clean(bool reStart = false)
        {
            InputResponse = String.Empty;

            Triggers.Clear();
            Path.Clear();

            if (reStart)
                return;

            Paragraphs = null;
            Actions = null;
            Constants = null;
        }
    }
}
