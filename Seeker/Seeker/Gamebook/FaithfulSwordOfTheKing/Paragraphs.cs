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

        public override Paragraph Get(int id, XmlNode xmlParagraph) => GetTemplate(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.RoundsToWin = Xml.IntParse(xmlAction["RoundsToWin"]);
            action.WoundsToWin = Xml.IntParse(xmlAction["WoundsToWin"]);
            action.SkillPenalty = Xml.IntParse(xmlAction["SkillPenalty"]);
            action.MeritalArt = MeritalArtsParse(xmlAction["MeritalArt"]);
            action.WithoutShooting = Xml.BoolParse(xmlAction["WithoutShooting"]);

            if (action.Name == "Option")
                action.Option = OptionParse(xmlAction["Option"]);

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

        public override Option OptionParse(XmlNode xmlOption) => OptionParseWithDo(xmlOption, new Modification());

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
                Name = Xml.StringParse(xmlEnemy.Attributes["Name"]),
                MaxSkill = Xml.IntParse(xmlEnemy.Attributes["Skill"]),
                MaxStrength = Xml.IntParse(xmlEnemy.Attributes["Strength"]),
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
