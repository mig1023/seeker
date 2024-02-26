﻿using System;
using System.Collections.Generic;
using System.Xml;
using Seeker.Game;
using Seeker.Output;

namespace Seeker.Gamebook.Hunt
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public override Paragraph Get(int id, XmlNode xmlParagraph) =>
           base.Get(xmlParagraph, ParagraphTemplate(xmlParagraph));

        public override List<Text> TextsParse(XmlNode xmlNode, bool main = false)
        {
            if (xmlNode["Text"] != null)
            {
                return new List<Text> { Xml.TextLineParse(xmlNode["Text"]) };
            }
            else
            {
                List<Text> texts = new List<Text>();

                foreach (XmlNode text in xmlNode.SelectNodes("Texts/Text"))
                {
                    string option = text.Attributes["Availability"].InnerText;
                    bool isOptioned = !String.IsNullOrEmpty(option);

                    if (isOptioned && option.Contains("!"))
                    {
                        if (Option.IsTriggered(option.Replace("!", String.Empty).Trim()))
                            continue;
                    }
                    else if (isOptioned && !Option.IsTriggered(option))
                    {
                        continue;
                    }

                    texts.Add(Xml.TextLineParse(text));
                }

                return texts;
            }
        }

        public override Option OptionParse(XmlNode xmlOption) =>
            OptionParseWithDo(xmlOption, new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Xml.ModificationParse(xmlModification, new Modification());
    }
}