using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.HeartOfIce
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph) => GetTemplate(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.Skill = Game.Xml.StringParse(xmlAction["Skill"]);
            action.RemoveTrigger = Game.Xml.StringParse(xmlAction["RemoveTrigger"]);
            action.SellType = Game.Xml.StringParse(xmlAction["SellType"]);
            action.Choice = Game.Xml.BoolParse(xmlAction["Choice"]);
            action.Sell = Game.Xml.BoolParse(xmlAction["Sell"]);
            action.SellIfAvailable = Game.Xml.BoolParse(xmlAction["SellIfAvailable"]);
            action.Split = Game.Xml.BoolParse(xmlAction["Split"]);

            if (xmlAction["Benefit"] != null)
            {
                action.BenefitList = new List<Abstract.IModification>();

                foreach (XmlNode bonefit in xmlAction.SelectNodes("Benefit"))
                    action.BenefitList.Add(Game.Xml.ModificationParse(bonefit, new Modification()));
            }

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption) => OptionParseWithDo(xmlOption, new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Game.Xml.ModificationParse(xmlModification, new Modification());
    }
}
