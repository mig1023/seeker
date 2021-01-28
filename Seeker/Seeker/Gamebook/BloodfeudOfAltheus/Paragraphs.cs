using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.BloodfeudOfAltheus
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
                Option option = GetOption
                (
                    destination: Game.Xml.IntParse(xmlOption.Attributes["Destination"]),
                    text: Game.Xml.StringParse(xmlOption.Attributes["Text"], defaultText: "Далее"),
                    onlyIf: Game.Xml.StringParse(xmlOption.Attributes["OnlyIf"])
                );

                paragraph.Options.Add(option);
            }

            if (Game.Xml.BoolParse(xmlParagraph["IntuitiveSolution"]))
                paragraph.Options.Add(GetOption(destination: id + 20, text: "Интуитивное решение", onlyIf: "selectOnly"));

            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/Action"))
            {
                Actions action = new Actions
                {
                    ActionName = Game.Xml.StringParse(xmlAction["ActionName"]),
                    ButtonName = Game.Xml.StringParse(xmlAction["ButtonName"]),
                    Aftertext = Game.Xml.StringParse(xmlAction["Aftertext"]),
                    Trigger = Game.Xml.StringParse(xmlAction["Trigger"]),
                };

                paragraph.Actions.Add(action);
            }

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
            {
                Modification modification = new Modification
                {
                    Name = Game.Xml.StringParse(xmlModification.Attributes["Name"]),
                    Value = Game.Xml.IntParse(xmlModification.Attributes["Value"]),
                };

                paragraph.Modification.Add(modification);
            }

            paragraph.Trigger = Game.Xml.StringParse(xmlParagraph["Triggers"]);

            return paragraph;
        }

        private static Option GetOption(int destination, string text, string onlyIf)
        {
            return new Option
            {
                Destination = destination,
                Text = text,
                OnlyIf = onlyIf,
            };
        }
    }
}
