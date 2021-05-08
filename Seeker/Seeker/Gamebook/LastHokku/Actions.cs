using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.LastHokku
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static bool CheckOnlyIf(string option) => true;

        public override string TextByOptions(string option)
        {
            if (!Constants.GetParagraphsWithoutHokkuCreation().Contains(Game.Data.CurrentParagraphID) && !String.IsNullOrEmpty(option))
                Character.Protagonist.Hokku.Add(option);

            if (Character.Protagonist.Hokku.Count >= 7)
            {
                List<string> oldHokku = Character.Protagonist.Hokku;

                oldHokku[2] = new System.Globalization.CultureInfo("ru-RU", false).TextInfo.ToTitleCase(oldHokku[2]);

                List<string> newHokku = new List<string>
                {
                    String.Format("{0} {1}", oldHokku[0], oldHokku[1]),
                    String.Format("{0} {1}", oldHokku[2], oldHokku[3]),
                    String.Format("{0} {1} {2}", oldHokku[4], oldHokku[5], oldHokku[6]),
                };

                Character.Protagonist.Hokku = newHokku;
            }

            return String.Join("\n", Character.Protagonist.Hokku);
        }
    }
}
