using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Seeker.Game
{
    public class Services
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
                string line = CoinsNoun(fullSize, "слово", "слова", "слов");
                return String.Format("{0} {1}", fullSize, line);
            }
            else
                return String.Format("{0} тыс. слов", (fullSize / 1000));
        }

        public static string ValueStringFuse(string value) =>
            (value == "ValueString" ? "Value" : value);

        public static bool ParagraphsWithoutStatuses(List<string> statuses)
        {
            bool withoutStatuses = (statuses == null) ||
                Data.Constants.GetParagraphsWithoutStatuses().Contains(Data.CurrentParagraphID);

            int limitStart = 0, limitEnd = 0;
            bool isLimited = Data.Constants?.GetParagraphsStatusesLimit(out limitStart, out limitEnd) ?? false;
            bool statusesLimit = false;

            if (statuses == null)
                statusesLimit = false;
            else if (isLimited)
                statusesLimit = (Data.CurrentParagraphID >= limitStart) && (Data.CurrentParagraphID <= limitEnd);

            return (withoutStatuses || statusesLimit);
        }

        public static void BookmarkName(Dictionary<string, string> bookmarks, string bookmarkIn, out string bookmarkOut, out string saveName)
        {
            int bookmarkIndex = 0;
            bookmarkOut = bookmarkIn;

            while (bookmarks.Keys.Contains(bookmarkOut))
            {
                bookmarkIndex += 1;
                bookmarkOut = Regex.Replace(bookmarkOut, @"\s+\(\d\)$", String.Empty);
                bookmarkOut += String.Format(" ({0})", bookmarkIndex);
            };

            int nextSaveGameIndex = 0;

            do
            {
                nextSaveGameIndex += 1;
                saveName = String.Format("SAVE{0}", nextSaveGameIndex);
            }
            while (bookmarks.Values.Contains(saveName));
        }
    }
}
