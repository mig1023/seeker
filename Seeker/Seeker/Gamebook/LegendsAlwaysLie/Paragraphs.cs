using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.LegendsAlwaysLie
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph) => GetTemplate(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.ConneryAttacks = Game.Xml.StringParse(xmlAction["ConneryAttacks"]);
            action.ReactionWounds = Game.Xml.StringParse(xmlAction["ReactionWounds"]);
            action.ReactionRound = Game.Xml.StringParse(xmlAction["ReactionRound"]);
            action.ReactionHit = Game.Xml.StringParse(xmlAction["ReactionHit"]);
            action.Dices = Game.Xml.IntParse(xmlAction["Dices"]);
            action.DiceBonus = Game.Xml.IntParse(xmlAction["DiceBonus"]);
            action.OnlyRounds = Game.Xml.IntParse(xmlAction["OnlyRounds"]);
            action.RoundsToWin = Game.Xml.IntParse(xmlAction["RoundsToWin"]);
            action.AttackWounds = Game.Xml.IntParse(xmlAction["AttackWounds"]);
            action.Disabled = Game.Xml.BoolParse(xmlAction["Disabled"]);
            action.IncrementWounds = Game.Xml.BoolParse(xmlAction["IncrementWounds"]);
            action.GolemFight = Game.Xml.BoolParse(xmlAction["GolemFight"]);
            action.ZombieFight = Game.Xml.BoolParse(xmlAction["ZombieFight"]);
            action.Benefit = ModificationParse(xmlAction["Benefit"]);
            action.Damage = ModificationParse(xmlAction["Damage"]);

            if (xmlAction["FoodSharing"] != null)
                action.FoodSharing = FoodSharingParse(xmlAction["FoodSharing"]);

            if (xmlAction["Specialization"] != null)
                action.Specialization = SpecializationParse(xmlAction["Specialization"]);

            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    action.Enemies.Add(EnemyParse(xmlEnemy));
            }

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

        private static Actions.FoodSharingType FoodSharingParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return Actions.FoodSharingType.KeepMyself;

            bool success = Enum.TryParse(xmlNode.InnerText, out Actions.FoodSharingType value);

            return (success ? value : Actions.FoodSharingType.KeepMyself);
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification
            {
                Name = Game.Xml.StringParse(xmlNode.Attributes["Name"]),
                Value = Game.Xml.IntParse(xmlNode.Attributes["Value"]),
                WizardWoundsPenalty = Game.Xml.IntParse(xmlNode.Attributes["WizardWoundsPenalty"]),
                ThrowerWoundsPenalty = Game.Xml.IntParse(xmlNode.Attributes["ThrowerWoundsPenalty"]),
                Empty = Game.Xml.BoolParse(xmlNode.Attributes["Empty"]),
                Init = Game.Xml.BoolParse(xmlNode.Attributes["Init"]),
            };

            return modification;
        }

        private static Character EnemyParse(XmlNode xmlEnemy) => new Character
        {
            Name = Game.Xml.StringParse(xmlEnemy.Attributes["Name"]),
            Strength = Game.Xml.IntParse(xmlEnemy.Attributes["Strength"]),
            Hitpoints = Game.Xml.IntParse(xmlEnemy.Attributes["Hitpoints"]),
        };
    }
}
