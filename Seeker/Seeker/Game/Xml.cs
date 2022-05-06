using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Seeker.Gamebook;
using Seeker.Output;
using Xamarin.Forms;

namespace Seeker.Game
{
    class Xml
    {
        private static Dictionary<string, XmlNode> Descriptions { get; set; }
        private static List<XmlNode> SettingsData { get; set; }

        public static int IntParse(string text) => int.TryParse(text, out int value) ? value : 0;

        public static int IntParse(XmlNode xmlNode) => IntParse(xmlNode?.InnerText ?? "0");

        public static string StringParse(XmlNode xmlNode) => xmlNode?.InnerText ?? String.Empty;

        public static Dictionary<string, string> ImagesParse(XmlNode xmlNode)
        {
            Dictionary<string, string> images = new Dictionary<string, string>();

            if (xmlNode == null)
                return images;

            foreach (XmlNode xmlImage in xmlNode.SelectNodes("Image"))
                images.Add(StringParse(xmlImage.Attributes["Image"]), StringParse(xmlImage.SelectSingleNode("Text")));

            return images;
        }

        public static bool BoolParse(XmlNode xmlNode) => xmlNode != null;

        public static Abstract.IModification ModificationParse(XmlNode xmlNode, Abstract.IModification modification)
        {
            if (xmlNode == null)
                return null;

            modification.Name = StringParse(xmlNode.Attributes["Name"]);
            modification.Value = IntParse(xmlNode.Attributes["Value"]);
            modification.ValueString = StringParse(xmlNode.Attributes["ValueString"]);
            modification.Empty = BoolParse(xmlNode.Attributes["Empty"]);
            modification.Restore = BoolParse(xmlNode.Attributes["Restore"]);

            return modification;
        }

        public static string TextParse(int id, string optionName)
        {
            string textByParagraph = String.Empty;

            if (Data.XmlParagraphs[id]["Text"] != null)
                textByParagraph = Data.XmlParagraphs[id]["Text"].InnerText;

            string textByOption = Data.Actions.TextByOptions(optionName);

            return (String.IsNullOrEmpty(textByOption) ? textByParagraph : textByOption);
        }

        public static void AllTextParse(ref StackLayout textPlace, int id, string optionName)
        {
            string text = Xml.TextParse(id, optionName);

            if (!String.IsNullOrEmpty(text))
                textPlace.Children.Add(Interface.Text(text));

            foreach (Text texts in Xml.TextsParse(Data.XmlParagraphs[id]))
            {
                if (texts.Selected)
                    textPlace.Children.Add(Interface.Text(texts, selected: true));
                else
                    textPlace.Children.Add(Interface.Text(texts));
            }
        }

        public static List<Text> TextsParse(XmlNode xmlNode, bool aftertext = false)
        {
            List<Text> texts = new List<Text>();

            foreach (XmlNode text in xmlNode.SelectNodes(aftertext ? "Aftertexts/Aftertext" : "Texts/Text"))
            {
                string font = StringParse(text.Attributes["Font"]);

                Text outputText = new Text
                {
                    Content = text.InnerText,
                    Bold = (font == "Bold"),
                    Italic = (font == "Italic"),
                    Alignment = StringParse(text.Attributes["Alignment"]),
                    Selected = BoolParse(text.Attributes["Selected"]),
                };

                if (text.Attributes["Size"] != null)
                {
                    Enum.TryParse(StringParse(text.Attributes["Size"]), out Interface.TextFontSize size);
                    outputText.Size = size;
                }
                else
                    outputText.Size = Interface.TextFontSize.nope;

                texts.Add(outputText);
            }

            return texts;
        }

        public static int PlaythrougParse(XmlNode xmlNode)
        {
            foreach (string type in Constants.PLAYTHROUGH_TIME_NODE.Keys)
                if (xmlNode[type] != null)
                    return Constants.PLAYTHROUGH_TIME_NODE[type];

            return 1;
        }

