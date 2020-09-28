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
            TimeSpan time = TimeSpan.FromMinutes(Character.Protagonist.Time);

            return new List<string>() { String.Format("Прошедшее время: {0}:{1}", time.Hours, time.Minutes) };
        }

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Время вышло...";

            return (Character.Protagonist.Time >= 720 ? true : false);
        }

        public bool IsButtonEnabled()
        {
            return true;
        }

        public static bool CheckOnlyIf(string option)
        {
            if (option.Contains(">") || oneOption.Contains("<"))
            {
                if (oneOption.Contains("ВРЕМЯ >=") && (int.Parse(oneOption.Split('=')[1]) > Character.Protagonist.Time))
                    return false;
                else if (oneOption.Contains("ВРЕМЯ <") && (int.Parse(oneOption.Split('=')[1]) < Character.Protagonist.Time))
                    return false;
                else
                    return true;
            }
            else
                return Game.Data.OpenedOption.Contains(option.Trim());
        }
    }
}
