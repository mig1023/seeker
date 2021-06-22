using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.HowlOfTheWerewolf
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Game.Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/Option"))
            {
                Option option = OptionsTemplateWithoutDestination(xmlOption);

                if (xmlOption.Attributes["Destination"].Value == "Back")
                    option.Destination = Character.Protagonist.WayBack;
                else
                    option.Destination = Game.Xml.IntParse(xmlOption.Attributes["Destination"]);

                if (xmlOption.Attributes["Do"] != null)
                    option.Do = Game.Xml.ModificationParse(xmlOption, new Modification(), name: "Do");

                paragraph.Options.Add(option);
            }

            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/Action"))
                paragraph.Actions.Add(ActionParse(xmlAction));

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
                paragraph.Modification.Add(Game.Xml.ModificationParse(xmlModification, new Modification()));

            return paragraph;
        }

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.Value = Game.Xml.IntParse(xmlAction["Value"]);
            action.RoundsToWin = Game.Xml.IntParse(xmlAction["RoundsToWin"]);
            action.RoundsWinToWin = Game.Xml.IntParse(xmlAction["RoundsWinToWin"]);
            action.RoundsFailToFail = Game.Xml.IntParse(xmlAction["RoundsFailToFail"]);
            action.RoundsToFight = Game.Xml.IntParse(xmlAction["RoundsToFight"]);
            action.WoundsToWin = Game.Xml.IntParse(xmlAction["WoundsToWin"]);
            action.WoundsToFail = Game.Xml.IntParse(xmlAction["WoundsToFail"]);
            action.WoundsForTransformation = Game.Xml.IntParse(xmlAction["WoundsForTransformation"]);
            action.WoundsLimit = Game.Xml.IntParse(xmlAction["WoundsLimit"]);
            action.HitStrengthBonus = Game.Xml.IntParse(xmlAction["HitStrengthBonus"]);
            action.ExtendedDamage = Game.Xml.IntParse(xmlAction["ExtendedDamage"]);
            action.Specificity = SpecificsParse(xmlAction["Specificity"]);

            if (xmlAction["Benefit"] != null)
            {
                action.BenefitList = new List<Abstract.IModification>();

                foreach (XmlNode bonefit in xmlAction.SelectNodes("Benefit"))
                    action.BenefitList.Add(Game.Xml.ModificationParse(bonefit, new Modification()));
            }

            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                {
                    Character enemy = EnemyParse(xmlEnemy);

                    if (Game.Xml.BoolParse(xmlAction["RandomEnemyCount"]))
                        EnemyMultiplier(Game.Dice.Roll(), ref action, enemy);
                    else
                        action.Enemies.Add(enemy);
                }
            }

            return action;
        }

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

        public static void EnemyMultiplier(int count, ref Actions action, Character enemy)
        {
            Dictionary<int, string> countLine = Constants.GetCountName();

            string name = enemy.Name;

            for (int i = 0; i < count; i++)
            {
                enemy.Name = countLine[i + 1] + " " + name;
                action.Enemies.Add(enemy.Clone());
            }
        }

        private static Actions.Specifics SpecificsParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return Actions.Specifics.Nope;

            bool success = Enum.TryParse(xmlNode.InnerText, out Actions.Specifics value);

            return (success ? value : Actions.Specifics.Nope);
        }
    }
}
