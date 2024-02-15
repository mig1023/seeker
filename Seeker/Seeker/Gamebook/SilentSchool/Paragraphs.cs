using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.SilentSchool
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();
        public static Paragraphs GetInstance() => StaticInstance;

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
            Option option = OptionsTemplateWithoutGoto(xmlOption);

            if (ThisIsGameover(xmlOption))
            {
                option.Goto = GetGoto(xmlOption);
            }
            else if (xmlOption.Attributes["Goto"].Value == "Change")
            {
                option.Goto = Character.Protagonist.ChangeDecision;
            }
            else if (ThisIsBack(xmlOption))
            {
                option.Goto = GetGoto(xmlOption, wayBack: Character.Protagonist.WayBack);
            }
            else
            {
                option.Goto = Xml.IntParse(xmlOption.Attributes["Goto"]);
            }

            XmlNode optionMod = xmlOption.SelectSingleNode("Modification");

            if (optionMod != null)
            {
                Modification modification = new Modification { Name = Xml.StringParse(optionMod.Attributes["Name"]) };

                if (int.TryParse(optionMod.Attributes["Value"].Value, out _))
                {
                    modification.Value = Xml.IntParse(optionMod.Attributes["Value"]);
                }
                else
                {
                    modification.ValueString = Xml.StringParse(optionMod.Attributes["Value"]);
                }

                option.Do.Add(modification);
            }

            return option;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification { Name = xmlNode.Name };

            if (xmlNode.Attributes["Value"] == null)
            {
                return modification;
            }
            else if (int.TryParse(xmlNode.Attributes["Value"].Value, out _))
            {
                modification.Value = Xml.IntParse(xmlNode.Attributes["Value"]);
            }
            else
            {
                modification.ValueString = Xml.StringParse(xmlNode.Attributes["Value"]);
            }

            return modification;
        }
    }
}
