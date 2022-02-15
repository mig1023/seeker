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

        public int Year;

        public string Paragraphs;

        public string Size;

        public string Translator;

        public string Translators;

        public string Text;

        public string Setting;

        public Links Links;

        public string AuthorsIndex(out string autorsOutput)
        {
            string author = Author + Authors;
            string[] elements = author.Split(' ');

            if (elements.Length > 1)
            {
                autorsOutput = String.Format("{0} {1}", elements[1], elements[0]);
                return elements[1];
            }
            else
            {
                autorsOutput = elements[0];
                return elements[0];
            }
        }
    }
}
