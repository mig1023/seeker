﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.OctopusIsland
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph) => GetTemplate(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.WoundsToWin = Game.Xml.IntParse(xmlAction["WoundsToWin"]);
            action.DinnerHitpointsBonus = Game.Xml.IntParse(xmlAction["Dinner"]);

            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    action.Enemies.Add(EnemyParse(xmlEnemy));
            }

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption)
        {
            Option option = OptionsTemplateWithoutDestination(xmlOption);

            if (int.TryParse(xmlOption.Attributes["Destination"].Value, out int _))
                option.Destination = Game.Xml.IntParse(xmlOption.Attributes["Destination"]);
            else
            {
                List<string> destinations = xmlOption.Attributes["Destination"].Value.Split(',').ToList<string>();
                option.Destination = int.Parse(destinations[random.Next(destinations.Count())]);
            }

            return option;
        }

        private static Character EnemyParse(XmlNode xmlEnemy) => new Character
        {
            Name = Game.Xml.StringParse(xmlEnemy.Attributes["Name"]),
            Skill = Game.Xml.IntParse(xmlEnemy.Attributes["Skill"]),
            Hitpoint = Game.Xml.IntParse(xmlEnemy.Attributes["Hitpoint"]),
        };

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Game.Xml.ModificationParse(xmlModification, new Modification());
    }
}
