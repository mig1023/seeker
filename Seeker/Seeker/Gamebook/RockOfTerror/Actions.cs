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

            List<string> statusLines = new List<string> { String.Format("Прошедшее время: {0:d2}:{1:d2}", time.Hours, time.Minutes) };

            if (Character.Protagonist.MonksHeart != null)
                statusLines.Add(String.Format("Сила сердца монаха: {0}", Character.Protagonist.MonksHeart));

            return statusLines;
        }

        public List<string> StaticButtons() => new List<string> { };

        public bool StaticAction() => false;

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
            if (option.Contains(">") || option.Contains("<"))
            {
                if (option.Contains("СИЛА СЕРДЦА МОНАХА >=") && (int.Parse(option.Split('=')[1]) > Character.Protagonist.MonksHeart))
                    return false;
                else if (option.Contains("ВРЕМЯ >=") && (int.Parse(option.Split('=')[1]) > Character.Protagonist.Time))
                    return false;
                else if (option.Contains("ВРЕМЯ <") && (int.Parse(option.Split('<')[1]) < Character.Protagonist.Time))
                    return false;
                else
                    return true;
            }
            else
                return Game.Data.Triggers.Contains(option.Trim());
        }
    }
}
