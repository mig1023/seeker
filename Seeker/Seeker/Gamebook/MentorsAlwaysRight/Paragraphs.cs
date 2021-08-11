using System;
using System.Collections.Generic;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.MentorsAlwaysRight
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph) => base.Get(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.OnlyRounds = Xml.IntParse(xmlAction["OnlyRounds"]);
            action.RoundsToWin = Xml.IntParse(xmlAction["RoundsToWin"]);
            action.RoundsWinToWin = Xml.IntParse(xmlAction["RoundsWinToWin"]);
            action.ThisIsSpell = Xml.BoolParse(xmlAction["ThisIsSpell"]);
            action.Regeneration = Xml.BoolParse(xmlAction["Regeneration"]);
            action.ReactionFight = Xml.BoolParse(xmlAction["ReactionFight"]);
            action.Poison = Xml.BoolParse(xmlAction["Poison"]);
            action.OnlyOne = Xml.StringParse(xmlAction["OnlyOne"]);
            action.TailAttack = Xml.BoolParse(xmlAction["TailAttack"]);
            action.IncrementWounds = Xml.BoolParse(xmlAction["IncrementWounds"]);
            action.ThreeWoundLimit = Xml.BoolParse(xmlAction["ThreeWoundLimit"]);
            action.Invincible = Xml.BoolParse(xmlAction["Invincible"]);
            action.Wound = Xml.IntParse(xmlAction["Wound"]);
            action.Dices = Xml.IntParse(xmlAction["Dices"]);
            action.EvenWound = Xml.BoolParse(xmlAction["EvenWound"]);
            action.WoundsLimit = Xml.IntParse(xmlAction["WoundsLimit"]);
            action.DeathLimit = Xml.IntParse(xmlAction["DeathLimit"]);
            action.ReactionWounds = Xml.StringParse(xmlAction["ReactionWounds"]);

            action.Benefit = ModificationParse(xmlAction["Benefit"]);
            action.Damage = ModificationParse(xmlAction["Damage"]);

            if (xmlAction["Specialization"] != null)
                action.Specialization = SpecializationParse(xmlAction["Specialization"]);

            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    action.Enemies.Add(EnemyParse(xmlEnemy));
            }

            if (action.Name == "Option")
                action.Option = OptionParse(xmlAction["Option"]);

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption) => OptionParseWithDo(xmlOption, new Modification());

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

        public override Abstract.IModification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification
            {
                Name = Xml.StringParse(xmlNode.Attributes["Name"]),
                Value = Xml.IntParse(xmlNode.Attributes["Value"]),
                ValueString = Xml.StringParse(xmlNode.Attributes["ValueString"]),
                Empty = Xml.BoolParse(xmlNode.Attributes["Empty"]),
                Restore = Xml.BoolParse(xmlNode.Attributes["Restore"]),
            };

            return modification;
        }
    }
}
