﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.AdventuresOfABeardlessDeceiver
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Game.Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/Option"))
                paragraph.Options.Add(OptionsTemplate(xmlOption));

            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/Action"))
                paragraph.Actions.Add(ActionParse(xmlAction));

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
                paragraph.Modification.Add(ModificationParse(xmlModification));

            return paragraph;
        }

        private Actions ActionParse(XmlNode xmlAction)
        {
            Actions action = new Actions
            {
                ActionName = Game.Xml.StringParse(xmlAction["ActionName"]),
                ButtonName = Game.Xml.StringParse(xmlAction["ButtonName"]),
                Aftertext = Game.Xml.StringParse(xmlAction["Aftertext"]),
                Trigger = Game.Xml.StringParse(xmlAction["Trigger"]),
                Text = Game.Xml.StringParse(xmlAction["Text"]),
                Stat = Game.Xml.StringParse(xmlAction["Stat"]),

                Price = Game.Xml.IntParse(xmlAction["Price"]),
                Level = Game.Xml.IntParse(xmlAction["Level"]),

                GreatKhanSpecialCheck = Game.Xml.BoolParse(xmlAction["GreatKhanSpecialCheck"]),
                GuessBonus = Game.Xml.BoolParse(xmlAction["GuessBonus"]),

                Benefit = ModificationParse(xmlAction["Benefit"]),
            };

            if (action.ActionName == "Option")
                action.Option = OptionParse(xmlAction["Option"]);

            return action;
        }

        private Option OptionParse(XmlNode xmlOption)
        {
            Option option = OptionsTemplate(xmlOption);

            if (xmlOption.Attributes["Do"] != null)
                option.Do = Game.Xml.ModificationParse(xmlOption, new Modification(), name: "Do");

            return option;
        }

        private static Modification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification
            {
                Name = Game.Xml.StringParse(xmlNode.Attributes["Name"]),
                Value = Game.Xml.IntParse(xmlNode.Attributes["Value"]),
                Empty = Game.Xml.BoolParse(xmlNode.Attributes["Empty"]),
                Init = Game.Xml.BoolParse(xmlNode.Attributes["Init"]),
            };

            return modification;
        }
    }
}
