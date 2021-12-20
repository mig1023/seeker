using System;

namespace Seeker.Game
{
    public class Other
    {
        public static string Сomparison(int a, int b)
        {
            if (a > b)
                return "больше";

            else if (a < b)
                return "меньше";

            else
                return "равно";
        }

        public static string CoinsNoun(int value, string one, string two, string five)
        {
            int absValue = Math.Abs(value);

            if (absValue % 10 == 5)
                return five;

            absValue %= 100;

            if ((absValue >= 5) && (absValue <= 20))
                return five;

            absValue %= 10;

            if (absValue == 1)
                return one;

            if ((absValue >= 2) && (absValue <= 5))
                return two;

            return five;
        }

        public static string NegativeMeaning(int value) =>
            value < 0 ? String.Format("минус {0}", Math.Abs(value)) : value.ToString(); 

        public static int LevelParse(string option) =>
            int.Parse(option.Contains("=") ? option.Split('=')[1] : option.Split('>', '<')[1]);

        public static bool DoNothing() => true;

        public static string SizeParse(string size)
        {
            int fullSize = int.Parse(size);

            if (fullSize < 1000)
            {
                string line = Game.Other.CoinsNoun(fullSize, "слово", "слова", "слов");
                return String.Format("{0} {1}", fullSize, line);
            }
            else
                return String.Format("{0} тыс. слов", (fullSize / 1000));
        }

        public static string ParagraphSize(string line, out int size)
        {
            if (line.Contains("("))
            {
                string subSize = line.Substring(0, line.IndexOf(" "));
                size = Xml.IntParse(subSize);
            }
            else
                size = Xml.IntParse(line);

            string paragraphs = Game.Other.CoinsNoun(size, "параграф", "параграфа", "параграфов");
            return String.Format("{0} {1}", size, paragraphs);
        }

        public static int ParagraphOrder(string line)
        {
            ParagraphSize(line, out int size);
            return size;
        }
    }
}
