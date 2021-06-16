using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;


namespace Seeker.Gamebook.StringOfWorlds
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Game.Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            Constants.RandomColor();

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/Option"))
            {
                Option option = OptionsTemplateWithoutDestination(xmlOption);

                if (xmlOption.Attributes["Destination"].Value == "Gate")
                {
                    if (Character.Protagonist.GateCode <= 0)
                        continue;
                    else
                    {
                        foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
                            if (xmlModification.Attributes["Name"].Value == "GateCode")
                                Character.Protagonist.GateCode += Game.Xml.IntParse(xmlModification.Attributes["Value"]);
                        
                        option.Destination = Character.Protagonist.GateCode;
                    }
                }
                else if (int.TryParse(xmlOption.Attributes["Destination"].Value, out int _))
                    option.Destination = Game.Xml.IntParse(xmlOption.Attributes["Destination"]);
                else
                {
                    List<string> destinations = xmlOption.Attributes["Destination"].Value.Split(',').ToList<string>();
                    option.Destination = int.Parse(destinations[random.Next(destinations.Count())]);
                }

                paragraph.Options.Add(option);
            }

            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/Action"))
                paragraph.Actions.Add(ActionParse(xmlAction));

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
                paragraph.Modification.Add(Game.Xml.ModificationParse(xmlModification, new Modification()));

            return paragraph;
        }

        private Actions ActionParse(XmlNode xmlAction)
        {
            Actions action = new Actions
            {
                ActionName = Game.Xml.StringParse(xmlAction["ActionName"]),
                ButtonName = Game.Xml.StringParse(xmlAction["ButtonName"]),
                Aftertext = Game.Xml.StringParse(xmlAction["Aftertext"]),
                Trigger = Game.Xml.StringParse(xmlAction["Trigger"]),
                Text = Game.Xml.StringParse(xmlAction["Text"]),
                Equipment = Game.Xml.StringParse(xmlAction["Equipment"]),

                RoundsToWin = Game.Xml.IntParse(xmlAction["RoundsToWin"]),

                HeroWoundsLimit = Game.Xml.BoolParse(xmlAction["HeroWoundsLimit"]),
                EnemyWoundsLimit = Game.Xml.BoolParse(xmlAction["EnemyWoundsLimit"]),
                DevastatingAttack = Game.Xml.BoolParse(xmlAction["DevastatingAttack"]),
                DarknessPenalty = Game.Xml.BoolParse(xmlAction["DarknessPenalty"]),
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

            if (xmlAction["Benefit"] != null)
                action.Benefit = Game.Xml.ModificationParse(xmlAction["Benefit"], new Modification());

            return action;
        }
    }
}