        public static void GameLoad(string name)
        {
            Data.XmlParagraphs.Clear();
            Data.Triggers.Clear();
            Healing.Clear();

            if (String.IsNullOrEmpty(name))
                throw new Exception("Gamebook name not found");

            Description gamebook = List.GetDescription(name);

            XmlDocument xmlFile = new XmlDocument();
            xmlFile.LoadXml(DependencyService.Get<Abstract.IAssets>().GetFromAssets(gamebook.XmlBook));

            foreach (XmlNode xmlNode in xmlFile.SelectNodes("Gamebook/Paragraphs/Paragraph"))
                Data.XmlParagraphs.Add(Xml.IntParse(xmlNode["Id"]), xmlNode);

            Data.Paragraphs = gamebook.Links.Paragraphs;
            Data.Actions = gamebook.Links.Actions;
            Data.Constants = gamebook.Links.Constants;
            Data.Protagonist = gamebook.Links.Protagonist;
            Data.Save = gamebook.Links.Save;
            Data.Load = gamebook.Links.Load;
            Data.Debug = gamebook.Links.Debug;
            Data.Availability = gamebook.Links.Availability;

            Data.Constants.Clean();

            foreach (XmlNode xmlNode in xmlFile.SelectNodes(Intro("Styles/Color")))
                foreach (string type in Multiples(xmlNode, "Type"))
                    Data.Constants.LoadColor(type, xmlNode.InnerText);

            string dasbledOption = Xml.StringParse(xmlFile.SelectSingleNode(Intro("Settings/DisabledOption")).Attributes["Value"]);
            Data.Constants.LoadEnabledDisabledOption(dasbledOption);

            foreach (XmlNode xmlNode in xmlFile.SelectNodes(Intro("Buttons/Button")))
                foreach (string type in Multiples(xmlNode, "Type"))
                    Data.Constants.LoadButtonText(type, xmlNode.InnerText);

            foreach (XmlNode xmlNode in xmlFile.SelectNodes(Intro("Constants/List")))
                Data.Constants.LoadList(xmlNode.Attributes["Type"].InnerText, xmlNode.InnerText.Split(',').ToList());

            foreach (XmlNode xmlNode in xmlFile.SelectNodes(Intro("Constants/Dictionary")))
            {
                List<XmlNode> xmlNodes = xmlNode.SelectNodes("Item").Cast<XmlNode>().ToList();

                Dictionary<string, string> items = xmlNodes
                    .ToDictionary(x => x.Attributes["Key"].InnerText, x => x.Attributes["Value"].InnerText);

                Data.Constants.LoadDictionary(xmlNode.Attributes["Type"].InnerText, items);
            }
        }

        private static List<string> Multiples(XmlNode xmlNode, string attributes) =>
            xmlNode.Attributes[attributes].InnerText.Split(',').Select(x => x.Trim()).ToList();

        private static string Intro(string node) => String.Format("Gamebook/Introduction/{0}", node);

        public static void GetXmlDescriptionData(ref Description description)
        {
            if (Descriptions == null)
                DescriptionLoad();

            if (!Descriptions.ContainsKey(description.Book))
                return;

            XmlNode data = Descriptions[description.Book];

            description.Title = StringParse(data["Title"]);
            description.Original = StringParse(data["Original"]);
            description.Author = StringParse(data["Author"]);
            description.Authors = StringParse(data["Authors"]);
            description.SinglePseudonym = BoolParse(data["SinglePseudonym"]);
            description.FullPseudonym = BoolParse(data["FullPseudonym"]);
            description.Translator = StringParse(data["Translator"]);
            description.Translators = StringParse(data["Translators"]);
            description.Year = IntParse(data["Year"]);
            description.Text = StringParse(data["Text"]);
            description.Paragraphs = StringParse(data["Paragraphs"]);
            description.Size = StringParse(data["Size"]);
            description.PlaythroughTime = PlaythrougParse(data);
            description.Setting = StringParse(data["Setting"]);

            XmlNode colors = data.SelectSingleNode("Colors");

            if ((colors != null) || Settings.IsEnabled("WithoutStyles"))
            {
                description.BookColor = ColorLoad(colors, "Book", Data.ColorTypes.BookColor);
                description.FontColor = ColorLoad(colors, "Font", Data.ColorTypes.BookFontColor);
                description.BorderColor = ColorLoad(colors, "Border", Data.ColorTypes.BookBorderColor);
            }
            else
                description.BookColor = ColorLoad(data, "Color", Data.ColorTypes.BookColor);
        }

        public static List<Output.Settings> GetXmlSettings()
        {
            if (SettingsData == null)
                SettingsLoad();

            List<Output.Settings> settings = new List<Output.Settings>();

            foreach (XmlNode data in SettingsData)
            {
                Output.Settings setting = new Output.Settings
                {
                    Name = StringParse(data["Name"]),
                    Type = StringParse(data["Type"]),
                    Description = StringParse(data["Description"]),
                };

                setting.Options = new List<string>();

                foreach (XmlNode option in data.SelectNodes("Options/Option"))
                    setting.Options.Add(option.InnerText);

                if (setting.Options.Count == 0)
                    setting.Options = null;

                settings.Add(setting);
            }

            return settings;
        }

        private static string ColorLoad(XmlNode xmlNode, string name, Data.ColorTypes defaultColor) =>
            Settings.IsEnabled("WithoutStyles") ? Prototypes.Constants.DefaultColor(defaultColor) : StringParse(xmlNode[name]);
 
        private static void DescriptionLoad()
        {
            Descriptions = new Dictionary<string, XmlNode>();

            XmlDocument xmlFile = new XmlDocument();
            xmlFile.LoadXml(DependencyService.Get<Abstract.IAssets>().GetFromAssets(Data.DescriptionXml));

            foreach (XmlNode xmlNode in xmlFile.SelectNodes("Gamebooks/Description"))
                Descriptions.Add(xmlNode["Name"].InnerText, xmlNode);
        }

        private static void SettingsLoad()
        {
            SettingsData = new List<XmlNode>();

            XmlDocument xmlFile = new XmlDocument();
            xmlFile.LoadXml(DependencyService.Get<Abstract.IAssets>().GetFromAssets(Data.SettingsXml));

            foreach (XmlNode xmlNode in xmlFile.SelectNodes("Settings/Setting"))
                SettingsData.Add(xmlNode);
        }
    }
}
