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

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph) => GetTemplate(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.Stat = Game.Xml.StringParse(xmlAction["Stat"]);
            action.Level = Game.Xml.IntParse(xmlAction["Level"]);
            action.GreatKhanSpecialCheck = Game.Xml.BoolParse(xmlAction["GreatKhanSpecialCheck"]);
            action.GuessBonus = Game.Xml.BoolParse(xmlAction["GuessBonus"]);
            action.Benefit = ModificationParse(xmlAction["Benefit"]);

            if (action.Name == "Option")
                action.Option = OptionParse(xmlAction["Option"]);

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption) => OptionParseWithDo(xmlOption, new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlNode)
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
