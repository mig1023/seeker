using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.LegendsAlwaysLie
{
    class Actions : Interfaces.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }
        public int Price { get; set; }
        public string Text { get; set; }
        public bool Spell { get; set; }


        public Character.SpecializationType? Specialization { get; set; }

        public List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            if (trigger)
                Game.Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = ((actionResult.Count >= 1) && (actionResult[0] == "RELOAD") ? true : false);

            return actionResult;
        }

        public List<string> Representer()
        {
            if (ActionName == "Get")
                return new List<string> { Text };

            return new List<string> { };
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Сила: {0}", Character.Protagonist.Strength),
                String.Format("Жизни: {0}", Character.Protagonist.Hitpoints),
                String.Format("Заклинаний: {0}", Character.Protagonist.Magicpoints),
                String.Format("Золото: {0}", Character.Protagonist.Gold)
            };

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
            bool disabledSpecializationButton = (Specialization != null) && (Character.Protagonist.Specialization != Character.SpecializationType.Nope);
            bool disabledByPrice = (Price > 0) && (Character.Protagonist.Gold < Price);
            bool disabledBySpellpoints = Spell && (Character.Protagonist.Spellpoints <= 0);
            bool disabledBySpellRepeat = Spell && Character.Protagonist.Spells.Contains(Text);

            bool disabledBySpecialization = Spell && (Text == "ВЗОР") && (Character.Protagonist.Specialization == Character.SpecializationType.Warrior);

            return !(disabledSpecializationButton || disabledByPrice || disabledBySpellpoints || disabledBySpellRepeat || disabledBySpecialization);
        }

        public List<string> Get()
        {
            if ((Specialization != null) && (Character.Protagonist.Specialization == Character.SpecializationType.Nope))
            {
                Character.Protagonist.Specialization = Specialization ?? Character.SpecializationType.Nope;

                if (Specialization == Character.SpecializationType.Warrior)
                    Character.Protagonist.Strength += 2;
                else if (Specialization == Character.SpecializationType.Wizard)
                {
                    Character.Protagonist.Spellpoints += 3;
                    Character.Protagonist.Magicpoints += 2;
                }
                else
                {
                    Character.Protagonist.Strength += 1;
                    Character.Protagonist.Spellpoints += 1;
                }
            }
            else if (Spell && (Character.Protagonist.Spellpoints >= 1))
            {
                Character.Protagonist.Spells.Add(Text);
                Character.Protagonist.Spellpoints -= 1;
            }
            else if ((Price > 0) && (Character.Protagonist.Gold >= Price))
                Character.Protagonist.Gold -= Price;

            return new List<string> { "RELOAD" };
        }

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
