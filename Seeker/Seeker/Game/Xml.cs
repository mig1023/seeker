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

        public static string OptionTextParse(XmlNode xmlOption) =>
            xmlOption.Name == "Start" ? Constants.START_TEXT : StringParse(xmlOption.Attributes["Text"]);

        public static string StringParse(XmlNode xmlNode) =>
            xmlNode?.InnerText ?? String.Empty;

        private static List<string> AllStringParse(XmlNode single, XmlNode multiple, string path)
        {
            if (single != null)
                return new List<string> { single.InnerText };

            List<string> lines = new List<string>();

            if (multiple == null)
                return lines;

            foreach (XmlNode option in multiple.SelectNodes(path))
                lines.Add(option.InnerText);

            return lines;
        }
        
        public static string ImageName(XmlNode xmlNode) =>
            $"{Data.CurrentGamebook}_{xmlNode.Name}.jpg";

        public static Dictionary<string, string> ImagesParse(XmlNode xmlNode)
        {
            Dictionary<string, string> images = new Dictionary<string, string>();

            if (xmlNode == null)
                return images;

            foreach (XmlNode xmlImage in xmlNode.SelectNodes("*"))
                images.Add(ImageName(xmlImage), StringParse(xmlImage.SelectSingleNode("Text")));

            return images;
        }

        public static bool BoolParse(XmlNode xmlNode) =>
            xmlNode != null;

        public static Abstract.IModification ModificationParse(XmlNode xmlNode,
            Abstract.IModification modification)
        {
            if (xmlNode == null)
                return null;

            modification.Name = xmlNode.Attributes["Name"] != null ?
                xmlNode.Attributes["Name"].InnerText : xmlNode.Name;
            
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

        private static Text TextLineParse(XmlNode text)
        {
            StringComparer ignoreCase = StringComparer.CurrentCultureIgnoreCase;

            List<string> style = StringParse(text.Attributes["Style"])
                .Split(',')
                .Select(x => x.Trim())
                .ToList();

            Text output = new Text
            {
                Content = text.InnerText,
                Bold = style.Contains("Bold", ignoreCase),
                Italic = style.Contains("Italic", ignoreCase),
                Selected = style.Contains("Selected", ignoreCase),
                Box = style.Contains("Box", ignoreCase),
            };

            output.Size = Interface.TextFontSize.nope;
            output.Background = null;

            foreach (string styleLine in style)
            {
                if (Enum.TryParse(styleLine, out Interface.TextFontSize size))
                    output.Size = size;

                if ((styleLine == "Center") || (styleLine == "Right"))
                    output.Alignment = styleLine;

                if (styleLine.Contains("Background"))
                {
                    List<string> color = styleLine
                        .Split(':')
                        .Select(x => x.Trim())
                        .ToList();

                    output.Background = color[1];
                }
            }

            return output;
        }

        public static void AllTextParse(ref StackLayout textPlace,
            int id, string optionName, out string text)
        {
            text = String.Empty;

            foreach (Text texts in Xml.TextsParse(Data.XmlParagraphs[id], optionName))
            {
                textPlace.Children.Add(Interface.TextBySelect(texts));
                text += $"{texts.Content}\\n\\n";
            }
        }

        private static Text TextLine(string line) => new Text
        {
            Content = line,
            Size = Interface.TextFontSize.Big,
        };

        public static List<Text> TextsParse(XmlNode xmlNode, string optionName = "")
        {
            List<string> textsByProperties = Data.Actions.TextByProperties(xmlNode["Text"]);
            string textByOption = Data.Actions.TextByOptions(optionName);

            if (textsByProperties != null)
            {
                List<Text> texts = new List<Text>();

                foreach (string text in textsByProperties)
                    texts.Add(TextLine(text));

                return texts;
            }
            else if (!String.IsNullOrEmpty(optionName) && !String.IsNullOrEmpty(textByOption))
            {
                return new List<Text> { TextLine(textByOption) };
            }
            else if (xmlNode["Text"] != null)
            {
                return new List<Text> { TextLineParse(xmlNode["Text"]) };
            }
            else
            {
                List<Text> texts = new List<Text>();
                
                foreach (XmlNode text in xmlNode.SelectNodes("Texts/Text"))
                    texts.Add(TextLineParse(text));

                return texts;
            }
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

            foreach (XmlNode xmlNode in xmlFile.SelectNodes(Intro("Styles/*")))
            {
                if (xmlNode.Attributes["Name"] != null)
                {
                    foreach (string type in Multiples(xmlNode, "Name"))
                        Data.Constants.LoadColor(type, xmlNode.Attributes["Value"].InnerText);
                }
                else
                {
                    Data.Constants.LoadColor(xmlNode.Name, xmlNode.Attributes["Value"].InnerText);
                }
            }

            Data.Constants.LoadEnabledDisabledOption(SettingString(xmlFile, "DisabledOption"),
                SettingString(xmlFile, "DisabledOption", specific: true));

            Data.Constants.LoadStartParagraphOption(SettingString(xmlFile, "StartParagraph"));
            Data.Constants.LoadDefaultFontSize(SettingString(xmlFile, "FontSize"));
            Data.Constants.LoadAdditionalStatusesEqualParts(SettingString(xmlFile, "AdditionalStatuses"));

            foreach (XmlNode xmlNode in xmlFile.SelectNodes(Intro("Buttons/*")))
                AddButtonsTexts(xmlNode);

            foreach (XmlNode xmlNode in xmlFile.SelectNodes(Intro("Lists/*")))
            {
                List<string> values = xmlNode.Attributes["Items"].InnerText.Split(',').ToList();
                Data.Constants.LoadList(xmlNode.Name, values);
            }

            foreach (XmlNode xmlNode in xmlFile.SelectNodes(Intro("Dictionaries/*")))
            {
                Dictionary<string, string> items = new Dictionary<string, string>();

                if (xmlNode.Attributes["ItemsByOrder"] != null)
                {
                    List<string> lines = xmlNode.Attributes["ItemsByOrder"]
                        .InnerText
                        .Split(',')
                        .Select(x => x.Trim())
                        .ToList();

                    for (int i = 1; i <= lines.Count; i++)
                    {
                        if (!String.IsNullOrEmpty(lines[i - 1]))
                            items.Add(i.ToString(), lines[i - 1]);
                    }
                }
                else if (xmlNode.Attributes["Items"] != null)
                {
                    List<string> lines = xmlNode.Attributes["Items"]
                        .InnerText
                        .Split(',')
                        .Select(x => x.Trim())
                        .ToList();

                    items = lines
                        .ToDictionary(x => ItemLineSplit(x), x => ItemLineSplit(x, second: true));
                }
                else
                {
                    List<XmlNode> xmlNodes = xmlNode
                        .SelectNodes("Item")
                        .Cast<XmlNode>()
                        .ToList();

                    items = xmlNodes
                        .ToDictionary(x => x.Attributes["Name"].InnerText, x => x.Attributes["Value"].InnerText);
                }

                Data.Constants.LoadDictionary(xmlNode.Name, items);
            }
        }

        private static string ItemLineSplit(string keyValue, bool second = false)
        {
            List<string> items = keyValue.Split(':').ToList();
            return items[second ? 1 : 0];
        }

        private static string SettingString(XmlDocument xmlFile, string option, bool specific = false)
        {
            string path = Intro($"Default/{option}");
            string attribute = specific ? "Specific" : "Value";

            return StringParse(xmlFile.SelectSingleNode(path)?.Attributes[attribute]);
        }

        private static void AddButtonsTexts(XmlNode xmlNode)
        {
            if (xmlNode.Attributes["Action"] == null)
            {
                Data.Constants.LoadButtonText(xmlNode.Name, xmlNode.InnerText);
            }
            else
            {
                foreach (string type in Multiples(xmlNode, "Action"))
                    Data.Constants.LoadButtonText(type, xmlNode.InnerText);
            }
        }

        private static List<string> Multiples(XmlNode xmlNode, string attributes) =>
            xmlNode.Attributes[attributes].InnerText.Split(',').Select(x => x.Trim()).ToList();

        private static string Intro(string node) =>
            $"Gamebook/Introduction/{node}";

        public static void GetXmlDescriptionData(ref Description description)
        {
            if (Descriptions == null)
                DescriptionLoad();

            if (!Descriptions.ContainsKey(description.Book))
                return;

            XmlNode data = Descriptions[description.Book];

            description.Title = StringParse(data["Title"]);
            description.Original = StringParse(data["Original"]);
            description.Authors = AllStringParse(data["Author"], data["Authors"], "Author");
            description.SinglePseudonym = BoolParse(data["SinglePseudonym"]);
            description.FullPseudonym = BoolParse(data["FullPseudonym"]);
            description.ConfusionOfAuthors = BoolParse(data["ConfusionOfAuthors"]);
            description.Translators = AllStringParse(data["Translator"], data["Translators"], "Translator");
            description.Year = IntParse(data["Year"]);
            description.Text = StringParse(data["Text"]);
            description.Paragraphs = StringParse(data["Paragraphs"]);
            description.OnlyFirstParagraphsValue = BoolParse(data["OnlyFirstParagraphsValue"]);
            description.Size = StringParse(data["Size"]);
            description.PlaythroughTime = StringParse(data["Playthrough"]);
            description.Setting = StringParse(data["Setting"]);


            XmlNode colors = data.SelectSingleNode("Colors");

            if ((colors != null) || Settings.IsEnabled("WithoutStyles"))
            {
                description.BookColor = ColorLoad(colors, "Book", Data.ColorTypes.BookColor);
                description.FontColor = ColorLoad(colors, "Font", Data.ColorTypes.BookFontColor);
                description.BorderColor = ColorLoad(colors, "Border", Data.ColorTypes.BookBorderColor);
            }
            else
            {
                description.BookColor = ColorLoad(data, "Color", Data.ColorTypes.BookColor);
            }
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
                return $"#{StringParse(xmlNode[name])}";
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
