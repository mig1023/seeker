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

        public override Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/Option"))
            {
                Option option = OptionsTemplateWithoutDestination(xmlOption);

                if (xmlOption.Attributes["Destination"].Value == "Back")
                    option.Destination = Character.Protagonist.WayBack;
                else
                    option.Destination = Xml.IntParse(xmlOption.Attributes["Destination"]);

                if (xmlOption.Attributes["Do"] != null)
                    option.Do = Xml.ModificationParse(xmlOption, new Modification(), name: "Do");

                paragraph.Options.Add(option);
            }

            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/Action"))
                paragraph.Actions.Add(ActionParse(xmlAction));

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
                paragraph.Modification.Add(Xml.ModificationParse(xmlModification, new Modification()));

            return paragraph;
        }

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.Value = Xml.IntParse(xmlAction["Value"]);
            action.RoundsToWin = Xml.IntParse(xmlAction["RoundsToWin"]);
            action.RoundsWinToWin = Xml.IntParse(xmlAction["RoundsWinToWin"]);
            action.RoundsFailToFail = Xml.IntParse(xmlAction["RoundsFailToFail"]);
            action.RoundsToFight = Xml.IntParse(xmlAction["RoundsToFight"]);
            action.WoundsToWin = Xml.IntParse(xmlAction["WoundsToWin"]);
            action.WoundsToFail = Xml.IntParse(xmlAction["WoundsToFail"]);
            action.WoundsForTransformation = Xml.IntParse(xmlAction["WoundsForTransformation"]);
            action.WoundsLimit = Xml.IntParse(xmlAction["WoundsLimit"]);
            action.HitStrengthBonus = Xml.IntParse(xmlAction["HitStrengthBonus"]);
            action.ExtendedDamage = Xml.IntParse(xmlAction["ExtendedDamage"]);
            action.Specificity = SpecificsParse(xmlAction["Specificity"]);

            if (xmlAction["Benefit"] != null)
            {
                action.BenefitList = new List<Abstract.IModification>();

                foreach (XmlNode bonefit in xmlAction.SelectNodes("Benefit"))
                    action.BenefitList.Add(Xml.ModificationParse(bonefit, new Modification()));
            }

            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                {
                    Character enemy = EnemyParse(xmlEnemy);

                    if (Xml.BoolParse(xmlAction["RandomEnemyCount"]))
                        EnemyMultiplier(Dice.Roll(), ref action, enemy);
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
                Name = Xml.StringParse(xmlEnemy.Attributes["Name"]),
                MaxMastery = Xml.IntParse(xmlEnemy.Attributes["Mastery"]),
                MaxEndurance = Xml.IntParse(xmlEnemy.Attributes["Endurance"]),
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
