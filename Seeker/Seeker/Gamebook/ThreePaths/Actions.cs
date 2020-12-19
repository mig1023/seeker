using System;
using System.Collections.Generic;
using System.Text;


namespace Seeker.Gamebook.ThreePaths
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

            reload = false;

            return new List<string> { };
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string>();

            if (Character.Protagonist.Time > 1)
                String.Format("Время: {0}", Character.Protagonist.Time);

            if (Character.Protagonist.Spells > 1)
                String.Format("Заклятий: {0}", Character.Protagonist.Spells);

            return statusLines;
        }

        public List<string> StaticButtons() => new List<string> { };

        public bool StaticAction(string action) => false;

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = String.Empty;

            return false;
        }

        public bool IsButtonEnabled() => false;

        public static bool CheckOnlyIf(string option)
        {
            if (option.Contains("!"))
                return (Game.Data.Triggers.Contains(option.Replace("!", String.Empty).Trim()) ? false : true);
            else
                return Game.Data.Triggers.Contains(option);
        }

        public List<string> Representer() => new List<string> { };
    }
}
