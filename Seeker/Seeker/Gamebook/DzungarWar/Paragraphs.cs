﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.DzungarWar
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph) => GetTemplate(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = new Actions
            {
                ActionName = Game.Xml.StringParse(xmlAction["ActionName"]),
                ButtonName = Game.Xml.StringParse(xmlAction["ButtonName"]),
                Aftertext = Game.Xml.StringParse(xmlAction["Aftertext"]),
                Trigger = Game.Xml.StringParse(xmlAction["Trigger"]),
                RemoveTrigger = Game.Xml.StringParse(xmlAction["RemoveTrigger"]),
                Text = Game.Xml.StringParse(xmlAction["Text"]),
                Stat = Game.Xml.StringParse(xmlAction["Stat"]),
                TriggerTestPenalty = Game.Xml.StringParse(xmlAction["TriggerTestPenalty"]),
                Price = Game.Xml.IntParse(xmlAction["Price"]),
                Level = Game.Xml.IntParse(xmlAction["Level"]),
                StatStep = Game.Xml.IntParse(xmlAction["StatStep"]),
                StatToMax = Game.Xml.BoolParse(xmlAction["StatToMax"]),
                Benefit = ModificationParse(xmlAction["Benefit"]),
            };

            if (action.ActionName == "Option")
                action.Option = OptionParse(xmlAction["Option"]);

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption)
        {
            Option option = OptionsTemplate(xmlOption);

            if (xmlOption.Attributes["Do"] != null)
                option.Do = Game.Xml.ModificationParse(xmlOption, new Modification(), name: "Do");

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
                Empty = Game.Xml.BoolParse(xmlNode.Attributes["Empty"]),
                Init = Game.Xml.BoolParse(xmlNode.Attributes["Init"]),
            };

            return modification;
        }
    }
}
