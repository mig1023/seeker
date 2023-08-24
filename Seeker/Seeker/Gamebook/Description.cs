using System;

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

        public string Author;

        public string Authors;

        public bool SinglePseudonym;

        public bool FullPseudonym;

        public int Year;

        public string Paragraphs;

        public bool OnlyFirstParagraphsValue;

        public string Size;

        public string PlaythroughTime;

        public string Translator;

        public string Translators;

        public string Text;

        public string Setting;

        public Links Links;

        public string AuthorsIndex()
        {
            string allAuthors = Author + Authors;
            string firstAuthor = allAuthors.Split(new string[] { "\\n" }, StringSplitOptions.RemoveEmptyEntries)[0];
            string[] elements = firstAuthor.Split(' ');

            if (!SinglePseudonym && !FullPseudonym && (elements.Length > 1))
                return String.Format("{0} {1}", elements[1].Replace(",", String.Empty), elements[0]);

            else if (FullPseudonym)
                return Author;

            else
                return elements[0].Replace(",", String.Empty);
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
