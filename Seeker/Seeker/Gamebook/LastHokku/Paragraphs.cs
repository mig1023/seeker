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
        public override Paragraph Get(int id, XmlNode xmlParagraph) =>
            base.Get(xmlParagraph);

        private string TextByOptions(string option)
        {
            if (Character.Protagonist.Hokku == null)
                return String.Empty;

            bool wihoutHokku = Constants.GetParagraphsWithoutHokkuCreation.Contains(Game.Data.CurrentParagraphID);

            bool needToAdd = (Character.Protagonist.Hokku.Count == 0) || (Character.Protagonist.Hokku.Last() != option);
            bool fullHokku = (Character.Protagonist.Hokku.Count > 0) && Character.Protagonist.Hokku.Last().Contains('.');

            if (!wihoutHokku && !String.IsNullOrEmpty(option) && needToAdd && !fullHokku)
                Character.Protagonist.Hokku.Add(option);

            if (Character.Protagonist.Hokku.Count >= 7)
            {
                List<string> oldHokku = Character.Protagonist.Hokku;

                oldHokku[2] = new System.Globalization.CultureInfo("ru-RU", false).TextInfo.ToTitleCase(oldHokku[2]);

                List<string> newHokku = new List<string>
                {
                    $"{oldHokku[0]} {oldHokku[1]}",
                    $"{oldHokku[2]} {oldHokku[3]}.",
                    $"{oldHokku[4]} - {oldHokku[5]} {oldHokku[6]}...",
                };

                Character.Protagonist.Hokku = newHokku;
            }

            return String.Join("\n", Character.Protagonist.Hokku);
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
