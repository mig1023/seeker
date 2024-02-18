using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Seeker.Game;
using Seeker.Output;

namespace Seeker.Gamebook.PresidentSimulator
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public new static Paragraphs StaticInstance = new Paragraphs();
        public new static Paragraphs GetInstance() => StaticInstance;
        private static Character protagonist = Character.Protagonist;

        public override Paragraph Get(int id, XmlNode xmlParagraph) =>
            base.Get(xmlParagraph, ParagraphTemplate(xmlParagraph));

        private List<string> TextByProperties(XmlNode text)
        {
            if ((text == null) || (text.InnerText != "[text by propertires]"))
                return null;

            int part = Option.IsTriggered("полугодие") ? 2 : 1;

            if ((protagonist.Year == 1980) && (part == 2))
            {
                part = Option.IsTriggered("Ультраправые террористы") ? 3 : 2;
            }
            else if ((protagonist.Year == 1983) && (part == 1))
            {
                part = Option.IsTriggered("Повстанцы-коммунисты") ? 1 : 3;
            }

            string yearPart = $"{protagonist.Year}-{part}";
            string yearLine = Constants.TextByYears[yearPart];
            return yearLine.Split('|').ToList();
        }

        public override List<Text> TextsParse(XmlNode xmlNode, bool main = false)
        {
            List<string> textsByProperties = TextByProperties(xmlNode["Text"]);

            List<Text> texts = new List<Text>();

            if (textsByProperties != null)
            {
                foreach (string text in textsByProperties)
                    texts.Add(Xml.TextLine(text));
            }
            else if (xmlNode["Text"] != null)
            {
                texts.Add(Xml.TextLineParse(xmlNode["Text"]));
            }

            return texts;
        }

        public override Option OptionParse(XmlNode xmlOption)
        {
            List<Abstract.IModification> modifications = new List<Abstract.IModification>();

            foreach (XmlNode optionMod in xmlOption.SelectNodes("*"))
            {
                if (!optionMod.Name.StartsWith("Text"))
                    modifications.Add(new Modification());
            }

            return OptionParseWithDo(xmlOption, modifications);
        }

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            foreach (string param in GetProperties(action))
                SetProperty(action, param, xmlAction);

            if (action.Type == "Option")
                action.Option = OptionParse(xmlAction);

            return action;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
           (Abstract.IModification)base.ModificationParse(xmlModification, new Modification());
    }
}
