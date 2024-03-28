using System.Collections.Generic;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.Insight
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public override Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/*"))
            {
                Option option = OptionsTemplateWithoutGoto(xmlOption);

                if (ThisIsBack(xmlOption))
                {
                    option.Goto = GetGoto(xmlOption, wayBack: Character.Protagonist.WayBack);
                 }
                else
                {
                    option.Goto = Xml.IntParse(xmlOption.Attributes["Goto"]);
                }

                paragraph.Options.Add(option);
            }

            if (!WhithoutEvidenceSearch(id))
                paragraph.Options.Add(GetEvidenceSearch(id, xmlParagraph));

            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/*"))
                paragraph.Actions.Add(ActionParse(xmlAction));

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/*"))
                paragraph.Modification.Add(Xml.ModificationParse(xmlModification, new Modification()));

            Character.Protagonist.WithoutEvidence = 0;

            return  paragraph;
        }

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());
            action.Bonus = Xml.StringParse(xmlAction["Bonus"]);

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption) =>
            OptionParseWithDo(xmlOption, new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Game.Xml.ModificationParse(xmlModification, new Modification());

        private static Option GetEvidenceSearch(int id, XmlNode xmlParagraph)
        {
            bool evidence = Xml.BoolParse(xmlParagraph["EvidenceSearch"]);
            int link = evidence ? id + 10 : 336;
            Character.Protagonist.WayBack = id;

            return GetOption(link, text: "Искать улики тут");
        }

        private bool WhithoutEvidenceSearch(int id)
        {
            if (Data.Constants.GetParagraphsWithoutStatuses().Contains(id))
            {
                return true;
            }
            else if ((id == 336) || (Character.Protagonist.WithoutEvidence > 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static Option GetOption(int link, string text)
        {
            Modification withoutEvidence = new Modification
            {
                Name = "WithoutEvidence",
                Value = 1,
            };

            Option option = new Option
            {
                Goto = link,
                Text = text,
            };

            if (option.Do == null)
                option.Do = new List<Abstract.IModification>();

            option.Do.Add(withoutEvidence);

            return option;
        }
    }
}