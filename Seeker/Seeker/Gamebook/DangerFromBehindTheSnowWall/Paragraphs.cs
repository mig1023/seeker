using System.Collections.Generic;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.DangerFromBehindTheSnowWall
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public new static Paragraphs StaticInstance = new Paragraphs();
        public new static Paragraphs GetInstance() => StaticInstance;

        public override Paragraph Get(int id, XmlNode xmlParagraph) =>
           base.Get(xmlParagraph, ParagraphTemplate(xmlParagraph));

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            foreach (string param in GetProperties(action))
                SetProperty(action, param, xmlAction);

            if (xmlAction["Enemy"] != null)
            {
                action.Enemies = new List<Character> { EnemyParse(xmlAction["Enemy"]) };
            }
            else if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    action.Enemies.Add(EnemyParse(xmlEnemy));
            }

            if (action.Type == "Option")
                action.Option = OptionParse(xmlAction);

            return action;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Xml.ModificationParse(xmlModification, new Modification());

        private Character EnemyParse(XmlNode xmlEnemy)
        {
            Character enemy = new Character();

            foreach (string param in GetProperties(enemy))
                SetPropertyByAttr(enemy, param, xmlEnemy, maxPrefix: true);

            enemy.Skill = enemy.MaxSkill;
            enemy.Strength = enemy.MaxStrength;

            return enemy;
        }
    }
}