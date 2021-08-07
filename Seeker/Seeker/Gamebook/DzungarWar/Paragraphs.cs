using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.DzungarWar
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph) => GetTemplate(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.RemoveTrigger = Xml.StringParse(xmlAction["RemoveTrigger"]);
            action.Stat = Xml.StringParse(xmlAction["Stat"]);
            action.TriggerTestPenalty = Xml.StringParse(xmlAction["TriggerTestPenalty"]);
            action.Level = Xml.IntParse(xmlAction["Level"]);
            action.StatStep = Xml.IntParse(xmlAction["StatStep"]);
            action.StatToMax = Xml.BoolParse(xmlAction["StatToMax"]);
            action.Benefit = ModificationParse(xmlAction["Benefit"]);

            bool bargain = Xml.BoolParse(xmlAction["Bargain"]);

            if (bargain && Game.Data.Triggers.Contains("Bargain"))
                action.Price /= 2;

            if (action.Name == "Option")
                action.Option = OptionParse(xmlAction["Option"]);

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption) => OptionParseWithDo(xmlOption, new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification
            {
                Name = Xml.StringParse(xmlNode.Attributes["Name"]),
                Value = Xml.IntParse(xmlNode.Attributes["Value"]),
                Empty = Xml.BoolParse(xmlNode.Attributes["Empty"]),
                Init = Xml.BoolParse(xmlNode.Attributes["Init"]),
            };

            return modification;
        }
    }
}
