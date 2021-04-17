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
        public bool ThisIsSpell { get; set; }
        public string Text { get; set; }

        public List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            if (trigger)
                Game.Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = (actionResult.Count >= 1) && (actionResult[0] == "RELOAD");

            return actionResult;
        }

        public List<string> Status()
        {
            if (Character.Protagonist.Time != null)
                return new List<string> { String.Format("Время: {0:d2}:00", Character.Protagonist.Time) };
            else
                return null;
        }

        public List<string> AdditionalStatus() => null;

        public List<string> StaticButtons() => new List<string> { };

        public bool StaticAction(string action) => false;

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = String.Empty;

            return false;
        }

        public bool IsButtonEnabled() => !(ThisIsSpell && (Character.Protagonist.SpellSlots <= 0));

        public List<string> Get()
        {
            Character.Protagonist.Spells.Add(Text);
            Character.Protagonist.SpellSlots -= 1;

            return new List<string> { "RELOAD" };
        }

        public static bool CheckOnlyIf(string option)
        {
            foreach (string oneOption in option.Split(','))
            {
                if (oneOption.Contains(">") || oneOption.Contains("<"))
                {
                    int level = int.Parse(oneOption.Split('>')[1]);

                    if (oneOption.Contains("ВРЕМЯ <") && (level <= Character.Protagonist.Time))
                        return false;
                    if (oneOption.Contains("ВРЕМЯ >=") && (level > Character.Protagonist.Time))
                        return false;
                }
                else if (oneOption.Contains("ЗАКЛЯТИЕ"))
                    return Character.Protagonist.Spells.Contains(oneOption.Trim());
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

        public List<string> Representer()
        {
            int count = 0;

            foreach (string spell in Character.Protagonist.Spells)
                if (spell == Text)
                    count += 1;

            return new List<string> { String.Format("{0}{1}", Text, (count > 0 ? String.Format(" (x{0})", count) : String.Empty)) };
        }

        public bool IsHealingEnabled() => false;

        public void UseHealing(int healingLevel) => Game.Other.DoNothing();

        public string TextByOptions(string option) => String.Empty;
    }
}
