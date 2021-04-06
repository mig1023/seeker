using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.HowlOfTheWerewolf
{
    class Paragraphs : Abstract.IParagraphs
    {
        public Game.Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Game.Paragraph paragraph = new Game.Paragraph();

            paragraph.Options = new List<Option>();
            paragraph.Actions = new List<Abstract.IActions>();
            paragraph.Modification = new List<Abstract.IModification>();

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/Option"))
            {
                Option option = new Option
                {
                    Text = Game.Xml.StringParse(xmlOption.Attributes["Text"], defaultText: "Далее"),
                    Aftertext = Game.Xml.StringParse(xmlOption.Attributes["Aftertext"]),
                };

                if (xmlOption.Attributes["Destination"].Value == "Back")
                    option.Destination = Character.Protagonist.WayBack;
                else
                    option.Destination = Game.Xml.IntParse(xmlOption.Attributes["Destination"]);

                if (xmlOption.Attributes["Do"] != null)
                {
                    Modification modification = new Modification
                    {
                        Name = Game.Xml.StringParse(xmlOption.Attributes["Do"]),
                        Value = Game.Xml.IntParse(xmlOption.Attributes["Value"]),
                    };

                    option.Do = modification;
                }

                paragraph.Options.Add(option);
            }

            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/Action"))
            {
                Actions action = new Actions
                {
                    ActionName = Game.Xml.StringParse(xmlAction["ActionName"]),
                    ButtonName = Game.Xml.StringParse(xmlAction["ButtonName"]),
                    Aftertext = Game.Xml.StringParse(xmlAction["Aftertext"]),
                    Text = Game.Xml.StringParse(xmlAction["Text"]),
                    Trigger = Game.Xml.StringParse(xmlAction["Trigger"]),

                    Price = Game.Xml.IntParse(xmlAction["Price"]),
                    Value = Game.Xml.IntParse(xmlAction["Value"]),
                    RoundsToWin = Game.Xml.IntParse(xmlAction["RoundsToWin"]),
                    RoundsWinToWin = Game.Xml.IntParse(xmlAction["RoundsWinToWin"]),
                    RoundsFailToFail = Game.Xml.IntParse(xmlAction["RoundsFailToFail"]),
                    RoundsToFight = Game.Xml.IntParse(xmlAction["RoundsToFight"]),
                    WoundsToWin = Game.Xml.IntParse(xmlAction["WoundsToWin"]),
                    WoundsToFail = Game.Xml.IntParse(xmlAction["WoundsToFail"]),
                    WoundsForTransformation = Game.Xml.IntParse(xmlAction["WoundsForTransformation"]),
                    WoundsLimit = Game.Xml.IntParse(xmlAction["WoundsLimit"]),
                    HitStrengthBonus = Game.Xml.IntParse(xmlAction["HitStrengthBonus"]),
                    ExtendedDamage = Game.Xml.IntParse(xmlAction["ExtendedDamage"]),

                    Multiple = Game.Xml.BoolParse(xmlAction["Multiple"]),

                    Specificity = SpecificsParse(xmlAction["Specificity"]),
                };

                if (xmlAction["Benefit"] != null)
                {
                    action.Benefit = new List<Modification>();

                    foreach (XmlNode bonefit in xmlAction.SelectNodes("Benefit"))
                        action.Benefit.Add(ModificationParse(bonefit));
                }

                if (xmlAction["Enemies"] != null)
                {
                    action.Enemies = new List<Character>();

                    foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    {
                        Character enemy = new Character
                        {
                            Name = Game.Xml.StringParse(xmlEnemy.Attributes["Name"]),
                            MaxMastery = Game.Xml.IntParse(xmlEnemy.Attributes["Mastery"]),
                            MaxEndurance = Game.Xml.IntParse(xmlEnemy.Attributes["Endurance"]),
                        };

                        enemy.Mastery = enemy.MaxMastery;
                        enemy.Endurance = enemy.MaxEndurance;

                        if (Game.Xml.BoolParse(xmlAction["RandomEnemyCount"]))
                            EnemyMultiplier(Game.Dice.Roll(), ref action, enemy);
                        else
                            action.Enemies.Add(enemy);
                    }
                }

                paragraph.Actions.Add(action);
            }

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
                paragraph.Modification.Add(ModificationParse(xmlModification));

            paragraph.Trigger = Game.Xml.StringParse(xmlParagraph["Triggers"]);

            return paragraph;
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

        private static Modification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification
            {
                Name = Game.Xml.StringParse(xmlNode.Attributes["Name"]),
                Value = Game.Xml.IntParse(xmlNode.Attributes["Value"]),
                ValueString = Game.Xml.StringParse(xmlNode.Attributes["ValueString"]),
            };

            return modification;
        }
    }
}
