﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.CreatureOfHavoc
{
    class Paragraphs : Abstract.IParagraphs
    {
        private Random random = new Random();

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
                    Text = Game.Xml.StringParse(xmlOption.Attributes["Text"], defaultText: "Далее"),
                    OnlyIf = Game.Xml.StringParse(xmlOption.Attributes["OnlyIf"]),
                };

                if (int.TryParse(xmlOption.Attributes["Destination"].Value, out int _))
                    option.Destination = Game.Xml.IntParse(xmlOption.Attributes["Destination"]);
                else
                {
                    List<string> destinations = xmlOption.Attributes["Destination"].Value.Split(',').ToList<string>();
                    option.Destination = int.Parse(destinations[random.Next(destinations.Count())]);
                    option.Text += "  ⚄";
                }

                if (xmlOption.Attributes["Do"] != null)
                {
                    Modification modification = new Modification
                    {
                        Name = Game.Xml.StringParse(xmlOption.Attributes["Do"]),
                        Value = Game.Xml.IntParse(xmlOption.Attributes["Value"]),
                    };

                    option.Do = modification;
                }

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

                    WoundsToWin = Game.Xml.IntParse(xmlAction["WoundsToWin"]),
                    RoundsToWin = Game.Xml.IntParse(xmlAction["RoundsToWin"]),

                    Ophidiotaur = Game.Xml.BoolParse(xmlAction["Ophidiotaur"]),
                    ManicBeast = Game.Xml.BoolParse(xmlAction["ManicBeast"]),
                    GiantHornet = Game.Xml.BoolParse(xmlAction["GiantHornet"]),
                };

                if (xmlAction["Enemies"] != null)
                {
                    action.Enemies = new List<Character>();

                    foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    {
                        Character enemy = new Character
                        {
                            Name = Game.Xml.StringParse(xmlEnemy.Attributes["Name"]),
                            Mastery = Game.Xml.IntParse(xmlEnemy.Attributes["Mastery"]),
                            Endurance = Game.Xml.IntParse(xmlEnemy.Attributes["Endurance"]),
                        };

                        action.Enemies.Add(enemy);
                    }
                }

                paragraph.Actions.Add(action);
            }

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
            {
                Modification modification = new Modification
                {
                    Name = Game.Xml.StringParse(xmlModification.Attributes["Name"]),
                    Value = Game.Xml.IntParse(xmlModification.Attributes["Value"]),
                };

                paragraph.Modification.Add(modification);
            }

            paragraph.Trigger = Game.Xml.StringParse(xmlParagraph["Triggers"]);
            paragraph.LateTrigger = Game.Xml.StringParse(xmlParagraph["LateTriggers"]);

            return paragraph;
        }
    }
}
