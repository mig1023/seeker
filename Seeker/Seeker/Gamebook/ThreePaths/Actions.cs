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

            if (Character.Protagonist.Time != null)
                statusLines.Add(String.Format("Время: {0}", Character.Protagonist.Time));

            if (Character.Protagonist.Spells != null)
                statusLines.Add(String.Format("Заклятий: {0}", Character.Protagonist.Spells));

            if (statusLines.Count == 0)
                return null;

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
            string[] options = option.Split(',');

            foreach (string oneOption in options)
            {
                if (oneOption.Contains(">") || oneOption.Contains("<"))
                {
                    if (oneOption.Contains("ЗАКЛЯТЬЯ >") && (int.Parse(oneOption.Split('>')[1]) >= Character.Protagonist.Spells))
                        return false;
                }
                else if (oneOption.Contains("!"))
                {
                    if (Game.Data.Triggers.Contains(oneOption.Replace("!", String.Empty).Trim()))
                        return false;
                }
                else if (!Game.Data.Triggers.Contains(oneOption.Trim()))
                    return false;
            }

            return true;
        }

        public List<string> Representer() => new List<string> { };
    }
}
