using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook
{
    class Description
    {
        public string Book;

        public string Title;

        public string Original;

        public string XmlBook;

        public string BookColor;

        public string FontColor;

        public string BorderColor;

        public string Illustration;

        public List<string> Authors;

        public bool SinglePseudonym;

        public bool FullPseudonym;

        public bool ConfusionOfAuthors;

        public int Year;

        public string Paragraphs;

        public bool OnlyFirstParagraphsValue;

        public string Size;

        public string PlaythroughTime;

        public List<string> Translators;

        public string Text;

        public string Setting;

        public Links Links;

        public string AuthorsIndex()
        {
            string[] elements = Authors.First().Split(' ');

            if (!ConfusionOfAuthors && !SinglePseudonym && !FullPseudonym && (elements.Length > 1))
            {
                return String.Format("{0} {1}", elements[1].Replace(",", String.Empty), elements[0]);
            }
            else if (ConfusionOfAuthors)
            {
                int lineLength = elements.Count() - 1;
                string[] newElementsOrd = new string[lineLength];
                Array.Copy(elements, newElementsOrd, lineLength);
                string newAuthorLine = String.Join(" ", newElementsOrd);
                return $"{elements.Last()} {newAuthorLine}";
            }
            else if (FullPseudonym)
            {
                return Authors.First();
            }
            else
            {
                return elements.First().Replace(",", String.Empty);
            }
        }

        public int ParagraphSize()
        {
            if (Paragraphs.Contains("(") && OnlyFirstParagraphsValue)
            {
                int start = Paragraphs.IndexOf("(") + 1;
                int len = Paragraphs.IndexOf("+") - Paragraphs.IndexOf("(") - 1;

                return Game.Xml.IntParse(Paragraphs.Substring(start, len));
            }
            else
            {
                string paragraphs = (Paragraphs.Contains("(") ?
                    Paragraphs.Substring(0, Paragraphs.IndexOf(" ")) : Paragraphs);

                return Game.Xml.IntParse(paragraphs);
            }
        }

        public string ParagraphSizeLine()
        {
            string paragraphs = Game.Services.CoinsNoun(ParagraphSize(), "параграф", "параграфа", "параграфов");
            return String.Format("{0} {1}", Paragraphs, paragraphs);
        }
    }
}
