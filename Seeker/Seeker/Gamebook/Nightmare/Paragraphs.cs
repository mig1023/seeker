using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.Nightmare
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public override Paragraph Get(int id, XmlNode xmlParagraph) =>
            base.Get(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction) =>
            (Actions)ActionTemplate(xmlAction, new Actions());

        public override Option OptionParse(XmlNode xmlOption)
        {
            Option option = OptionsTemplate(xmlOption);

            if (Constants.Buttons.ContainsKey(option.Goto))
                option.Style = Constants.Buttons[option.Goto]; 

            return option;
        }
            
    }
}
