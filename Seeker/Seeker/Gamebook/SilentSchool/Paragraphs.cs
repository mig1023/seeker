using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.SilentSchool
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph) => base.Get(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.HarmedMyself = Xml.IntParse(xmlAction["HarmedMyself"]);
            action.Dices = Xml.IntParse(xmlAction["Dices"]);

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption)
        {
            Option option = OptionsTemplateWithoutDestination(xmlOption);

            if (xmlOption.Attributes["Destination"].Value == "Change")
                option.Destination = Character.Protagonist.ChangeDecision;
            else if (xmlOption.Attributes["Destination"].Value == "Back")
                option.Destination = Character.Protagonist.WayBack;
            else
                option.Destination = Xml.IntParse(xmlOption.Attributes["Destination"]);

            if (xmlOption.Attributes["Do"] != null)
            {
                Modification modification = new Modification { Name = Xml.StringParse(xmlOption.Attributes["Do"]) };

                if (int.TryParse(xmlOption.Attributes["Value"].Value, out _))
                    modification.Value = Xml.IntParse(xmlOption.Attributes["Value"]);
                else
                    modification.ValueString = Xml.StringParse(xmlOption.Attributes["Value"]);

                option.Do = modification;
            }

            return option;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification { Name = Xml.StringParse(xmlNode.Attributes["Name"]) };

            if (xmlNode.Attributes["Value"] == null)
                return modification;

            if (int.TryParse(xmlNode.Attributes["Value"].Value, out _))
                modification.Value = Xml.IntParse(xmlNode.Attributes["Value"]);
            else
                modification.ValueString = Xml.StringParse(xmlNode.Attributes["Value"]);

            return modification;
        }
    }
}
