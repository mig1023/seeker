using System.Collections.Generic;
using System.Xml;
using Seeker.Game;


namespace Seeker.Gamebook.BlackCastleDungeon
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
            action.StrengthPenlty = Xml.IntParse(xmlAction["StrengthPenlty"]);
            action.ThisIsSpell = Xml.BoolParse(xmlAction["ThisIsSpell"]);

            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    action.Enemies.Add(EnemyParse(xmlEnemy));
            }


            if (action.Name == "Option")
                action.Option = OptionParse(xmlAction["Option"]);

            action.Benefit = Xml.ModificationParse(xmlAction["Benefit"], new Modification());

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption) => OptionParseWithDo(xmlOption, new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Xml.ModificationParse(xmlModification, new Modification());

        private static Character EnemyParse(XmlNode xmlEnemy)
        {
            Character enemy = new Character
            {
                Name = Xml.StringParse(xmlEnemy.Attributes["Name"]),
                MaxMastery = Xml.IntParse(xmlEnemy.Attributes["Mastery"]),
                MaxEndurance = Xml.IntParse(xmlEnemy.Attributes["Endurance"]),
            };

            enemy.Mastery = enemy.MaxMastery;
            enemy.Endurance = enemy.MaxEndurance;

            return enemy;
        }
    }
}
