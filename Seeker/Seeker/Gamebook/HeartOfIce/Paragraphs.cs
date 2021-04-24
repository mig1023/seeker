using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.HeartOfIce
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
                    Skill = Game.Xml.StringParse(xmlAction["Skill"]),
                    RemoveTrigger = Game.Xml.StringParse(xmlAction["RemoveTrigger"]),

                    Price = Game.Xml.IntParse(xmlAction["Price"]),

                    Choice = Game.Xml.BoolParse(xmlAction["Choice"]),
                    Multiple = Game.Xml.BoolParse(xmlAction["Multiple"]),
                    Sell = Game.Xml.BoolParse(xmlAction["Sell"]),
                };

                if (xmlAction["Benefit"] != null)
                {
                    action.Benefit = new List<Modification>();

                    foreach (XmlNode bonefit in xmlAction.SelectNodes("Benefit"))
                        action.Benefit.Add(ModificationParse(bonefit));
                }

                paragraph.Actions.Add(action);
            }

            paragraph.Trigger = Game.Xml.StringParse(xmlParagraph["Triggers"]);
            paragraph.RemoveTrigger = Game.Xml.StringParse(xmlParagraph["RemoveTriggers"]);

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
                ValueString = Game.Xml.StringParse(xmlNode.Attributes["ValueString"]),
            };

            return modification;
        }
    }
}
