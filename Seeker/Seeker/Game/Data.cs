using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace Seeker.Game
{
    class Data
    {
        public static string CurrentGamebook { get; set; }

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

        public static void MethodFromBook(string method,
            out string result, string param = "")
        {
            string[] names = method.Split('.');

            Type gamebookClass = Type.GetType($"Seeker.Gamebook.{CurrentGamebook}.{names[0]}");

            MethodInfo gamebookInstance = gamebookClass.GetMethod("GetInstance");
            object instance = gamebookInstance.Invoke(null, parameters: null);
            
            MethodInfo gamebookMethod = gamebookClass.GetMethod(names[1]);
            object[] paramsIfExists = String.IsNullOrEmpty(param) ? null : new object[] { param };  
            object methodResult = gamebookMethod.Invoke(instance, parameters: paramsIfExists);
            result = methodResult?.ToString() ?? String.Empty;
        }

        public static Gamebook.Links.DisableMethod DisableMethod { get; set; }

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
