using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;


namespace Seeker.Gamebook.SilentSchool
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph) => GetTemplate(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction) => new Actions
        {
            ActionName = Game.Xml.StringParse(xmlAction["ActionName"]),
            ButtonName = Game.Xml.StringParse(xmlAction["ButtonName"]),
            Aftertext = Game.Xml.StringParse(xmlAction["Aftertext"]),
            Trigger = Game.Xml.StringParse(xmlAction["Trigger"]),
            Text = Game.Xml.StringParse(xmlAction["Text"]),

            HarmedMyself = Game.Xml.IntParse(xmlAction["HarmedMyself"]),
            Dices = Game.Xml.IntParse(xmlAction["Dices"]),
        };

        public override Option OptionParse(XmlNode xmlOption)
        {
            Option option = OptionsTemplateWithoutDestination(xmlOption);

            if (xmlOption.Attributes["Destination"].Value == "CHANGE")
                option.Destination = Character.Protagonist.ChangeDecision;
            else
                option.Destination = Game.Xml.IntParse(xmlOption.Attributes["Destination"]);

            if (xmlOption.Attributes["Do"] != null)
            {
                Modification modification = new Modification { Name = Game.Xml.StringParse(xmlOption.Attributes["Do"]) };

                if (int.TryParse(xmlOption.Attributes["Value"].Value, out _))
                    modification.Value = Game.Xml.IntParse(xmlOption.Attributes["Value"]);
                else
                    modification.ValueString = Game.Xml.StringParse(xmlOption.Attributes["Value"]);

                option.Do = modification;
            }

            return option;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification { Name = Game.Xml.StringParse(xmlNode.Attributes["Name"]) };

            if (xmlNode.Attributes["Value"] == null)
                return modification;

            if (int.TryParse(xmlNode.Attributes["Value"].Value, out _))
                modification.Value = Game.Xml.IntParse(xmlNode.Attributes["Value"]);
            else
                modification.ValueString = Game.Xml.StringParse(xmlNode.Attributes["Value"]);

            return modification;
        }
    }
}
