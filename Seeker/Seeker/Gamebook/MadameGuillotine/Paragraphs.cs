using System;
using System.Collections.Generic;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.MadameGuillotine
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public new static Paragraphs StaticInstance = new Paragraphs();
        public new static Paragraphs GetInstance() => StaticInstance;

        public override Paragraph Get(int id, XmlNode xmlParagraph) =>
            base.Get(xmlParagraph);

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

        public override Option OptionParse(XmlNode xmlOption) =>
            OptionParseWithDo(xmlOption, new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
           (Abstract.IModification)base.ModificationParse(xmlModification, new Modification());

        private static Character EnemyParse(XmlNode xmlEnemy) => new Character
        {
            Name = Xml.StringParse(xmlEnemy.Attributes["Name"]),
            Weapon = Xml.StringParse(xmlEnemy.Attributes["Weapon"]),
            Skill = Xml.IntParse(xmlEnemy.Attributes["Skill"]),
            Hitpoints = Xml.IntParse(xmlEnemy.Attributes["Hitpoints"]),
            Wounds = 0,
        };
    }
}