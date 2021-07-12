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
            action.Benefit = ModificationParse(xmlAction["Benefit"]);
            action.ThisIsSpell = Game.Xml.BoolParse(xmlAction["ThisIsSpell"]);
            action.Regeneration = Game.Xml.BoolParse(xmlAction["Regeneration"]);
            action.Wound = Game.Xml.IntParse(xmlAction["Wound"]);
            action.EvenWound = Game.Xml.BoolParse(xmlAction["EvenWound"]);
            action.WoundsLimit = Game.Xml.IntParse(xmlAction["WoundsLimit"]);
            action.ReactionWounds = Game.Xml.StringParse(xmlAction["ReactionWounds"]);

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
            Hitpoints = Game.Xml.IntParse(xmlEnemy.Attributes["Hitpoints"]),
        };
    }
}
