using System;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.BangkokSky
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public override Paragraph Get(int id, XmlNode xmlParagraph) =>
            base.Get(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            foreach (string param in GetProperties(action))
                SetProperty(action, param, xmlAction);

            if (!String.IsNullOrEmpty(action.Traits))
            {
                action.Trigger = action.Traits;
                action.Button = action.Traits;
            }

            if (xmlAction["Benefit"] != null)
                action.Benefit = ModificationParse(xmlAction["Benefit"]);

            return action;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            (Abstract.IModification)base.ModificationParse(xmlModification, new Modification());
    }
}