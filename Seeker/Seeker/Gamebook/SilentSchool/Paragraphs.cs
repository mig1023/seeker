using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.SilentSchool
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph) =>
            base.Get(xmlParagraph);

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

            if (ThisIsGameover(xmlOption))
            {
                option.Destination = GetDestination(xmlOption);
            }
            else if (xmlOption.Attributes["Destination"].Value == "Change")
            {
                option.Destination = Character.Protagonist.ChangeDecision;
            }
            else if (ThisIsBack(xmlOption))
            {
                option.Destination = GetDestination(xmlOption, wayBack: Character.Protagonist.WayBack);
            }
            else
            {
                option.Destination = Xml.IntParse(xmlOption.Attributes["Destination"]);
            }

            XmlNode optionMod = xmlOption.SelectSingleNode("Modification");

            if (optionMod != null)
            {
                Modification modification = new Modification { Name = Xml.StringParse(optionMod.Attributes["Name"]) };

                if (int.TryParse(optionMod.Attributes["Val"].Value, out _))
                {
                    modification.Value = Xml.IntParse(optionMod.Attributes["Val"]);
                }
                else
                {
                    modification.ValueString = Xml.StringParse(optionMod.Attributes["Val"]);
                }

                option.Do = modification;
            }

            return option;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification { Name = xmlNode.Name };

            if (xmlNode.Attributes["Val"] == null)
            {
                return modification;
            }
            else if (int.TryParse(xmlNode.Attributes["Val"].Value, out _))
            {
                modification.Value = Xml.IntParse(xmlNode.Attributes["Val"]);
            }
            else
            {
                modification.ValueString = Xml.StringParse(xmlNode.Attributes["Val"]);
            }

            return modification;
        }
    }
}
