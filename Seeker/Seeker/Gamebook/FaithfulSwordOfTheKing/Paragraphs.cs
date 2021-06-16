using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.FaithfulSwordOfTheKing
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
                Text = Game.Xml.StringParse(xmlAction["Text"]),
                RoundsToWin = Game.Xml.IntParse(xmlAction["RoundsToWin"]),
                WoundsToWin = Game.Xml.IntParse(xmlAction["WoundsToWin"]),
                SkillPenalty = Game.Xml.IntParse(xmlAction["SkillPenalty"]),
                Price = Game.Xml.IntParse(xmlAction["Price"]),
                MeritalArt = MeritalArtsParse(xmlAction["MeritalArt"]),
                Multiple = Game.Xml.BoolParse(xmlAction["Multiple"]),
                WithoutShooting = Game.Xml.BoolParse(xmlAction["WithoutShooting"]),
            };

            if (xmlAction["Benefit"] != null)
            {
                action.BenefitList = new List<Abstract.IModification>();

                foreach (XmlNode bonefit in xmlAction.SelectNodes("Benefit"))
                    action.BenefitList.Add(ModificationParse(bonefit));
            }

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
            Option option = OptionsTemplate(xmlOption);

            if (xmlOption.Attributes["Do"] != null)
                option.Do = Game.Xml.ModificationParse(xmlOption, new Modification(), name: "Do");

            return option;
        }

        private static Character.MeritalArts MeritalArtsParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return Character.MeritalArts.Nope;

            bool success = Enum.TryParse(xmlNode.InnerText, out Character.MeritalArts value);

            return (success ? value : Character.MeritalArts.Nope);
        }

        private static Character EnemyParse(XmlNode xmlEnemy)
        {
            Character enemy = new Character
            {
                Name = Game.Xml.StringParse(xmlEnemy.Attributes["Name"]),
                MaxSkill = Game.Xml.IntParse(xmlEnemy.Attributes["Skill"]),
                MaxStrength = Game.Xml.IntParse(xmlEnemy.Attributes["Strength"]),
            };

            enemy.Skill = enemy.MaxSkill;
            enemy.Strength = enemy.MaxStrength;

            return enemy;
        }

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
