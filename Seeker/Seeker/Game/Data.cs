using System;
using System.Collections.Generic;
using System.Xml;

namespace Seeker.Game
{
    class Data
    {
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

        public static Paragraph CurrentParagraph { get; set; }
        public static int CurrentParagraphID { get; set; }

        public static Dictionary<int, XmlNode> XmlParagraphs = new Dictionary<int, XmlNode>();

        public static List<string> Triggers = new List<string>();

        public static List<string> Path = new List<string>();

        public static Abstract.IParagraphs Paragraphs;
        public static Abstract.IActions Actions;
        public static Abstract.IConstants Constants;

        public static Gamebook.Links.ProtagonistMethod Protagonist;
        public static Gamebook.Links.AvailabilityMethod Availability;
        public static Gamebook.Links.StringMethod Save;
        public static Gamebook.Links.LoadMethod Load;
        public static Gamebook.Links.StringMethod Debug;

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
            Protagonist = null;
            Availability = null;
        }
    }
}
