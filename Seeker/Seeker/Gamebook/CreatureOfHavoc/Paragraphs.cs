﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.CreatureOfHavoc
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Game.Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/Option"))
            {
                Option option = OptionsTemplate(xmlOption);

                if (int.TryParse(xmlOption.Attributes["Destination"].Value, out int _))
                    option.Destination = Game.Xml.IntParse(xmlOption.Attributes["Destination"]);
                else
                {
                    List<string> destinations = xmlOption.Attributes["Destination"].Value.Split(',').ToList<string>();
                    option.Destination = int.Parse(destinations[random.Next(destinations.Count())]);
                }

                if (xmlOption.Attributes["Do"] != null)
                    option.Do = Game.Xml.ModificationParse(xmlOption, new Modification(), name: "Do");

                paragraph.Options.Add(option);
            }

            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/Action"))
                paragraph.Actions.Add(ActionParse(xmlAction));

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
                paragraph.Modification.Add(ModificationParse(xmlModification));

            return paragraph;
        }

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.WoundsToWin = Game.Xml.IntParse(xmlAction["WoundsToWin"]);
            action.RoundsToWin = Game.Xml.IntParse(xmlAction["RoundsToWin"]);
            action.RoundsToFight = Game.Xml.IntParse(xmlAction["RoundsToFight"]);
            action.Ophidiotaur = Game.Xml.BoolParse(xmlAction["Ophidiotaur"]);
            action.ManicBeast = Game.Xml.BoolParse(xmlAction["ManicBeast"]);
            action.GiantHornet = Game.Xml.BoolParse(xmlAction["GiantHornet"]);

            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    action.Enemies.Add(EnemyParse(xmlEnemy));
            }

            return action;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) => new Modification
        {
            Name = Game.Xml.StringParse(xmlModification.Attributes["Name"]),
            Value = Game.Xml.IntParse(xmlModification.Attributes["Value"]),
            Restore = Game.Xml.BoolParse(xmlModification.Attributes["Restore"]),
        };

        private static Character EnemyParse(XmlNode xmlEnemy)
        {
            Character enemy = new Character
            {
                Name = Game.Xml.StringParse(xmlEnemy.Attributes["Name"]),
                MaxMastery = Game.Xml.IntParse(xmlEnemy.Attributes["Mastery"]),
                MaxEndurance = Game.Xml.IntParse(xmlEnemy.Attributes["Endurance"]),
            };

            enemy.Mastery = enemy.MaxMastery;
            enemy.Endurance = enemy.MaxEndurance;

            return enemy;
        }
    }
}
