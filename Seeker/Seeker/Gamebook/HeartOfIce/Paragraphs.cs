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

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Game.Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/Option"))
                paragraph.Options.Add(OptionParse(xmlOption));

            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/Action"))
                paragraph.Actions.Add(ActionParse(xmlAction));

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
                paragraph.Modification.Add(Game.Xml.ModificationParse(xmlModification, new Modification()));

            return paragraph;
        }

        private Actions ActionParse(XmlNode xmlAction)
        {
            Actions action = new Actions
            {
                ActionName = Game.Xml.StringParse(xmlAction["ActionName"]),
                ButtonName = Game.Xml.StringParse(xmlAction["ButtonName"]),
                Aftertext = Game.Xml.StringParse(xmlAction["Aftertext"]),
                Trigger = Game.Xml.StringParse(xmlAction["Trigger"]),
                Text = Game.Xml.StringParse(xmlAction["Text"]),
                Skill = Game.Xml.StringParse(xmlAction["Skill"]),
                RemoveTrigger = Game.Xml.StringParse(xmlAction["RemoveTrigger"]),
                SellType = Game.Xml.StringParse(xmlAction["SellType"]),

                Price = Game.Xml.IntParse(xmlAction["Price"]),

                Choice = Game.Xml.BoolParse(xmlAction["Choice"]),
                Multiple = Game.Xml.BoolParse(xmlAction["Multiple"]),
                Sell = Game.Xml.BoolParse(xmlAction["Sell"]),
                SellIfAvailable = Game.Xml.BoolParse(xmlAction["SellIfAvailable"]),
                Split = Game.Xml.BoolParse(xmlAction["Split"]),
            };

            if (xmlAction["Benefit"] != null)
            {
                action.BenefitList = new List<Abstract.IModification>();

                foreach (XmlNode bonefit in xmlAction.SelectNodes("Benefit"))
                    action.BenefitList.Add(Game.Xml.ModificationParse(bonefit, new Modification()));
            }

            return action;
        }

        private Option OptionParse(XmlNode xmlOption)
        {
            Option option = OptionsTemplate(xmlOption);

            if (xmlOption.Attributes["Do"] != null)
                option.Do = Game.Xml.ModificationParse(xmlOption, new Modification(), name: "Do");

            return option;
        }
    }
}
