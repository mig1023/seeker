using System.Collections.Generic;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.PrairieLaw
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph) => base.Get(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.RemoveTrigger = Xml.StringParse(xmlAction["RemoveTrigger"]);
            action.SellPrices = Xml.StringParse(xmlAction["SellPrices"]);
            action.Dices = Xml.IntParse(xmlAction["Dices"]);
            action.Firefight = Xml.BoolParse(xmlAction["Firefight"]);
            action.ProtagonistWoundsLimit = Xml.BoolParse(xmlAction["HeroWoundsLimit"]);
            action.EnemyWoundsLimit = Xml.BoolParse(xmlAction["EnemyWoundsLimit"]);
            action.Roulette = Xml.BoolParse(xmlAction["Roulette"]);

            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    action.Enemies.Add(EnemyParse(xmlEnemy));
            }

            if (xmlAction["Benefit"] != null)
                action.Benefit = Xml.ModificationParse(xmlAction["Benefit"], new Modification());

            return action;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Xml.ModificationParse(xmlModification, new Modification());

        private static Character EnemyParse(XmlNode xmlEnemy)
        {
            Character enemy = new Character
            {
                Name = Xml.StringParse(xmlEnemy.Attributes["Name"]),
                MaxSkill = Xml.IntParse(xmlEnemy.Attributes["Skill"]),
                MaxStrength = Xml.IntParse(xmlEnemy.Attributes["Strength"]),
                Cartridges = Xml.IntParse(xmlEnemy.Attributes["Сartridges"]),
            };

            enemy.Skill = enemy.MaxSkill;
            enemy.Strength = enemy.MaxStrength;

            return enemy;
        }
    }
}
