using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.LastHokku
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protogonist = Character.Protagonist;

        public override string TextByOptions(string option)
        {
            if (!Constants.GetParagraphsWithoutHokkuCreation().Contains(Game.Data.CurrentParagraphID) && !String.IsNullOrEmpty(option))
                protogonist.Hokku.Add(option);

            if (protogonist.Hokku.Count >= 7)
            {
                List<string> oldHokku = protogonist.Hokku;

                oldHokku[2] = new System.Globalization.CultureInfo("ru-RU", false).TextInfo.ToTitleCase(oldHokku[2]);

                List<string> newHokku = new List<string>
                {
                    String.Format("{0} {1}", oldHokku[0], oldHokku[1]),
                    String.Format("{0} {1}.", oldHokku[2], oldHokku[3]),
                    String.Format("{0} - {1} {2}...", oldHokku[4], oldHokku[5], oldHokku[6]),
                };

                protogonist.Hokku = newHokku;
            }

            return String.Join("\n", protogonist.Hokku);
        }
    }
}
