using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.GoingToLaughter
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/*"))
            {
                Option option = OptionsTemplateWithoutDestination(xmlOption);

                if (xmlOption.Attributes["Destination"].Value == "Random")
                    option.Destination = id + 1 + random.Next(6);
                else
                    option.Destination = Xml.IntParse(xmlOption.Attributes["Destination"]);

                if (xmlOption.Attributes["Do"] != null)
                    option.Do = Game.Xml.ModificationParse(xmlOption, new Modification(), name: "Do");

                paragraph.Options.Add(option);
            }

            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/Action"))
                paragraph.Actions.Add(ActionParse(xmlAction));

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
                paragraph.Modification.Add(ModificationParse(xmlModification));

            return paragraph;
        }

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            foreach (string param in GetProperties(action))
                SetProperty(action, param, xmlAction);

            return action;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            (Abstract.IModification)base.ModificationParse(xmlModification, new Modification());
    }
}
