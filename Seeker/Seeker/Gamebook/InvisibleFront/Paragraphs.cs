﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;


namespace Seeker.Gamebook.InvisibleFront
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Game.Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/Option"))
                paragraph.Options.Add(OptionParse(xmlOption));

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
                paragraph.Modification.Add(ModificationParse(xmlModification));

            return paragraph;
        }

        public override Option OptionParse(XmlNode xmlOption)
        {
            Option option = OptionsTemplate(xmlOption);

            if (xmlOption.Attributes["Do"] != null)
            {
                Modification modification = new Modification { Name = Game.Xml.StringParse(xmlOption.Attributes["Do"]) };

                ValueParse(xmlOption, ref modification);

                option.Do = modification;
            }

            return option;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification
            {
                Name = Game.Xml.StringParse(xmlNode.Attributes["Name"]),
                Value = Game.Xml.IntParse(xmlNode.Attributes["Value"]),
            };

            return modification;
        }

        private static void ValueParse(XmlNode xmlOption, ref Modification modification)
        {
            if (xmlOption.Attributes["Value"] == null)
                return;

            if (int.TryParse(xmlOption.Attributes["Value"].Value, out _))
                modification.Value = Game.Xml.IntParse(xmlOption.Attributes["Value"]);
            else
                modification.ValueString = Game.Xml.StringParse(xmlOption.Attributes["Value"]);
        }
    }
}
