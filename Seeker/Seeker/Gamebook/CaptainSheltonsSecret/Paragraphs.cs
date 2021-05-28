using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;


namespace Seeker.Gamebook.CaptainSheltonsSecret
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Game.Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/Option"))
                paragraph.Options.Add(OptionsTemplate(xmlOption));

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
                    DamageToWin = Game.Xml.IntParse(xmlAction["DamageToWin"]),
                    MasteryPenalty = Game.Xml.IntParse(xmlAction["MasteryPenalty"]),
                    Price = Game.Xml.IntParse(xmlAction["Price"]),

                    Multiple = Game.Xml.BoolParse(xmlAction["Multiple"]),
                    GroupFight = Game.Xml.BoolParse(xmlAction["GroupFight"]),
                };

                if (xmlAction["Allies"] != null)
                {
                    action.Allies = new List<Character>();

                    foreach (XmlNode xmlAlly in xmlAction.SelectNodes("Allies/Ally"))
                    {
                        Character ally = new Character
                        {
                            Name = Game.Xml.StringParse(xmlAlly.Attributes["Name"]),
                            MaxMastery = Game.Xml.IntParse(xmlAlly.Attributes["Mastery"]),
                            MaxEndurance = Game.Xml.IntParse(xmlAlly.Attributes["Endurance"]),
                        };

                        ally.Mastery = ally.MaxMastery;
                        ally.Endurance = ally.MaxEndurance;

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
                            MaxMastery = Game.Xml.IntParse(xmlEnemy.Attributes["Mastery"]),
                            MaxEndurance = Game.Xml.IntParse(xmlEnemy.Attributes["Endurance"]),
                        };

                        enemy.Mastery = enemy.MaxMastery;
                        enemy.Endurance = enemy.MaxEndurance;

                        action.Enemies.Add(enemy);
                    }
                }

                paragraph.Actions.Add(action);
            }

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
                paragraph.Modification.Add(Game.Xml.ModificationParse(xmlModification, new Modification()));

            return paragraph;
        }
    }
}
