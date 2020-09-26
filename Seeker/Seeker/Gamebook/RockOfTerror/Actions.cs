using System;
using System.Collections.Generic;
using System.Text;


namespace Seeker.Gamebook.RockOfTerror
{
    class Actions : Interfaces.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string OpenOption { get; set; }


        public List<string> Do(out bool reload, string action = "", bool openOption = false)
        {
            reload = false;

            return new List<string> { };
        }

        public List<string> Representer()
        {
            return new List<string> { };
        }

        public List<string> Status()
        {
            return new List<string>() { String.Format("Время: {0}", Character.Protagonist.Time)  };
        }

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Время вышло...";

            return (Character.Protagonist.Time >= 12 ? true : false);
        }

        public bool IsButtonEnabled()
        {
            return true;
        }

        public static bool CheckOnlyIf(string option)
        {
            return Game.Data.OpenedOption.Contains(option.Trim());
        }
    }
}
