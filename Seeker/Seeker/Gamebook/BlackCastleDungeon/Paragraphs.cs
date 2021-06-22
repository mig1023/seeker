using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;


namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph) => GetTemplate(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.RoundsToWin = Game.Xml.IntParse(xmlAction["RoundsToWin"]);
            action.WoundsToWin = Game.Xml.IntParse(xmlAction["WoundsToWin"]);
            action.ThisIsSpell = Game.Xml.BoolParse(xmlAction["ThisIsSpell"]);

            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    action.Enemies.Add(EnemyParse(xmlEnemy));
            }

            action.Benefit = Game.Xml.ModificationParse(xmlAction["Benefit"], new Modification());

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption)
        {
            Option option = OptionsTemplate(xmlOption);

            if (xmlOption.Attributes["Do"] != null)
            {
                Modification modification = new Modification
                {
                    Name = Game.Xml.StringParse(xmlOption.Attributes["Do"]),
                    Value = Game.Xml.IntParse(xmlOption.Attributes["Value"]),
                    ValueString = Game.Xml.StringParse(xmlOption.Attributes["ValueString"]),
                };

                option.Do = modification;
            }

            return option;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Game.Xml.ModificationParse(xmlModification, new Modification());

        private static Character EnemyParse(XmlNode xmlEnemy)
        {
            Character enemy = new Character
            {
                Name = Game.Xml.StringParse(xmlEnemy.Attributes["Name"]),
                MaxMastery = Game.Xml.IntParse(xmlEnemy.Attributes["Mastery"]),
                MaxEndurance = Game.Xml.IntParse(xmlEnemy.Attributes["Endurance"]),
            };

            enemy.Mastery = enemy.MaxMastery;
            enemy.Endurance = enemy.MaxEndurance;

            return enemy;
        }
    }
}
