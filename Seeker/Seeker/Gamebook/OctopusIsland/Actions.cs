using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.OctopusIsland
{
    class Actions : Abstract.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }
        public int Dices { get; set; }


        public List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            if (trigger)
                Game.Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = ((actionResult.Count >= 1) && (actionResult[0] == "RELOAD") ? true : false);

            return actionResult;
        }

        public List<string> Representer() => new List<string> { };

        public List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Серж: {0}/{1}", Character.Protagonist.SergeSkill, Character.Protagonist.SergeHitpoint),
                String.Format("Ксалотл: {0}/{1}", Character.Protagonist.XolotlSkill, Character.Protagonist.XolotlHitpoint),
                String.Format("Тибо: {0}/{1}", Character.Protagonist.ThibautSkill, Character.Protagonist.ThibautHitpoint),
                String.Format("Суи: {0}/{1}", Character.Protagonist.SouhiSkill, Character.Protagonist.SouhiHitpoint),
            };

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

        public bool IsButtonEnabled() => true;

        public static bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
            {
                string[] options = option.Split('|');

                foreach (string oneOption in options)
                    if (Game.Data.Triggers.Contains(oneOption.Trim()))
                        return true;

                return false;
            }
            else
            {
                string[] options = option.Split(',');

                foreach (string oneOption in options)
                {
                    if (oneOption.Contains("!"))
                    {
                        if (Game.Data.Triggers.Contains(oneOption.Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (!Game.Data.Triggers.Contains(oneOption.Trim()))
                        return false;
                }

                return true;
            }
        }
    }
}
