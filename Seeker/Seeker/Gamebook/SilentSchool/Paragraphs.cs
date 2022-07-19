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
            Option option = OptionsTemplateWithoutLink(xmlOption);

            if (xmlOption.Attributes["Link"].Value == "Change")
                option.Link = Character.Protagonist.ChangeDecision;
            else if (xmlOption.Attributes["Link"].Value == "Back")
                option.Link = Character.Protagonist.WayBack;
            else
                option.Link = Xml.IntParse(xmlOption.Attributes["Link"]);

            XmlNode optionMod = xmlOption.SelectSingleNode("Modification");

            if (optionMod != null)
            {
                Modification modification = new Modification { Name = Xml.StringParse(optionMod.Attributes["Name"]) };

                if (int.TryParse(optionMod.Attributes["Value"].Value, out _))
                    modification.Value = Xml.IntParse(optionMod.Attributes["Value"]);
                else
                    modification.ValueString = Xml.StringParse(optionMod.Attributes["Value"]);

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
