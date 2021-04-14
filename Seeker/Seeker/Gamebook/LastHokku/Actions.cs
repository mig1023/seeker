using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.LastHokku
{
    class Actions : Abstract.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }


        public List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            if (trigger)
                Game.Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = (actionResult.Count >= 1) && (actionResult[0] == "RELOAD");

            return actionResult;
        }

        public List<string> Representer() => new List<string> { };

        public List<string> Status() => null;

        public List<string> AdditionalStatus() => null;

        public List<string> StaticButtons() => new List<string> { };

        public bool StaticAction(string action) => false;

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = String.Empty;

            return false;
        }

        public bool IsButtonEnabled() => true;

        public static bool CheckOnlyIf(string option) => true;

        public bool IsHealingEnabled() => false;

        public void UseHealing(int healingLevel) => Game.Other.DoNothing();

        public string TextByOptions(string option)
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
                Character.Protagonist.Created += 1;
            }

            return String.Join("\n", Character.Protagonist.Hokku);
        }
    }
}
