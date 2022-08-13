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

            if (xmlNode.Attributes["Name"] != null)
                modification.Name = xmlNode.Attributes["Name"].InnerText;
            else
                modification.Name = xmlNode.Name;
            

            modification.Empty = BoolParse(xmlNode.Attributes["Empty"]);
            modification.Restore = BoolParse(xmlNode.Attributes["Restore"]);

            if (xmlNode.Attributes["Value"] == null)
                return modification;

            string value = xmlNode.Attributes["Value"].InnerText;

            if (int.TryParse(value, out _))
                modification.Value = IntParse(value);
            else
                modification.ValueString = value;

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

        public static void AllTextParse(ref StackLayout textPlace, int id, string optionName, out string text)
        {
            text = Xml.TextParse(id, optionName);

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

        public static List<Text> TextsParse(XmlNode xmlNode, bool action = false)
        {
            List<Text> texts = new List<Text>();

            foreach (XmlNode text in xmlNode.SelectNodes(String.Format("{0}/Text", action ? "After" : "Texts")))
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

            Data.Constants.LoadEnabledDisabledOption(SettingParse(xmlFile, "DisabledOption"));
            Data.Constants.LoadStartParagraphOption(SettingParse(xmlFile, "StartParagraph"));

            foreach (XmlNode xmlNode in xmlFile.SelectNodes(Intro("Buttons/*")))
                AddButtonsTexts(xmlNode);

            foreach (XmlNode xmlNode in xmlFile.SelectNodes(Intro("Constants/List")))
                Data.Constants.LoadList(xmlNode.Attributes["Name"].InnerText, xmlNode.InnerText.Split(',').ToList());

            foreach (XmlNode xmlNode in xmlFile.SelectNodes(Intro("Constants/Dictionary")))
            {
                Dictionary<string, string> items = new Dictionary<string, string>();

                if (xmlNode.Attributes["FromLineByOrder"] != null)
                {
                    List<string> lines = xmlNode.Attributes["FromLineByOrder"].InnerText.Split(',').Select(x => x.Trim()).ToList();

                    for (int i = 1; i <= lines.Count; i++)
                        if (!String.IsNullOrEmpty(lines[i - 1]))
                            items.Add(i.ToString(), lines[i - 1]);
                }
                else if (xmlNode.Attributes["FromLine"] != null)
                {
                    List<string> lines = xmlNode.Attributes["FromLine"].InnerText.Split(',').Select(x => x.Trim()).ToList();
                    items = lines.ToDictionary(x => ItemLineSplit(x), x => ItemLineSplit(x, second: true));
                }
                else
                {
                    List<XmlNode> xmlNodes = xmlNode.SelectNodes("Item").Cast<XmlNode>().ToList();
                    items = xmlNodes.ToDictionary(x => x.Attributes["Name"].InnerText, x => x.InnerText);
                }

                Data.Constants.LoadDictionary(xmlNode.Attributes["Name"].InnerText, items);
            }
        }

        private static string ItemLineSplit(string keyValue, bool second = false)
        {
            List<string> items = keyValue.Split(':').ToList();
            return items[second ? 1 : 0];
        }

        private static string SettingParse(XmlDocument xmlFile, string option) =>
            Xml.StringParse(xmlFile.SelectSingleNode(Intro(String.Format("Settings/{0}", option)))?.Attributes["Value"]) ?? String.Empty;

        private static void AddButtonsTexts(XmlNode xmlNode)
        {
            if (xmlNode.Attributes["Action"] == null)
                Data.Constants.LoadButtonText(xmlNode.Name, xmlNode.InnerText);
            else
                foreach (string type in Multiples(xmlNode, "Action"))
                    Data.Constants.LoadButtonText(type, xmlNode.InnerText);
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
            description.OnlyFirstParagraphsValue = BoolParse(data["OnlyFirstParagraphsValue"]);
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
