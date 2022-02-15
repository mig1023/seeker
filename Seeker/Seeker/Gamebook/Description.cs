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

        public string Size;

        public string Translator;

        public string Translators;

        public string Text;

        public string Setting;

        public Links Links;

        public string AuthorsIndex()
        {
            string author = Author + Authors;
            string[] elements = author.Split(' ');

            if (!SinglePseudonym && !FullPseudonym && (elements.Length > 1))
                return String.Format("{0} {1}", elements[1].Replace(",", String.Empty), elements[0]);

            else if (FullPseudonym)
                return Author;

            else
                return elements[0];
        }
    }
}
