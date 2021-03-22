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
                    Destination = Game.Xml.IntParse(xmlOption.Attributes["Destination"]),
                    Text = Game.Xml.StringParse(xmlOption.Attributes["Text"], defaultText: "Далее"),
                    Aftertext = Game.Xml.StringParse(xmlOption.Attributes["Aftertext"]),
                };

                paragraph.Options.Add(option);
            }

            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/Action"))
            {
                Actions action = new Actions
                {
                    ActionName = Game.Xml.StringParse(xmlAction["ActionName"]),
                    ButtonName = Game.Xml.StringParse(xmlAction["ButtonName"]),
                    Aftertext = Game.Xml.StringParse(xmlAction["Aftertext"]),
                    Trigger = Game.Xml.StringParse(xmlAction["Trigger"]),

                    RoundsToWin = Game.Xml.IntParse(xmlAction["RoundsToWin"]),
                    RoundsToFight = Game.Xml.IntParse(xmlAction["RoundsToFight"]),
                    WoundsToWin = Game.Xml.IntParse(xmlAction["WoundsToWin"]),
                    WoundsForTransformation = Game.Xml.IntParse(xmlAction["WoundsForTransformation"]),
                    HitStrengthBonus = Game.Xml.IntParse(xmlAction["HitStrengthBonus"]),
                    ExtendedDamage = Game.Xml.IntParse(xmlAction["ExtendedDamage"]),

                    ElectricDamage = Game.Xml.BoolParse(xmlAction["ElectricDamage"]),
                    WitchFight = Game.Xml.BoolParse(xmlAction["WitchFight"]),
                };

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
                        {
                            Dictionary<int, string> countLine = Constants.GetCountName();

                            int count = Game.Dice.Roll();
                            string name = enemy.Name;

                            for (int i = 0; i < count; i++)
                            {
                                enemy.Name = countLine[i + 1] + " " + name;
                                action.Enemies.Add(enemy.Clone());
                            }
                        }
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

        private static Modification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification
            {
                Name = Game.Xml.StringParse(xmlNode.Attributes["Name"]),
                Value = Game.Xml.IntParse(xmlNode.Attributes["Value"]),
            };

            return modification;
        }
    }
}
