using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.MentorsAlwaysRight
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph) => GetTemplate(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.OnlyRounds = Game.Xml.IntParse(xmlAction["OnlyRounds"]);
            action.RoundsToWin = Game.Xml.IntParse(xmlAction["RoundsToWin"]);
            action.RoundsWinToWin = Game.Xml.IntParse(xmlAction["RoundsWinToWin"]);
            action.ThisIsSpell = Game.Xml.BoolParse(xmlAction["ThisIsSpell"]);
            action.Regeneration = Game.Xml.BoolParse(xmlAction["Regeneration"]);
            action.ReactionFight = Game.Xml.BoolParse(xmlAction["ReactionFight"]);
            action.Poison = Game.Xml.BoolParse(xmlAction["Poison"]);
            action.OnlyOne = Game.Xml.BoolParse(xmlAction["OnlyOne"]);
            action.TailAttack = Game.Xml.BoolParse(xmlAction["TailAttack"]);
            action.IncrementWounds = Game.Xml.BoolParse(xmlAction["IncrementWounds"]);
            action.ThreeWoundLimit = Game.Xml.BoolParse(xmlAction["ThreeWoundLimit"]);
            action.Invincible = Game.Xml.BoolParse(xmlAction["Invincible"]);
            action.Wound = Game.Xml.IntParse(xmlAction["Wound"]);
            action.Dices = Game.Xml.IntParse(xmlAction["Dices"]);
            action.EvenWound = Game.Xml.BoolParse(xmlAction["EvenWound"]);
            action.WoundsLimit = Game.Xml.IntParse(xmlAction["WoundsLimit"]);
            action.DeathLimit = Game.Xml.IntParse(xmlAction["DeathLimit"]);
            action.ReactionWounds = Game.Xml.StringParse(xmlAction["ReactionWounds"]);

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
            Name = Game.Xml.StringParse(xmlEnemy.Attributes["Name"]),
            Strength = Game.Xml.IntParse(xmlEnemy.Attributes["Strength"]),
            MaxHitpoints = Game.Xml.IntParse(xmlEnemy.Attributes["Hitpoints"]),
            Hitpoints = Game.Xml.IntParse(xmlEnemy.Attributes["Hitpoints"]),
        };

        public override Abstract.IModification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification
            {
                Name = Game.Xml.StringParse(xmlNode.Attributes["Name"]),
                Value = Game.Xml.IntParse(xmlNode.Attributes["Value"]),
                ValueString = Game.Xml.StringParse(xmlNode.Attributes["ValueString"]),
                Empty = Game.Xml.BoolParse(xmlNode.Attributes["Empty"]),
                Restore = Game.Xml.BoolParse(xmlNode.Attributes["Restore"]),
            };

            return modification;
        }
    }
}
