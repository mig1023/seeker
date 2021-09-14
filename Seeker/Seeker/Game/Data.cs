using System.Collections.Generic;
using System.Xml;

namespace Seeker.Game
{
    class Data
    {
        public enum ColorTypes
        {
            Background, Font, ActionBox, StatusBar, StatusFont, StatusBorder, AdditionalStatus,
            AdditionalFont, BookColor, BookFontColor, BookBorderColor, SystemFont,
        }

        public const int StartParagraph = 0;

        public const string DescriptionXml = "Descriptions.xml";

        public static Paragraph CurrentParagraph { get; set; }
        public static int CurrentParagraphID { get; set; }

        public static Dictionary<int, XmlNode> XmlParagraphs = new Dictionary<int, XmlNode>();

        public static List<string> Triggers = new List<string>();

        public static Abstract.IParagraphs Paragraphs;
        public static Abstract.IActions Actions;
        public static Abstract.IConstants Constants;

        public static Gamebook.Links.ProtagonistMethod Protagonist;
        public static Gamebook.Links.CheckOnlyIfMethod CheckOnlyIf;
        public static Gamebook.Links.SaveMethod Save;
        public static Gamebook.Links.LoadMethod Load;

        public static void Clean()
        {
            Game.Router.Clean();
            Triggers.Clear();

            Paragraphs = null;
            Actions = null;
            Constants = null;
            Protagonist = null;
            CheckOnlyIf = null;
        }
    }
}
