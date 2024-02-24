using System;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.ByTheWillOfRome
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public override Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Game.Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/*"))
            {
                bool addon = id >= Constants.AddonStartParagraph;
                bool visibleBySetting = Game.Settings.GetValue("DisabledOption") == 1;
                string availability = xmlOption.Attributes["Availability"]?.Value ?? String.Empty;

                if (addon && !visibleBySetting && !Actions.StaticInstance.Availability(availability))
                    continue;

                paragraph.Options.Add(OptionParseWithDo(xmlOption, new Modification()));
            }

            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/*"))
                paragraph.Actions.Add(ActionParse(xmlAction));

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/*"))
                paragraph.Modification.Add(ModificationParse(xmlModification));

            return paragraph;
        }

        public override Abstract.IActions ActionParse(XmlNode xmlAction) =>
            base.ActionParse(xmlAction, new Actions(), GetProperties(new Actions()), new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
           (Abstract.IModification)base.ModificationParse(xmlModification, new Modification());
    }
}
