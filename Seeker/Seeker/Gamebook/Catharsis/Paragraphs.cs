using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.Catharsis
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
                    Text = Game.Xml.StringParse(xmlOption.Attributes["Text"]),
                    OnlyIf = Game.Xml.StringParse(xmlOption.Attributes["OnlyIf"]),
                    Aftertext = Game.Xml.StringParse(xmlOption.Attributes["Aftertext"]),
                };

                if (xmlOption.Attributes["Do"] != null)
                    option.Do = Game.Xml.ModificationParse(xmlOption, new Modification(), name: "Do");

                paragraph.Options.Add(option);
            }


            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/Action"))
            {
                Actions action = new Actions
                {
                    ActionName = Game.Xml.StringParse(xmlAction["ActionName"]),
                    ButtonName = Game.Xml.StringParse(xmlAction["ButtonName"]),
                    Text = Game.Xml.StringParse(xmlAction["Text"]),
                    Aftertext = Game.Xml.StringParse(xmlAction["Aftertext"]),
                    Trigger = Game.Xml.StringParse(xmlAction["Trigger"]),
                    Bonus = Game.Xml.StringParse(xmlAction["Bonus"]),
                };

                paragraph.Actions.Add(action);
            }

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
                paragraph.Modification.Add(Game.Xml.ModificationParse(xmlModification, new Modification()));

            paragraph.Trigger = Game.Xml.StringParse(xmlParagraph["Triggers"]);
            paragraph.RemoveTrigger = Game.Xml.StringParse(xmlParagraph["RemoveTriggers"]);
            paragraph.Image = Game.Xml.StringParse(xmlParagraph["Image"]);

            return paragraph;
        }
    }
}
