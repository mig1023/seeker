using System;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.YouAreMillionaire
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public override Paragraph Get(int id, XmlNode xmlParagraph) =>
            base.Get(xmlParagraph);

        public override Option OptionParse(XmlNode xmlOption)
        {
            Option option = base.OptionParse(xmlOption);

            if (String.IsNullOrEmpty(option.Text))
                option.Text = (option.Goto == 0 ? "Начать сначала" : "Далее");

            option.Text = $"$$$  {option.Text}  $$$";

            return option;
        }
    }
}