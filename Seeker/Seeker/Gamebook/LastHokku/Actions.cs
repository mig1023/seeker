using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.LastHokku
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        public static Actions GetInstance() => StaticInstance;
        private static Character protagonist = Character.Protagonist;

        public override string TextByOptions(string option)
        {
            if (protagonist.Hokku == null)
                return String.Empty;

            bool wihoutHokku = Constants.GetParagraphsWithoutHokkuCreation.Contains(Game.Data.CurrentParagraphID);

            if (!wihoutHokku && !String.IsNullOrEmpty(option))
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
    }
}
