using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.DzungarWar
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph) => GetTemplate(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.RemoveTrigger = Game.Xml.StringParse(xmlAction["RemoveTrigger"]);
            action.Stat = Game.Xml.StringParse(xmlAction["Stat"]);
            action.TriggerTestPenalty = Game.Xml.StringParse(xmlAction["TriggerTestPenalty"]);
            action.Level = Game.Xml.IntParse(xmlAction["Level"]);
            action.StatStep = Game.Xml.IntParse(xmlAction["StatStep"]);
            action.StatToMax = Game.Xml.BoolParse(xmlAction["StatToMax"]);
            action.Benefit = ModificationParse(xmlAction["Benefit"]);

            if (action.ActionName == "Option")
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
                Name = Game.Xml.StringParse(xmlNode.Attributes["Name"]),
                Value = Game.Xml.IntParse(xmlNode.Attributes["Value"]),
                Empty = Game.Xml.BoolParse(xmlNode.Attributes["Empty"]),
                Init = Game.Xml.BoolParse(xmlNode.Attributes["Init"]),
            };

            return modification;
        }
    }
}
