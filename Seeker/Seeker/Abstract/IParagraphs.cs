using System.Xml;

namespace Seeker.Abstract
{
    interface IParagraphs
    {
        Game.Paragraph Get(int id, XmlNode xmlParagraph);

        IActions ActionParse(XmlNode xmlAction);

        Game.Option OptionParse(XmlNode xmlOption);

        IModification ModificationParse(XmlNode xmlxmlModification);
    }
}
