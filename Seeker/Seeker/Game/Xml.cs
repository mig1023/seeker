using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Xamarin.Forms;
using Seeker.Gamebook;
using Seeker.Output;

namespace Seeker.Game
{
    class Xml
    {
        private static Dictionary<string, XmlNode> Descriptions { get; set; }
        private static List<XmlNode> SettingsData { get; set; }

        public static int IntParse(string text) =>
            int.TryParse(text, out int value) ? value : 0;

        public static int IntParse(XmlNode xmlNode) =>
            IntParse(xmlNode?.InnerText ?? "0");

        public static string StringParse(XmlNode xmlNode) =>
            xmlNode?.InnerText ?? String.Empty;

        public static Dictionary<string, string> ImagesParse(XmlNode xmlNode)
        {
            Dictionary<string, string> images = new Dictionary<string, string>();

            if (xmlNode == null)
                return images;

            foreach (XmlNode xmlImage in xmlNode.SelectNodes("Image"))
                images.Add(StringParse(xmlImage.Attributes["Image"]), StringParse(xmlImage.SelectSingleNode("Text")));

            return images;
        }

        public static bool BoolParse(XmlNode xmlNode) =>
            xmlNode != null;

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

            if (xmlNode.Attributes["Val"] == null)
                return modification;

            string value = xmlNode.Attributes["Val"].InnerText;

            if (int.TryParse(value, out _))
                modification.Value = IntParse(value);
            else
                modification.ValueString = value;

            return modification;
        }

        private static Text TextLineParse(XmlNode text)
        {
            StringComparer ignoreCase = StringComparer.CurrentCultureIgnoreCase;
            List<string> style = StringParse(text.Attributes["Style"]).Split(',').Select(x => x.Trim()).ToList();

            Text output = new Text
            {
                Content = text.InnerText,
                Bold = style.Contains("Bold", ignoreCase),
                Italic = style.Contains("Italic", ignoreCase),
                Selected = style.Contains("Selected", ignoreCase),
                Box = style.Contains("Box", ignoreCase),
            };

            output.Size = Interface.TextFontSize.nope;

            foreach (string styleLine in style)
            {
                if (Enum.TryParse(styleLine, out Interface.TextFontSize size))
                    output.Size = size;

                if ((styleLine == "Center") || (styleLine == "Right"))
                    output.Alignment = styleLine;
            }

            return output;
        }

        public static void AllTextParse(ref StackLayout textPlace, int id, string optionName, out string text)
        {
            text = String.Empty;

            foreach (Text texts in Xml.TextsParse(Data.XmlParagraphs[id], optionName))
            {
                textPlace.Children.Add(Interface.TextBySelect(texts));
                text += String.Format("{0}\\n\\n", texts.Content);
            }
        }

        public static List<Text> TextsParse(XmlNode xmlNode, string optionName = "")
        {
            string textByOption = Data.Actions.TextByOptions(optionName);

            if (!String.IsNullOrEmpty(optionName) && !String.IsNullOrEmpty(textByOption))
            {
                return new List<Text>
                {
                    new Text
                    {
                        Content = textByOption,
                        Size = Interface.TextFontSize.Big,
                    }
                };
            }
            else if (xmlNode["Text"] != null)
            {
                return new List<Text>
                {
                    TextLineParse(xmlNode["Text"])
                };
            }
            else
            {
                List<Text> texts = new List<Text>();
                
                foreach (XmlNode text in xmlNode.SelectNodes("Texts/Text"))
                    texts.Add(TextLineParse(text));

                return texts;
            }
        }

        public static int PlaythrougParse(XmlNode xmlNode)
        {
            foreach (string type in Constants.PLAYTHROUGH_TIME_NODE.Keys)
                if (xmlNode[type] != null)
                    return Constants.PLAYTHROUGH_TIME_NODE[type];

            return 1;
        }

        private static XmlDocument GetGamebookXmlFile(string name)
        {
            XmlDocument xmlFile = new XmlDocument();
            xmlFile.LoadXml(DependencyService.Get<Abstract.IAssets>().GetFromAssets(name));

            return xmlFile;
        }

        public static void GameLoad(string name)
        {
            Data.XmlParagraphs.Clear();
            Data.Triggers.Clear();
            Healing.Clear();

            if (String.IsNullOrEmpty(name))
                throw new Exception("Gamebook name not found");

            Description gamebook = List.GetDescription(name);
            XmlDocument xmlFile = GetGamebookXmlFile(gamebook.XmlBook);

            foreach (XmlNode xmlNode in xmlFile.SelectNodes("Gamebook/Paragraphs/Paragraph"))
                Data.XmlParagraphs.Add(Xml.IntParse(xmlNode.Attributes["No"]), xmlNode);

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
                foreach (string type in Multiples(xmlNode, "Name"))
                    Data.Constants.LoadColor(type, xmlNode.Attributes["Val"].InnerText);

            Data.Constants.LoadEnabledDisabledOption(SettingBool(xmlFile, "ShowDisabledOption"));
            Data.Constants.LoadStartParagraphOption(SettingString(xmlFile, "StartParagraph"));
            Data.Constants.LoadDefaultFontSize(SettingString(xmlFile, "DefaultFontSize"));

            foreach (XmlNode xmlNode in xmlFile.SelectNodes(Intro("Buttons/*")))
                AddButtonsTexts(xmlNode);

            foreach (XmlNode xmlNode in xmlFile.SelectNodes(Intro("Constants/List")))
            {
                List<string> values = xmlNode.Attributes["Val"].InnerText.Split(',').ToList();
                Data.Constants.LoadList(xmlNode.Attributes["Name"].InnerText, values);
            }

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
                    items = xmlNodes.ToDictionary(x => x.Attributes["Name"].InnerText, x => x.Attributes["Val"].InnerText);
                }

                Data.Constants.LoadDictionary(xmlNode.Attributes["Name"].InnerText, items);
            }
        }

        private static string ItemLineSplit(string keyValue, bool second = false)
        {
            List<string> items = keyValue.Split(':').ToList();
            return items[second ? 1 : 0];
        }

        private static string SettingString(XmlDocument xmlFile, string option) =>
            StringParse(xmlFile.SelectSingleNode(Intro(String.Format("Settings/{0}", option)))?.Attributes["Val"]) ?? String.Empty;

        private static bool SettingBool(XmlDocument xmlFile, string optionTrue) =>
            xmlFile.SelectSingleNode(Intro(String.Format("Settings/{0}", optionTrue))) != null;

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

        private static string Intro(string node) =>
            String.Format("Gamebook/Introduction/{0}", node);

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

        private static string ColorLoad(XmlNode xmlNode, string name, Data.ColorTypes defaultColor)
        {
            if (Settings.IsEnabled("WithoutStyles"))
            {
                return Prototypes.Constants.DefaultColor(defaultColor);
            }
            else
            {
                string color = StringParse(xmlNode[name]);
                return String.Format("#{0}", color);
            }
        }
 
        private static void DescriptionLoad()
        {
            Descriptions = new Dictionary<string, XmlNode>();

            XmlDocument xmlFile = new XmlDocument();
            xmlFile.LoadXml(DependencyService.Get<Abstract.IAssets>().GetFromAssets(Data.DescriptionXml));

            foreach (XmlNode xmlNode in xmlFile.SelectNodes("Gamebooks/*"))
                Descriptions.Add(xmlNode.Name, xmlNode);
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
