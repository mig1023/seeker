using System;
using System.Collections.Generic;
using System.Text;


namespace Seeker.Gamebook.SwampFever
{
    class Actions : Interfaces.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }


        public List<string> Do(out bool reload, string action = "", bool trigger = false)
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
            List<string> statusLines = new List<string> { String.Empty };

            return statusLines;
        }

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Начать с начала...";

            return false;
        }

        public bool IsButtonEnabled()
        {
            return true;
        }

        public static bool CheckOnlyIf(string option)
        {
            return Game.Data.Triggers.Contains(option.Trim());
        }
    }
}
