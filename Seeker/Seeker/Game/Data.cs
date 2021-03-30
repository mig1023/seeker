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

        public const int StartParagraph = 0;

        public static Paragraph CurrentParagraph { get; set; }
        public static int CurrentParagraphID { get; set; }
        public static bool ShowDisabledOption { get; set; }
        public static bool GameNotStartYet { get; set; }

        public static Dictionary<int, XmlNode> XmlParagraphs = new Dictionary<int, XmlNode>();

        public static List<string> Triggers = new List<string>();

        public static Abstract.IParagraphs Paragraphs;
        public static Abstract.IActions Actions;
        public static Abstract.IConstants Constants;

        public static Gamebook.Description.ProtagonistMethod Protagonist;
        public static Gamebook.Description.CheckOnlyIfMethod CheckOnlyIf;
        public static Gamebook.Description.SaveMethod Save;
        public static Gamebook.Description.LoadMethod Load;
    }
}
