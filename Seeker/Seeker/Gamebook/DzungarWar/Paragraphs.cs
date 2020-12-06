﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.DzungarWar
{
    class Paragraphs : Abstract.IParagraphs
    {
        public Game.Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Game.Paragraph paragraph = new Game.Paragraph();

            paragraph.Options = new List<Option>();
            paragraph.Actions = new List<Abstract.IActions>();
            paragraph.Modification = new List<Abstract.IModification>();

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/Option"))
            {
                Option option = new Option
                {
                    Destination = Game.Xml.IntParse(xmlOption.Attributes["Destination"]),
                    Text = Game.Xml.StringParse(xmlOption.Attributes["Text"]),
                    OnlyIf = Game.Xml.StringParse(xmlOption.Attributes["OnlyIf"]),
                };

                paragraph.Options.Add(option);
            }

            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/Action"))
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
                };

                paragraph.Actions.Add(action);
            }

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
            {
                Modification modification = new Modification
                {
                    Name = Game.Xml.StringParse(xmlModification.Attributes["Name"]),
                    Value = Game.Xml.IntParse(xmlModification.Attributes["Value"]),
                };

                if (xmlModification.Attributes["Empty"] != null)
                    modification.Empty = true;

                if (xmlModification.Attributes["Init"] != null)
                    modification.Init = true;

                paragraph.Modification.Add(modification);
            }

            paragraph.Trigger = Game.Xml.StringParse(xmlParagraph["Triggers"]);
            paragraph.LateTrigger = Game.Xml.StringParse(xmlParagraph["LateTriggers"]);
            paragraph.RemoveTrigger = Game.Xml.StringParse(xmlParagraph["RemoveTriggers"]);
            paragraph.Image = Game.Xml.StringParse(xmlParagraph["Image"]);

            return paragraph;
        }
    }
}
