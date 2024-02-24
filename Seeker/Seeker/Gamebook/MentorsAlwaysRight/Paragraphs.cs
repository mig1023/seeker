using System;
using System.Collections.Generic;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.MentorsAlwaysRight
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public override Game.Paragraph Get(int id, XmlNode xmlParagraph) =>
            base.Get(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            foreach (string param in GetProperties(action))
                SetProperty(action, param, xmlAction);

            action.Benefit = ModificationParse(xmlAction["Benefit"]);
            action.Damage = ModificationParse(xmlAction["Damage"]);

            if (xmlAction["Specialization"] != null)
                action.Specialization = SpecializationParse(xmlAction["Specialization"]);

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

        private static Character.SpecializationType SpecializationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return Character.SpecializationType.Nope;

            bool success = Enum.TryParse(xmlNode.InnerText, out Character.SpecializationType value);

            return (success ? value : Character.SpecializationType.Nope);
        }

        private static Character EnemyParse(XmlNode xmlEnemy) => new Character
        {
            Name = Xml.StringParse(xmlEnemy.Attributes["Name"]),
            Strength = Xml.IntParse(xmlEnemy.Attributes["Strength"]),
            MaxHitpoints = Xml.IntParse(xmlEnemy.Attributes["Hitpoints"]),
            Hitpoints = Xml.IntParse(xmlEnemy.Attributes["Hitpoints"]),
        };

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
           (Abstract.IModification)base.ModificationParse(xmlModification, new Modification());
    }
}
