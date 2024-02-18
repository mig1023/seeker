using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Seeker.Game;
using Seeker.Output;

namespace Seeker.Gamebook.LastHokku
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public new static Paragraphs StaticInstance = new Paragraphs();
        public new static Paragraphs GetInstance() => StaticInstance;
        private Character protagonist = Character.Protagonist;

        public override Paragraph Get(int id, XmlNode xmlParagraph) =>
            base.Get(xmlParagraph);

        private string TextByOptions(string option)
        {
            if (Character.Protagonist.Hokku == null)
                return String.Empty;

            bool wihoutHokku = Constants.GetParagraphsWithoutHokkuCreation.Contains(Game.Data.CurrentParagraphID);

            bool needToAdd = (protagonist.Hokku.Count == 0) || (protagonist.Hokku.Last() != option);
            bool fullHokku = (protagonist.Hokku.Count > 0) && protagonist.Hokku.Last().Contains('.');

            if (!wihoutHokku && !String.IsNullOrEmpty(option) && needToAdd && !fullHokku)
                protagonist.Hokku.Add(option);

            if (protagonist.Hokku.Count >= 7)
            {
                List<string> oldHokku = protagonist.Hokku;

                oldHokku[2] = new System.Globalization.CultureInfo("ru-RU", false).TextInfo.ToTitleCase(oldHokku[2]);

                List<string> newHokku = new List<string>
                {
                    $"{oldHokku[0]} {oldHokku[1]}",
                    $"{oldHokku[2]} {oldHokku[3]}.",
                    $"{oldHokku[4]} - {oldHokku[5]} {oldHokku[6]}...",
                };

                protagonist.Hokku = newHokku;
            }

            return String.Join("\n", protagonist.Hokku);
        }

        public override List<Text> TextsParse(XmlNode xmlNode, bool main = false)
        {
            string textByOption = TextByOptions(Data.CurrentSelectedOption);

            List<Text> text = new List<Text>();

            if (main && !String.IsNullOrEmpty(textByOption))
            {
                text.Add(Xml.TextLine(textByOption));
            }
            else if (xmlNode["Text"] != null)
            {
                text.Add(Xml.TextLineParse(xmlNode["Text"]));
            }

            return text;
        }
    }
}
