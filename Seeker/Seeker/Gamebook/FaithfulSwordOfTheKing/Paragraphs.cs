using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.FaithfulSwordOfTheKing
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
                    OnlyIf = Game.Xml.StringParse(xmlOption.Attributes["OnlyIf"]),
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

                    RoundsToWin = Game.Xml.IntParse(xmlAction["RoundsToWin"]),
                    WoundsToWin = Game.Xml.IntParse(xmlAction["WoundsToWin"]),
                    SkillPenalty = Game.Xml.IntParse(xmlAction["SkillPenalty"]),
                    Price = Game.Xml.IntParse(xmlAction["Price"]),

                    MeritalArt = MeritalArtsParse(xmlAction["MeritalArt"]),
                    Multiple = Game.Xml.BoolParse(xmlAction["Multiple"]),

                    Benefit = ModificationParse(xmlAction["Benefit"]),
                };

                if (xmlAction["Enemies"] != null)
                {
                    action.Enemies = new List<Character>();

                    foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    {
                        Character enemy = new Character
                        {
                            Name = Game.Xml.StringParse(xmlEnemy.Attributes["Name"]),
                            MaxSkill = Game.Xml.IntParse(xmlEnemy.Attributes["Skill"]),
                            MaxStrength = Game.Xml.IntParse(xmlEnemy.Attributes["Strength"]),
                        };

                        enemy.Skill = enemy.MaxSkill;
                        enemy.Strength = enemy.MaxStrength;

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

        private static Character.MeritalArts MeritalArtsParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return Character.MeritalArts.Nope;

            bool success = Enum.TryParse(xmlNode.InnerText, out Character.MeritalArts value);

            return (success ? value : Character.MeritalArts.Nope);
        }

        private static Modification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification
            {
                Name = Game.Xml.StringParse(xmlNode.Attributes["Name"]),
                Value = Game.Xml.IntParse(xmlNode.Attributes["Value"]),
                Empty = Game.Xml.BoolParse(xmlNode.Attributes["Empty"]),
                Restore = Game.Xml.BoolParse(xmlNode.Attributes["Restore"]),
            };

            return modification;
        }
    }
}
