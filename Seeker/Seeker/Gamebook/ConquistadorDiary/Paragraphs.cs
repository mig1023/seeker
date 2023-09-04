using System;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.ConquistadorDiary
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph) =>
            base.Get(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)base.ActionParse(xmlAction,
                new Actions(), GetProperties(new Actions()), new Modification());

            if (!String.IsNullOrEmpty(action.Round))
            {
                string clean = action.Round.Trim('a').Trim('b');
                bool success = int.TryParse(clean, out int round);

                if (success)
                    action.CurrentRound = round;
            }

            return action;
        }
            

        public override Option OptionParse(XmlNode xmlOption) =>
            OptionParseWithDo(xmlOption, new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
           (Abstract.IModification)base.ModificationParse(xmlModification, new Modification());
    }
}
