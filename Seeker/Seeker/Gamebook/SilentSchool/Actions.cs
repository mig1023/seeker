﻿using System;
using System.Collections.Generic;
using System.Text;


namespace Seeker.Gamebook.SilentSchool
{
    class Actions : Abstract.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }
        public string Text { get; set; }

        public List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            if (trigger)
                Game.Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = ((actionResult.Count >= 1) && (actionResult[0] == "RELOAD") ? true : false);

            return actionResult;
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Жизнь: {0}", Character.Protagonist.Life),
            };

            if (Character.Protagonist.Grail > 0)
                statusLines.Add(String.Format("Грааль: {0}", Character.Protagonist.Grail));

            if (!String.IsNullOrEmpty(Character.Protagonist.Weapon))
                statusLines.Add(String.Format("Оружие: {0}", Character.Protagonist.Weapon));

            return statusLines;
        }

        public List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Game.Data.Triggers.Contains("Шоколадка"))
                staticButtons.Add("Съесть шоколадку");

            return staticButtons;
        }

        public bool StaticAction(string action)
        {
            Game.Option.Trigger("Шоколадка", remove: true);

            Character.Protagonist.Life += 3;

            return true;
        }

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Начать сначала";

            return Character.Protagonist.Life <= 0;
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
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        if (oneOption.Contains("ГРААЛЬ >=") && (int.Parse(oneOption.Split('=')[1]) > Character.Protagonist.Grail))
                            return false;
                    }
                    else if (oneOption.Contains("ОРУЖИЕ"))
                    {
                        if (oneOption.Contains("!") && (oneOption.Split('=')[1].Trim() == Character.Protagonist.Weapon))
                            return false;
                        else if (!oneOption.Contains("!") && (oneOption.Split('=')[1].Trim() != Character.Protagonist.Weapon))
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
        }

        public List<string> Representer() => new List<string> { Text.ToUpper() };

        public List<string> Get()
        {
            Character.Protagonist.Weapon = Text;

            return new List<string> { "RELOAD" };
        }
    }
}
