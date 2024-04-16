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

        public string AuthorsIndex(bool noUpper = false)
        {
            string author = Authors.First();

            if (!noUpper)
                author = author.ToUpper();

            string[] elements = author.Split(' ');

            if (!ConfusionOfAuthors && !SinglePseudonym && !FullPseudonym && (elements.Length > 1))
            {
                return $"{elements[1]} {elements[0]}";
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
                return author;
            }
            else
            {
                return elements.First();
            }
        }

        public int ParagraphSize(bool full = false)
        {
            if (Paragraphs.Contains("(") && OnlyFirstParagraphsValue && !full)
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

        public string ParagraphSizeLine(bool full = false)
        {
            int paragraphs = ParagraphSize(full);
            string line = Game.Services.CoinsNoun(paragraphs, "параграф", "параграфа", "параграфов");
            string outputParagraphs = full ? Paragraphs : paragraphs.ToString();

            return $"{outputParagraphs} {line}";
        }

        public string SizeLine()
        {
            int fullSize = int.Parse(Size);

            if (fullSize < 1000)
            {
                string line = Game.Services.CoinsNoun(fullSize, "слово", "слова", "слов");
                return $"{fullSize} {line}";
            }
            else
            {
                int size = fullSize / 1000;
                string line = Game.Services.CoinsNoun(size, "тысяча", "тысячи", "тысяч");
                return $"{size} {line} слов";
            }
        }
    }
}
