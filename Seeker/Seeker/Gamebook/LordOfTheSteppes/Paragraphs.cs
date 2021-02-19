using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.LordOfTheSteppes
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
                    Text = Game.Xml.StringParse(xmlAction["Text"]),
                    Stat = Game.Xml.StringParse(xmlAction["Stat"]),

                    StatStep = Game.Xml.IntParse(xmlAction["StatStep"]),
                    RoundsToWin = Game.Xml.IntParse(xmlAction["RoundsToWin"]),
                    WoundsToWin = Game.Xml.IntParse(xmlAction["WoundsToWin"]),

                    GroupFight = Game.Xml.BoolParse(xmlAction["GroupFight"]),

                    SpecialTechnique = SpecialTechniquesParse(xmlAction["SpecialTechnique"]),
                };

                if (xmlAction["Allies"] != null)
                {
                    action.Allies = new List<Character>();

                    foreach (XmlNode xmlAlly in xmlAction.SelectNodes("Allies/Ally"))
                    {
                        Character ally = null;

                        if (xmlAlly.Attributes["Hero"] != null)
                        {
                            ally = new Character
                            {
                                Name = Character.Protagonist.Name,
                            };
                        }
                        else
                        {
                            ally = new Character
                            {
                                Name = Game.Xml.StringParse(xmlAlly.Attributes["Name"]),
                                MaxAttack = Game.Xml.IntParse(xmlAlly.Attributes["Attack"]),
                                MaxEndurance = Game.Xml.IntParse(xmlAlly.Attributes["Endurance"]),
                                MaxDefence = Game.Xml.IntParse(xmlAlly.Attributes["Defence"]),
                                MaxInitiative = Game.Xml.IntParse(xmlAlly.Attributes["Initiative"]),
                            };

                            ally.Attack = ally.MaxAttack;
                            ally.Endurance = ally.MaxEndurance;
                            ally.Defence = ally.MaxDefence;
                            ally.Initiative = ally.MaxInitiative;
                        }

                        action.Allies.Add(ally);
                    }
                }

                if (xmlAction["Enemies"] != null)
                {
                    action.Enemies = new List<Character>();

                    foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    {
                        Character enemy = new Character
                        {
                            Name = Game.Xml.StringParse(xmlEnemy.Attributes["Name"]),
                            MaxAttack = Game.Xml.IntParse(xmlEnemy.Attributes["Attack"]),
                            MaxEndurance = Game.Xml.IntParse(xmlEnemy.Attributes["Endurance"]),
                            MaxDefence = Game.Xml.IntParse(xmlEnemy.Attributes["Defence"]),
                            MaxInitiative = Game.Xml.IntParse(xmlEnemy.Attributes["Initiative"]),
                        };

                        enemy.Attack = enemy.MaxAttack;
                        enemy.Endurance = enemy.MaxEndurance;
                        enemy.Defence = enemy.MaxDefence;
                        enemy.Initiative = enemy.MaxInitiative;

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

        private static Character.SpecialTechniques SpecialTechniquesParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return Character.SpecialTechniques.Nope;

            bool success = Enum.TryParse(xmlNode.InnerText, out Character.SpecialTechniques value);

            return (success ? value : Character.SpecialTechniques.Nope);
        }

        private static Modification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification
            {
                Name = Game.Xml.StringParse(xmlNode.Attributes["Name"]),
                Value = Game.Xml.IntParse(xmlNode.Attributes["Value"]),

                Restore = Game.Xml.BoolParse(xmlNode.Attributes["Restore"]),
            };

            return modification;
        }
    }
}
