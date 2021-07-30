using System.Collections.Generic;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.HeartOfIce
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph) => GetTemplate(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.Skill = Xml.StringParse(xmlAction["Skill"]);
            action.RemoveTrigger = Xml.StringParse(xmlAction["RemoveTrigger"]);
            action.SellType = Xml.StringParse(xmlAction["SellType"]);
            action.Choice = Xml.BoolParse(xmlAction["Choice"]);
            action.Sell = Xml.BoolParse(xmlAction["Sell"]);
            action.SellIfAvailable = Xml.BoolParse(xmlAction["SellIfAvailable"]);
            action.Split = Xml.BoolParse(xmlAction["Split"]);

            if (xmlAction["Benefit"] != null)
            {
                action.BenefitList = new List<Abstract.IModification>();

                foreach (XmlNode bonefit in xmlAction.SelectNodes("Benefit"))
                    action.BenefitList.Add(Xml.ModificationParse(bonefit, new Modification()));
            }

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption) => OptionParseWithDo(xmlOption, new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Xml.ModificationParse(xmlModification, new Modification());
    }
}
