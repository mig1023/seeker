using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using Seeker.Game;
using Seeker.Output;

namespace Seeker.Prototypes
{
    class Paragraphs : Abstract.IParagraphs
    {
        public static Constants StaticInstance = new Constants();
        public static Constants GetInstance() => StaticInstance;

        internal Random random = new Random();

        public virtual Abstract.IActions ActionParse(XmlNode xmlAction) =>
            null;

        public virtual Option OptionParse(XmlNode xmlOption) =>
            OptionsTemplate(xmlOption);

        public virtual Abstract.IModification ModificationParse(XmlNode xmlxmlModification) =>
            null;

        public virtual Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Paragraph paragraph = new Paragraph { Options = new List<Option>() };

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/*"))
            {
                Option option = new Option
                {
                    Goto = GetGoto(xmlOption),
                    Text = Xml.OptionTextParse(xmlOption),
                };

                paragraph.Options.Add(option);
            }

            return paragraph;
        }

        public Paragraph Get(XmlNode xmlParagraph) =>
            Get(xmlParagraph, ParagraphTemplate(xmlParagraph));

        public Paragraph Get(XmlNode xmlParagraph, Paragraph paragraph)
        {
            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/*"))
                paragraph.Options.Add(OptionParse(xmlOption));

            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/*"))
                paragraph.Actions.Add(ActionParse(xmlAction));

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/*"))
                paragraph.Modification.Add(ModificationParse(xmlModification));

            return paragraph;
        }

        protected bool ThisIsGameover(XmlNode xmlOption) =>
            xmlOption.Name == "Gameover";

        protected bool ThisIsBack(XmlNode xmlOption) =>
            xmlOption.Name == "Back";

        protected int GetGoto(XmlNode xmlOption, int? wayBack = null)
        {
            if (ThisIsGameover(xmlOption))
            {
                return Data.Constants.GetStartParagraph();
            }
            else if (ThisIsBack(xmlOption))
            {
                return wayBack ?? 0;
            }
            else
            {
                return Xml.IntParse(xmlOption.Attributes["Goto"]);
            }
        }

        public Abstract.IActions ActionTemplate(XmlNode xmlAction, Abstract.IActions actions)
        {
            actions.Type = xmlAction.Name;
            actions.Button = Xml.StringParse(xmlAction["Button"]);
            actions.Texts = TextsParse(xmlAction);
            actions.Trigger = Xml.StringParse(xmlAction["Trigger"]);
            actions.Head = Xml.StringParse(xmlAction["Head"]);
            actions.Price = Xml.IntParse(xmlAction["Price"]);
            actions.Multiple = Xml.BoolParse(xmlAction["Multiple"]);

            return actions;
        }

        public Abstract.IActions ActionParse(XmlNode xmlAction, Abstract.IActions actions,
            List<string> paramsList, Abstract.IModification modification)
        {
            Abstract.IActions action = ActionTemplate(xmlAction, actions);

            foreach (string param in paramsList)
                SetProperty(action, param, xmlAction);

            if (xmlAction["Benefit"] != null)
                action.Benefit = ModificationParse(xmlAction["Benefit"]);

            if (action.Type == "Option")
                action.Option = OptionParse(xmlAction);

            return action;
        }

        public virtual object ModificationParse(XmlNode xmlModification, object modification)
        {
            if (xmlModification == null)
                return null;

            if (xmlModification.Attributes["Name"] == null)
                modification.GetType().GetProperty("Name").SetValue(modification, xmlModification.Name);

            foreach (string param in GetProperties(modification))
                SetPropertyByAttr(modification, param, xmlModification);

            return modification;
        }

        //public virtual List<Text> TextsParse(XmlNode xmlNode, string optionName = "")
        public virtual List<Text> TextsParse(XmlNode xmlNode, bool main = false)
        {
            //List<string> textsByProperties = Data.Actions.TextByProperties(xmlNode["Text"]);
            //string textByOption = Data.Actions.TextByOptions(optionName);

            //if (textsByProperties != null)
            //{
            //    List<Text> texts = new List<Text>();

            //    foreach (string text in textsByProperties)
            //        texts.Add(TextLine(text));

            //    return texts;
            //}
            //else if (!String.IsNullOrEmpty(optionName) && !String.IsNullOrEmpty(textByOption))
            //{
            //    return new List<Text> { TextLine(textByOption) };
            //}
            //else
            if (xmlNode["Text"] != null)
            {
                return new List<Text> { Xml.TextLineParse(xmlNode["Text"]) };
            }
            else
            {
                List<Text> texts = new List<Text>();

                foreach (XmlNode text in xmlNode.SelectNodes("Texts/Text"))
                    texts.Add(Xml.TextLineParse(text));

                return texts;
            }
        }

        public Paragraph ParagraphTemplate(XmlNode xmlParagraph) => new Paragraph
        {
            Texts = TextsParse(xmlParagraph, main: true),

            Options = new List<Option>(),
            Actions = new List<Abstract.IActions>(),
            Modification = new List<Abstract.IModification>(),

            Trigger = Xml.StringParse(xmlParagraph["Triggers"]),
            LateTrigger = Xml.StringParse(xmlParagraph["LateTriggers"]),
            Untrigger = Xml.StringParse(xmlParagraph["Untriggers"]),

            Images = Xml.ImagesParse(xmlParagraph["Images"]),
        };

        public Option OptionsTemplateWithoutGoto(XmlNode xmlOption) => new Option()
        {
            Text = Xml.OptionTextParse(xmlOption),
            Availability = Xml.StringParse(xmlOption.Attributes["Availability"]),
            Dynamic = Xml.BoolParse(xmlOption.Attributes["Dynamic"]),
            Singleton = Xml.StringParse(xmlOption.Attributes["Singleton"]),
            Input = Xml.StringParse(xmlOption.Attributes["Input"]),
            Aftertexts = TextsParse(xmlOption),
            Style = Xml.StringParse(xmlOption.Attributes["Style"]),
            Do = new List<Abstract.IModification>(),
        };
            
        public Option OptionsTemplate(XmlNode xmlOption)
        {
            Option option = OptionsTemplateWithoutGoto(xmlOption);

            option.Goto = GetGoto(xmlOption);

            return option;
        }

        public Option OptionParseWithDo(XmlNode xmlOption, List<Abstract.IModification> modifications)
        {
            Option option = OptionsTemplate(xmlOption);

            int modIndex = 0;

            foreach (XmlNode optionMod in xmlOption.SelectNodes("*"))
            {
                if (optionMod.Name.StartsWith("Text"))
                    continue;

                if (modIndex >= modifications.Count)
                    break;

                option.Do.Add(Xml.ModificationParse(optionMod, modifications[modIndex]));
                modIndex += 1;
            }

            return option;
        }

        public Option OptionParseWithDo(XmlNode xmlOption, Abstract.IModification modification)
        {
            Option option = OptionsTemplate(xmlOption);

            foreach (XmlNode optionMod in xmlOption.SelectNodes("*"))
            {
                if (!optionMod.Name.StartsWith("Text"))
                    option.Do.Add(Xml.ModificationParse(optionMod, modification));
            }

            return option;
        }

        private object PropertyByType(object action, XmlNode value, string paramName)
        {
            if (value == null)
                return null;

            PropertyInfo param = action.GetType().GetProperty(paramName);

            if (param.PropertyType == typeof(bool))
            {
                return Xml.BoolParse(value);
            }
            else if (param.PropertyType == typeof(int))
            {
                return Xml.IntParse(value);
            }
            else if (param.PropertyType == typeof(string))
            {
                return Xml.StringParse(value);
            }
            else
            {
                return null;
            }
        }

        public List<string> GetProperties(object action) =>
            action.GetType().GetProperties().Select(x => x.Name).ToList();

        public void SetProperty(object action, string param, XmlNode xmlNode)
        {
            XmlNode value = null;

            if (xmlNode[param]?.Attributes["Value"] != null)
            {
                value = xmlNode[param].Attributes["Value"];
            }
            else
            {
                value = xmlNode[param];
            }

            object propetyValue = PropertyByType(action, value, param);

            if (propetyValue != null)
                action.GetType().GetProperty(param).SetValue(action, propetyValue);
        }

        public void SetPropertyByAttr(object action, string param, XmlNode value, bool maxPrefix = false)
        {
            string propertyName = (maxPrefix && param.StartsWith("Max")) ?
                param.Substring(3) : param;

            object propertyValue = PropertyByType(action,
                value.Attributes[Services.ValueStringFuse(propertyName)], propertyName);

            if (propertyValue != null)
                action.GetType().GetProperty(param).SetValue(action, propertyValue);
        }
    }
}
