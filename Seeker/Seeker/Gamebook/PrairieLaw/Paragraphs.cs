using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.PrairieLaw
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
                SellPrices = Game.Xml.StringParse(xmlAction["SellPrices"]),
                Dices = Game.Xml.IntParse(xmlAction["Dices"]),
                Price = Game.Xml.IntParse(xmlAction["Price"]),
                Firefight = Game.Xml.BoolParse(xmlAction["Firefight"]),
                HeroWoundsLimit = Game.Xml.BoolParse(xmlAction["HeroWoundsLimit"]),
                EnemyWoundsLimit = Game.Xml.BoolParse(xmlAction["EnemyWoundsLimit"]),
                Multiple = Game.Xml.BoolParse(xmlAction["Multiple"]),
                Roulette = Game.Xml.BoolParse(xmlAction["Roulette"]),
            };

            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    action.Enemies.Add(EnemyParse(xmlEnemy));
            }

            if (xmlAction["Benefit"] != null)
                action.Benefit = Game.Xml.ModificationParse(xmlAction["Benefit"], new Modification());

            return action;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Game.Xml.ModificationParse(xmlModification, new Modification());

        private static Character EnemyParse(XmlNode xmlEnemy)
        {
            Character enemy = new Character
            {
                Name = Game.Xml.StringParse(xmlEnemy.Attributes["Name"]),
                MaxSkill = Game.Xml.IntParse(xmlEnemy.Attributes["Skill"]),
                MaxStrength = Game.Xml.IntParse(xmlEnemy.Attributes["Strength"]),
                Cartridges = Game.Xml.IntParse(xmlEnemy.Attributes["Сartridges"]),
            };

            enemy.Skill = enemy.MaxSkill;
            enemy.Strength = enemy.MaxStrength;

            return enemy;
        }
    }
}
